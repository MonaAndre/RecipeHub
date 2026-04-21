import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { apiFetch } from '@/services/api'

export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('token'))
  const userName = ref(localStorage.getItem('userName'))
  const userId = ref(localStorage.getItem('userId') ? Number(localStorage.getItem('userId')) : null)

  const isLoggedIn = computed(() => !!token.value)

  async function login(email: string, password: string) {
    const data = await apiFetch<{ token: string; userName: string; userId: number }>('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, password }),
    })
    token.value = data.token
    userName.value = data.userName
    userId.value = data.userId
    localStorage.setItem('token', data.token)
    localStorage.setItem('userName', data.userName)
    localStorage.setItem('userId', String(data.userId))
  }

  async function register(payload: {
    userName: string
    email: string
    password: string
    firstName: string
    lastName: string
  }) {
    await apiFetch('/auth/register', {
      method: 'POST',
      body: JSON.stringify(payload),
    })
  }

  function logout() {
    token.value = null
    userName.value = null
    userId.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('userName')
    localStorage.removeItem('userId')
  }

  return { token, userName, userId, isLoggedIn, login, register, logout }
})
