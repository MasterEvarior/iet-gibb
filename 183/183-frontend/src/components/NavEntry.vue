<template>
  <v-list-item link :to="route">
    <v-list-item-icon>
      <v-icon>{{ icon }}</v-icon>
    </v-list-item-icon>

    <v-list-item-content>
     {{ text }}
    </v-list-item-content>
  </v-list-item>
</template>

<script>
export default {
  props: ['icon', 'text', 'route']
};
</script>