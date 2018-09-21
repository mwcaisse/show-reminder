<template>
    <div>
        <section class="section">
            <add-show></add-show>
        </section>
        <section class="section">
            <show-list></show-list>
        </section>
    </div>
</template>

<script>
    import AddShow from "components/Show/AddShow.vue"
    import ShowList from "components/Show/ShowList.vue"

    export default {
        data: function() {
            return {             
            }
        },
        components: {
            "add-show": AddShow,
            "show-list": ShowList
        }
    }
</script>