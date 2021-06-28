<template>
    <div>
        <h2 class="title">{{ photohunt.title }}</h2>
        <div class="row">
            <div class="column" v-for="picture in photohunt.pictures" v-bind:key="picture">
                <b-img thumbnail class="picture" :src="getImgUrl(picture)"/>
            </div>
        </div>
    </div>    
</template>

<script>

export default {
    props: {
        photohunt: Object
    },
    methods: {
        getImgUrl(file_name) {
            return require('../assets/pictures/' + file_name);
        }
    }
}
</script>


<style>
.column {
  float: left;
  max-width: 33.3%;
  padding: 5px;
}

.row::after {
  content: "";
  clear: both;
  display: table;
}

.title{
  font-size: 3vw;
}
</style>