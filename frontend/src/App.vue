<script setup lang="ts">
import { RouterLink, RouterView, useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useAuthStore } from '@/stores/auth'

const auth = useAuthStore()
const { isLoggedIn, userName } = storeToRefs(auth)
const router = useRouter()

function logout() {
  auth.logout()
  router.push('/')
}
</script>

<template>
  <div class="min-h-screen bg-gray-50">
    <nav class="bg-white shadow-sm border-b border-gray-200">
      <div class="max-w-5xl mx-auto px-4 py-3 flex items-center justify-between">
        <RouterLink to="/" class="text-xl font-bold text-green-600">RecipeHub</RouterLink>
        <div class="flex items-center gap-4">
          <RouterLink to="/" class="text-gray-600 hover:text-green-600 transition-colors">Recept</RouterLink>
          <template v-if="isLoggedIn">
            <span class="text-gray-500 text-sm">{{ userName }}</span>
            <button @click="logout" class="text-sm text-red-500 hover:text-red-700 transition-colors">
              Logga ut
            </button>
          </template>
          <template v-else>
            <RouterLink to="/login" class="text-gray-600 hover:text-green-600 transition-colors">Logga in</RouterLink>
            <RouterLink
              to="/register"
              class="bg-green-600 text-white px-3 py-1.5 rounded-lg text-sm hover:bg-green-700 transition-colors"
            >
              Registrera
            </RouterLink>
          </template>
        </div>
      </div>
    </nav>

    <main class="max-w-5xl mx-auto px-4 py-8">
      <RouterView />
    </main>
  </div>
</template>
