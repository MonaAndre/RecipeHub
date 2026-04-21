import { describe, it, expect, vi, beforeEach } from 'vitest'
import { apiFetch } from '../api'

describe('apiFetch', () => {
  beforeEach(() => {
    vi.stubGlobal('fetch', vi.fn())
    localStorage.clear()
  })

  it('unwraps data from wrapper response', async () => {
    vi.mocked(fetch).mockResolvedValueOnce({
      ok: true,
      json: async () => ({ data: { recipes: [{ id: 1, recipeName: 'Pasta' }] }, message: 'OK' }),
    } as Response)

    const result = await apiFetch<{ recipes: { id: number; recipeName: string }[] }>('/paged-recipes')
    expect(result.recipes[0].recipeName).toBe('Pasta')
  })

  it('throws error on non-ok response', async () => {
    vi.mocked(fetch).mockResolvedValueOnce({
      ok: false,
      status: 404,
      statusText: 'Not Found',
    } as Response)

    await expect(apiFetch('/recipes/999')).rejects.toThrow('404 Not Found')
  })

  it('adds Authorization header when token exists in localStorage', async () => {
    localStorage.setItem('token', 'test-jwt-token')
    vi.mocked(fetch).mockResolvedValueOnce({
      ok: true,
      json: async () => ({ data: {} }),
    } as Response)

    await apiFetch('/recipes')

    expect(fetch).toHaveBeenCalledWith(
      expect.any(String),
      expect.objectContaining({
        headers: expect.objectContaining({
          Authorization: 'Bearer test-jwt-token',
        }),
      }),
    )
  })

  it('does not add Authorization header when no token', async () => {
    vi.mocked(fetch).mockResolvedValueOnce({
      ok: true,
      json: async () => ({ data: {} }),
    } as Response)

    await apiFetch('/recipes')

    const callHeaders = (vi.mocked(fetch).mock.calls[0][1] as RequestInit).headers as Record<string, string>
    expect(callHeaders['Authorization']).toBeUndefined()
  })
})
