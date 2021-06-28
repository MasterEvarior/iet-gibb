<template>
  <div>
    <b-row class="mb-4">
      <b-col>
        <h2>{{ video.title }}</h2>
        <p>{{ video.description }}</p>
        <video class="vid" controls>
          <source :src="getVidUrl(video.file_name)" type="video/mp4" />
        </video>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "vid",
  components: {},
  data: function() {
    return {};
  },
  props: {
    video: Object
  },
  methods: {
    getVidUrl(file_name) {
      return require("../assets/videos/" + file_name);
    }
  }
};
</script>

<style>
.vid {
  width: inherit;
}
</style>
