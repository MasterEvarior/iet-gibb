<template>
  <div>
    <h1>Video & Livestream</h1>
    <Video :video="video" />
    <livestream :stream="stream" />
  </div>
</template>

<script>
import profiles from "../assets/profile_data.json";
import Video from "./Video.vue";
import Livestream from "./Livestream.vue";

export default {
  name: "video-page",
  components: {
    Video,
    Livestream
  },
  methods: {},
  data: function() {
    return {
      video: {
        title: "Big Buck Bunny (Video)",
        description: "Placeholder video under creative commons licence.",
        file_name: "big_buck_bunny.mp4"
      },
      stream: {
        title: "NASA ISS (Livestream)",
        description: "Livestream of space from Amazing Space hosted on YouTube.",
        url: "big_buck_bunny.mp4"
      }
    };
  },
  created: function() {
    this.profile = profiles.profiles.find(
      x => x.route == this.$route.params.name
    );
  }
};
</script>

<style>
</style>
