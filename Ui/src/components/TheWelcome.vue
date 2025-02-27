<script setup lang="ts">
import { useUserStore } from '@/stores/user'

import { ref } from 'vue'
const username = ref('')
const password = ref('')
const userStore = useUserStore()

async function login() {
  console.log(username, password)
  const rawResponse = await fetch('http://localhost:5171/login', {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      username: username.value,
      password: password.value
    })
  })
  const json = await rawResponse.json()
  userStore.setUser(json.userDto)
  userStore.setToken(json.token)
}
</script>

<template>
  <v-text-field label="Username" v-model="username"></v-text-field>
  <v-text-field label="Password" v-model="password" type="password"></v-text-field>
  <v-btn text="Login" @click="login"></v-btn>
  <br />
</template>
<script lang="ts"></script>
