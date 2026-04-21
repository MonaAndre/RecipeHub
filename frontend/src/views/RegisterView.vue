<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const auth = useAuthStore()
const form = ref({ userName: '', email: '', password: '', firstName: '', lastName: '' })
const error = ref('')
const loading = ref(false)

async function submit() {
  error.value = ''
  loading.value = true
  try {
    await auth.register(form.value)
    await auth.login(form.value.email, form.value.password)
    router.push('/')
  } catch {
    error.value = 'Registreringen misslyckades. Kontrollera uppgifterna.'
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="max-w-md mx-auto mt-12">
    <div class="bg-white rounded-xl border border-gray-200 p-8">
      <h1 class="text-2xl font-bold text-gray-800 mb-6">Skapa konto</h1>

      <div class="space-y-4">
        <div class="grid grid-cols-2 gap-3">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Förnamn</label>
            <input v-model="form.firstName" type="text" class="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400" />
          </div>
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Efternamn</label>
            <input v-model="form.lastName" type="text" class="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400" />
          </div>
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Användarnamn</label>
          <input v-model="form.userName" type="text" class="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400" />
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">E-post</label>
          <input v-model="form.email" type="email" class="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400" />
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Lösenord</label>
          <input v-model="form.password" type="password" @keyup.enter="submit" class="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400" />
        </div>

        <p v-if="error" class="text-red-500 text-sm">{{ error }}</p>

        <button
          @click="submit"
          :disabled="loading"
          class="w-full bg-green-600 text-white py-2 rounded-lg text-sm font-medium hover:bg-green-700 disabled:opacity-50 transition-colors"
        >
          {{ loading ? 'Skapar konto...' : 'Registrera' }}
        </button>
      </div>

      <p class="mt-4 text-sm text-center text-gray-500">
        Har du redan ett konto?
        <RouterLink to="/login" class="text-green-600 hover:underline">Logga in</RouterLink>
      </p>
    </div>
  </div>
</template>
