<template>
    <v-container>
        <v-row justify="center">
            <h1>Register</h1>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-text-field 
                label="Username" 
                v-model="username" 
                outlined
                hint="Must be at least 3 letters long">
                </v-text-field>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-text-field 
                    type="password"
                    label="Password" 
                    v-model="password"
                    :error="password !== repeatPassword ? true : false"
                    outlined
                    hint="Password must be at least 10 characters. 1 small letter, 1 capital letter, 1 special character and 1 number.">
                </v-text-field>
                <v-text-field
                    type="password"
                    label="Repeat Password"
                    v-model="repeatPassword"
                    :error="password !== repeatPassword ? true : false"
                    outlined
                >
                </v-text-field>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="12">
                <p class="red--text" v-if="invalid">{{ invalid }}</p>
                <v-btn 
                    v-on:click="handleRegister"
                    :disabled="password !== repeatPassword ? true : false"
                    outlined
                >
                    Register
                </v-btn>
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
            repeatPassword: '',
            invalid: '',
        }
    },
    methods: {
        async handleRegister() {
            const data = {
                username: this.username,
                password: this.password,
            };
            const response = await this.postRegister(data);
            if (response) {
                setJWT(response)
                this.$router.push('/');
            }
        },
        async postRegister(data) {
            const response = await fetch(`${this.API_URL}/users`, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            });
            if (response.status === 201) {
                return response.json();
            } else {
                // handle errors
                if (response.status === 400) {
                    this.invalid = 'The entered values are invalid'
                }
            }
        },
    }
    
}
</script>