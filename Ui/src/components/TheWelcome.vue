<script setup lang="ts">
import { useUserStore } from '@/stores/user'

import { ref } from 'vue'
const todos = ref([])
const username = ref('');
const password = ref('')
const userStore = useUserStore()
function getTodoItems() {
  fetch('http://localhost:5171/todoitems')
    .then((response) => response.json())
    .then((data) => {
      todos.value = data
    })
}

async function login() {
  console.log(username, password);
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

function logItems() {
  console.log(userStore.getUser(), userStore.getToken())
}
</script>

<template>
  <v-text-field label="Username" v-model="username"></v-text-field>
  <v-text-field label="Password" v-model="password" type="password"></v-text-field>
  <v-btn text="Login" @click="login"></v-btn>
    <button @click="login">Login</button>
    <button @click="getTodoItems">Get Items</button>
    <button @click="logItems">Log Items</button>
    <br />
</template>
<script lang="ts"></script>
