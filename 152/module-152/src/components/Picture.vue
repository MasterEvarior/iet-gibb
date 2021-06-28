<template>
  <div>
    <head>
      <link
        rel="stylesheet"
        href="https://unpkg.com/beerslider/dist/BeerSlider.css"
      />
    </head>

    <b-row class="mb-4">
      <b-col>
        <div class="frame">
          <h2>{{ picture.title }}</h2>
          <p>{{ picture.description }}</p>
          <hr>
          <div
            :id="'slider' + picture.id"
            class="beer-slider"
            data-beer-label="Bevor"
            ref="slider">
            <img :src="getImgUrl(picture.file_name_original)" alt="" />
            <div class="beer-reveal" data-beer-label="Danach">
              <img :src="getImgUrl(picture.file_name_edited)" alt="" />
            </div>
          </div>
        </div>
      </b-col>
    </b-row>
  </div>
</template>

<script src="https://unpkg.com/beerslider/dist/BeerSlider.js"></script>
<script>
new BeerSlider(document.getElementById('slider'));
</script>

<script>
import BeerSlider from 'beerslider';

export default {
  name: 'pic',
  data() {
    return {};
  },
  props: {
    picture: Object,
  },
  methods: {
    getImgUrl(file_name) {
      return require('../assets/pictures/' + file_name);
    },
  },
  mounted: function() {
    new BeerSlider(this.$refs['slider']);
  },
};
</script>

<style>
.picture {
  width: inherit;
}
.frame {
  color: black;
}
</style>
