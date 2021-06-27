<template>
    <v-container>
        <v-row justify="center">
            <h1>Login</h1>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-text-field label="Username" outlined v-model="username"></v-text-field>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-text-field type="password" label="Password" outlined v-model="password"></v-text-field>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="12">
                <p class="red--text" v-if="invalid">{{ invalid }}</p>
                <v-btn v-on:click="handleLogin" outlined >
                    Login
                </v-btn>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="12">
                <router-link to="/register">Register here.</router-link>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { setJWT } from '../auth';

export default {
    data() {
        return {
            username: '',
            password: '',
            invalid: '',
        }
    },
    methods: {
        async handleLogin() {
            const data = {
                username: this.username,
                password: this.password,
            };
            const response = await this.postLogin(data);
            if (response) {
                setJWT(response)
                this.$router.push({ name: 'Command' });
            }
        },
        async postLogin(data) {
            const response = await fetch(`${this.API_URL}/authenticate`, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            });
            if (response.status === 200) {
                this.invalid = '';
                return response.text();
            } else {
                // handle errors
                if (response.status === 400) {
                    this.invalid = 'Incorrect username or password'
                }
            }
        },
    }
}
</script>
