<template>
    <div class="pdf">
        <h2 class="title">{{pdf.title}}</h2>
        <iframe :src="getPdfUrl(pdf.file)" width="100%" />
        <a :href="getPdfUrl(pdf.file)" download>Download PDF</a>
    </div>
</template>


<script>
export default {
    props: {
        pdf: Object
    },
    methods: {
        getPdfUrl(file_name) {
            return require('./../assets/pdfs/' + file_name);
        }
    }
}
</script>

<style scoped>
    .pdf {
        border: white solid 5px;
        max-height: 10%;
    }
    iframe {
        min-height: 15rem;
        max-height: 21rem;
    }
    .title{
        font-size: 3vw;
    }
</style>