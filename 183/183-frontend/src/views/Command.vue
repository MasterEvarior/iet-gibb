<template>
    <v-container>
        <v-row justify="center">
            <v-col>
                <h1>Command</h1>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-combobox
                    clearable
                    multiple
                    outlined
                    :items="items"
                    v-model="selectedItems"
                    label="Command Options"
                >
                </v-combobox>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-btn
                    outlined
                    v-on:click='handleExecution'
                >
                    Execute command
                </v-btn>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                Your command resulted in the following output:
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-col cols="8">
                <v-textarea
                    outlined
                    readonly
                    v-model="output"
                >

                </v-textarea>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { getJWT } from '../auth';

export default {
    data() {
        return {
            items: ['d', 's', 'h'],
            selectedItems: [],
            output: ""
        }
    },
    methods: {
        async handleExecution() {
            if (!this.selectedItems.every(a => this.items.includes(a))) return;
            const query = this.selectedItems.length > 0 ? `/command?options=${this.selectedItems.join(',')}` : '/command';
            const response = await this.executeCommand(query);
            if (response) this.output = response.result;
        },
        async executeCommand(query) {
            const response = await fetch(`${this.API_URL}${query}`, {
                method: 'get',
                headers: {
                    'Authorization': `Bearer ${getJWT()}`,
                }
            });
            if (response.status === 200) {
                return response.json();
            } else {
                // handle errors
                if (response.status === 401) {
                    this.$router.push('/login');
                }
            }
        }
    }
}
</script>