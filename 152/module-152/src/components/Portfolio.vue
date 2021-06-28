<template>
    <div>
        <h1>{{ this.portfolio.name }}</h1>
        <b-row cols-md="1">
            <b-col class="column" v-for="pdf in this.portfolio.pdfs" v-bind:key="pdf.title">
                <EmbededPdf :pdf="pdf"/>
            </b-col>
        </b-row>
        <Photohunt v-for="photohunt in this.portfolio.photohunts" v-bind:key="photohunt.name" :photohunt="photohunt"/>
    </div>
</template>

<script>
import portfolios from '../assets/portfolio_data.json';
import Photohunt from './Photohunt.vue';
import EmbededPdf from './EmbededPdf.vue'

export default {
  name: 'portfolio',
  components: {
      Photohunt,
      EmbededPdf
  },
  methods: {},
  data: function () {
  return {
        portfolio: null
        }
 },
  created: function() {
      this.portfolio = portfolios.portfolios.find(x => x.route == this.$route.params.name) 
 }
}
</script>

<style>
.column {
  float: left;
  max-width: 33.3%;
  padding: 5px;
  max-height: 20%;
}

.row::after {
  content: "";
  clear: both;
  display: table;
}
</style>
