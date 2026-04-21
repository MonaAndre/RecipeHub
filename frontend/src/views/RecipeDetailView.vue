<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { apiFetch } from '@/services/api'
import { useAuthStore } from '@/stores/auth'

interface RecipeDetail {
  recipeId: number
  recipeName: string
  recipeDescription: string | null
  recipeCategory: string | null
  userId: number
  userName: string
  instructionSteps: { stepId: number; stepNumber: number; stepText: string }[]
  ingredients: { productId: number; productName: string; quantity: number; unit: string }[]
  comments: { commentId: number; text: string; userName: string; createdAt: string }[]
}

const route = useRoute()
const router = useRouter()
const auth = useAuthStore()
const recipe = ref<RecipeDetail | null>(null)
const loading = ref(true)
const error = ref('')
const newComment = ref('')
const commentError = ref('')

async function fetchRecipe() {
  try {
    recipe.value = await apiFetch<RecipeDetail>(`/recipes/${route.params.id}`)
  } catch {
    error.value = 'Kunde inte hämta receptet.'
  } finally {
    loading.value = false
  }
}

async function submitComment() {
  if (!newComment.value.trim()) return
  commentError.value = ''
  try {
    await apiFetch(`/recipes/${route.params.id}/comments/`, {
      method: 'POST',
      body: JSON.stringify({ text: newComment.value }),
    })
    newComment.value = ''
    await fetchRecipe()
  } catch {
    commentError.value = 'Kunde inte skicka kommentar.'
  }
}

async function deleteComment(commentId: number) {
  try {
    await apiFetch(`/recipes/${route.params.id}/comments/${commentId}`, { method: 'DELETE' })
    await fetchRecipe()
  } catch {
    // ignore
  }
}

onMounted(fetchRecipe)
</script>

<template>
  <div>
    <button @click="router.back()" class="mb-4 text-sm text-gray-500 hover:text-green-600 transition-colors">
      ← Tillbaka
    </button>

    <div v-if="loading" class="text-center py-16 text-gray-400">Laddar...</div>
    <div v-else-if="error" class="text-center py-16 text-red-500">{{ error }}</div>

    <div v-else-if="recipe">
      <div class="bg-white rounded-xl border border-gray-200 p-6 mb-6">
        <div class="flex items-start justify-between">
          <div>
            <span v-if="recipe.recipeCategory" class="text-xs font-medium text-green-600 bg-green-50 px-2 py-0.5 rounded-full">
              {{ recipe.recipeCategory }}
            </span>
            <h1 class="mt-2 text-2xl font-bold text-gray-800">{{ recipe.recipeName }}</h1>
            <p class="text-sm text-gray-500 mt-1">av {{ recipe.userName }}</p>
          </div>
        </div>
        <p v-if="recipe.recipeDescription" class="mt-4 text-gray-700">{{ recipe.recipeDescription }}</p>
      </div>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
        <div v-if="recipe.ingredients.length" class="bg-white rounded-xl border border-gray-200 p-5">
          <h2 class="font-semibold text-gray-800 mb-3">Ingredienser</h2>
          <ul class="space-y-1">
            <li v-for="ing in recipe.ingredients" :key="ing.productId" class="text-sm text-gray-700 flex justify-between">
              <span>{{ ing.productName }}</span>
              <span class="text-gray-500">{{ ing.quantity }} {{ ing.unit }}</span>
            </li>
          </ul>
        </div>

        <div v-if="recipe.instructionSteps.length" class="bg-white rounded-xl border border-gray-200 p-5">
          <h2 class="font-semibold text-gray-800 mb-3">Instruktioner</h2>
          <ol class="space-y-2">
            <li v-for="step in recipe.instructionSteps" :key="step.stepId" class="text-sm text-gray-700 flex gap-3">
              <span class="font-semibold text-green-600 shrink-0">{{ step.stepNumber }}.</span>
              <span>{{ step.stepText }}</span>
            </li>
          </ol>
        </div>
      </div>

      <div class="bg-white rounded-xl border border-gray-200 p-5">
        <h2 class="font-semibold text-gray-800 mb-4">Kommentarer ({{ recipe.comments.length }})</h2>

        <div v-if="recipe.comments.length === 0" class="text-sm text-gray-400 mb-4">Inga kommentarer ännu.</div>
        <div v-else class="space-y-3 mb-4">
          <div v-for="comment in recipe.comments" :key="comment.commentId" class="flex justify-between items-start bg-gray-50 rounded-lg p-3">
            <div>
              <div class="flex items-baseline gap-2">
                <p class="text-sm font-medium text-gray-700">{{ comment.userName }}</p>
                <p class="text-xs text-gray-400">{{ new Date(comment.createdAt).toLocaleString('sv-SE', { dateStyle: 'short', timeStyle: 'short' }) }}</p>
              </div>
              <p class="text-sm text-gray-600">{{ comment.text }}</p>
            </div>
            <button
              v-if="auth.isLoggedIn && auth.userName === comment.userName"
              @click="deleteComment(comment.commentId)"
              class="text-xs text-red-400 hover:text-red-600 transition-colors ml-2 shrink-0"
            >
              Ta bort
            </button>
          </div>
        </div>

        <div v-if="auth.isLoggedIn" class="flex gap-2">
          <input
            v-model="newComment"
            @keyup.enter="submitComment"
            type="text"
            placeholder="Skriv en kommentar..."
            class="flex-1 border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          />
          <button
            @click="submitComment"
            class="bg-green-600 text-white px-4 py-2 rounded-lg text-sm hover:bg-green-700 transition-colors"
          >
            Skicka
          </button>
        </div>
        <p v-if="commentError" class="text-red-500 text-sm mt-2">{{ commentError }}</p>
        <p v-if="!auth.isLoggedIn" class="text-sm text-gray-400">
          <RouterLink to="/login" class="text-green-600 hover:underline">Logga in</RouterLink> för att kommentera.
        </p>
      </div>
    </div>
  </div>
</template>
