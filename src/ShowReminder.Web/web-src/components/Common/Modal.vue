<template>
    <div class="modal" v-bind:class="{'is-active': show }">
        <div class="modal-background" v-on:click="close"></div>
        <div class="modal-card">
            <header class="modal-card-head">
                <p class="modal-card-title">{{title}}</p>
                <button class="delete" aria-label="close" v-on:click="close"></button>
            </header>
            <section class="modal-card-body">
                <slot>
                    <p>Empty Modal. Feel free to fill this in</p>
                </slot>
            </section>
            <footer class="modal-card-foot" style="justify-content: flex-end;">              
                <slot name="footer-buttons"></slot>
                <button class="button" type="button" v-on:click="close">{{ cancelText }}</button>
            </footer>
        </div>
    </div>
</template>

<script>
     export default {
        name: "app-modal",
        data: function() {
            return {
                show: false
            }
        },     
        props: {
            title: {
                type: String,
                required: true
            },
            cancelText: {
                type: String,
                required: false,
                default: "Cancel"
            }
        },
        methods: {
            open: function () {
                this.show = true;
            },
            close: function () {
                this.show = false;
            }
        }
    }
</script>

<style scoped>
    .action-icon:hover {
        transform: scale(1.5);
        cursor: pointer;
    }
</style>