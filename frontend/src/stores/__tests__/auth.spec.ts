import { describe, it, expect, vi, beforeEach } from 'vitest'
import { setActivePinia, createPinia } from 'pinia'
import { useAuthStore } from '../auth'

vi.mock('@/services/api', () => ({
  apiFetch: vi.fn(),
}))

import { apiFetch } from '@/services/api'

describe('useAuthStore', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
    localStorage.clear()
    vi.clearAllMocks()
  })

  it('isLoggedIn is false when no token', () => {
    const auth = useAuthStore()
    expect(auth.isLoggedIn).toBe(false)
  })

  it('login sets token, userName and userId', async () => {
    vi.mocked(apiFetch).mockResolvedValueOnce({
      token: 'abc123',
      userName: 'TestUser',
      userId: 1,
    })

    const auth = useAuthStore()
    await auth.login('test@test.com', 'password')

    expect(auth.isLoggedIn).toBe(true)
    expect(auth.userName).toBe('TestUser')
    expect(auth.userId).toBe(1)
    expect(localStorage.getItem('token')).toBe('abc123')
  })

  it('logout clears token and userName', async () => {
    vi.mocked(apiFetch).mockResolvedValueOnce({
      token: 'abc123',
      userName: 'TestUser',
      userId: 1,
    })

    const auth = useAuthStore()
    await auth.login('test@test.com', 'password')
    auth.logout()

    expect(auth.isLoggedIn).toBe(false)
    expect(auth.userName).toBeNull()
    expect(localStorage.getItem('token')).toBeNull()
  })

  it('login throws error on failed request', async () => {
    vi.mocked(apiFetch).mockRejectedValueOnce(new Error('401 Unauthorized'))

    const auth = useAuthStore()
    await expect(auth.login('wrong@test.com', 'wrong')).rejects.toThrow('401 Unauthorized')
    expect(auth.isLoggedIn).toBe(false)
  })
})
