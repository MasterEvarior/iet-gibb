<template>
    <div>
        <b-row class="mb-4">
            <b-col style="height: 524px">
                <h2>{{ stream.title }}</h2>
                <p>{{ stream.description }}</p>
                <iframe width="100%" height="100%" src="https://www.youtube.com/embed/EEIk7gwjgIM?controls=0&autoplay=1&showinfo=0&mute=1" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
            </b-col>
        </b-row>
    </div>
</template>

<script>

    export default {
        name: "livestream",
        components: {},
        data: function () {
            return {};
        },
        props: {
            stream: Object
        }
    };
</script>

<style>
    .vid {
        width: inherit;
    }
</style>
