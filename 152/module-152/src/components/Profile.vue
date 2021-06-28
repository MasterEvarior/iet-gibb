<template>
    <div>
        <h1>{{ profile.name }}</h1>
        <Picture v-for="picture in profile.pictures" v-bind:key="picture.title" :picture="picture" />
    </div>
</template>

<script>
import profiles from '../assets/profile_data.json'
import Picture from './Picture.vue'

export default {
  name: 'profile',
  components: {
      Picture
  },
  methods: {},
  data: function () {
  return {
        profile: null
    }
    },
  created: function() {
      this.profile = profiles.profiles.find(x => x.route == this.$route.params.name) 
  }
}
</script>

<style>
</style>
