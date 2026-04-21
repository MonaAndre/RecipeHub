<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useRouter } from 'vue-router'
import { apiFetch } from '@/services/api'

interface Recipe {
  id: number
  recipeName: string
  recipeCategory: string
  userName: string
}

interface PagedResponse {
  page: number
  pageSize: number
  totalCount: number
  recipes: Recipe[]
}

const router = useRouter()
const recipes = ref<Recipe[]>([])
const categories = ref<string[]>([])
const search = ref('')
const selectedCategory = ref('')
const page = ref(1)
const pageSize = 9
const totalCount = ref(0)
const loading = ref(false)
const error = ref('')

async function fetchCategories() {
  try {
    const all = await apiFetch<Recipe[]>('/recipes/all-recipes')
    categories.value = [...new Set(all.map(r => r.recipeCategory).filter(Boolean))]
  } catch {
    // ignore
  }
}

async function fetchRecipes() {
  loading.value = true
  error.value = ''
  try {
    const params = new URLSearchParams({
      page: String(page.value),
      pageSize: String(pageSize),
      ...(search.value ? { search: search.value } : {}),
      ...(selectedCategory.value ? { category: selectedCategory.value } : {}),
    })
    const data = await apiFetch<PagedResponse>(`/paged-recipes?${params}`)
    recipes.value = data.recipes ?? []
    totalCount.value = data.totalCount ?? 0
  } catch {
    error.value = 'Kunde inte hämta recept.'
  } finally {
    loading.value = false
  }
}

const totalPages = () => Math.ceil(totalCount.value / pageSize)

watch([search, selectedCategory], () => {
  page.value = 1
  fetchRecipes()
})

onMounted(() => {
  fetchCategories()
  fetchRecipes()
})
</script>

<template>
  <div>
    <div class="mb-6 flex flex-wrap items-center justify-between gap-3">
      <h1 class="text-2xl font-bold text-gray-800">Recept</h1>
      <div class="flex gap-2">
        <select
          v-model="selectedCategory"
          class="border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400 bg-white"
        >
          <option value="">Alla kategorier</option>
          <option v-for="cat in categories" :key="cat" :value="cat">{{ cat }}</option>
        </select>
        <input
          v-model="search"
          type="text"
          placeholder="Sök recept..."
          class="border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-400 w-52"
        />
      </div>
    </div>

    <div v-if="loading" class="text-center py-16 text-gray-400">Laddar...</div>
    <div v-else-if="error" class="text-center py-16 text-red-500">{{ error }}</div>
    <div v-else-if="recipes.length === 0" class="text-center py-16 text-gray-400">Inga recept hittades.</div>

    <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
      <div
        v-for="recipe in recipes"
        :key="recipe.id"
        @click="router.push(`/recipes/${recipe.id}`)"
        class="bg-white rounded-xl border border-gray-200 p-5 cursor-pointer hover:shadow-md hover:border-green-300 transition-all"
      >
        <span
          v-if="recipe.recipeCategory"
          class="text-xs font-medium text-green-600 bg-green-50 px-2 py-0.5 rounded-full"
        >
          {{ recipe.recipeCategory }}
        </span>
        <h2 class="mt-2 text-base font-semibold text-gray-800">{{ recipe.recipeName }}</h2>
        <p class="mt-1 text-sm text-gray-500">av {{ recipe.userName }}</p>
      </div>
    </div>

    <div v-if="totalPages() > 1" class="mt-8 flex justify-center gap-2">
      <button
        :disabled="page === 1"
        @click="page--; fetchRecipes()"
        class="px-3 py-1.5 rounded-lg border text-sm disabled:opacity-40 hover:bg-gray-100 transition-colors"
      >
        Föregående
      </button>
      <span class="px-3 py-1.5 text-sm text-gray-600">{{ page }} / {{ totalPages() }}</span>
      <button
        :disabled="page >= totalPages()"
        @click="page++; fetchRecipes()"
        class="px-3 py-1.5 rounded-lg border text-sm disabled:opacity-40 hover:bg-gray-100 transition-colors"
      >
        Nästa
      </button>
    </div>
  </div>
</template>
