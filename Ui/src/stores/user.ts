import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {
  state: () => {
    return {
      user: {
        email: '',
        firstName: '',
        id: 0,
        lastName: '',
        userAccess: [],
        username: ''
      },
      token: ''
    }
  },
  // could also be defined as
  // state: () => ({ count: 0 })
  actions: {
    setUser(user: any) {
      this.user = user
    },
    setToken(token: string) {
      this.token = token
    },
    getUser() {
      return this.user
    },
    getToken() {
      return this.token
    }
  }
})
