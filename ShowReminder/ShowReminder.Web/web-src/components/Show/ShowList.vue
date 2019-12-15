<template>
    <div>
        <div class="box">
            <p class="subtitle">
                My Shows
                <span class="is-pulled-right">
                    <app-icon icon="sync-alt" :action="true" v-on:click.native="fetchShows"></app-icon>
                </span>
            </p>
            <ul v-if="shows.length > 0">
                <li class="box" v-for="show in shows">
                    <span class="subtitle">{{show.name}}</span>
                    <span class="is-pulled-right">
                        <a v-bind:href="show | showLink" target="_blank">
                            <app-icon icon="download" :action="true"></app-icon>
                        </a>
                        &nbsp;
                        <app-icon icon="trash" :action="true" v-on:click.native="deleteShow(show)"></app-icon>
                    </span>
                    <div class="columns">
                        <div class="column">
                            <div class="field">
                                <label class="label">First Aired</label>
                                <div class="control">
                                    <span>{{ show.firstAiredDate | formatDate }}</span>
                                </div>
                            </div>
                            <div class="field">
                                <label class="label">Last Aired</label>
                                <div class="control">
                                    <span v-if="show.lastEpisode === null">N/A</span>
                                    <span v-else>{{ show.lastEpisode.airDate | formatDate }}</span>
                                    <br />
                                    <span v-if="show.lastEpisode !== null">S{{show.lastEpisode.seasonNumber}}E{{show.lastEpisode.episodeNumber}}</span>
                                </div>
                            </div>
                        </div>
                        <div class="column">
                            <div class="field">
                                <label class="label">Airs On</label>
                                <div class="control">
                                    <span>{{ show.airDay }}</span>
                                </div>
                            </div>
                            <div class="field">
                                <label class="label">Airs Next</label>
                                <div class="control">
                                    <span v-if="show.nextEpisode === null">N/A</span>
                                    <span v-else>{{ show.nextEpisode.airDate | formatDate("dddd, MMMM D, YYYY") }}</span>
                                    <br />
                                    <span v-if="show.nextEpisode !== null">S{{show.nextEpisode.seasonNumber}}E{{show.nextEpisode.episodeNumber}}</span>
                                </div>
                            </div>
                        </div>

                    </div>
                </li>
            </ul>
            <p v-else class="has-text-centered">
                You haven't added any shows yet!
            </p>
        </div>        
    </div>
</template>

<script>
    import system from "services/System.js"
    import { ShowService } from "services/ApplicationProxy.js"
    import Icon from "components/Common/Icon.vue"

    export default {
        name: "show-list",
        data: function() {
            return {
                shows: []
            }
        },        
        methods: {
            fetchShows: function () {
                ShowService.getAll().then(function (data) {
                    this.shows = data.data;
                }.bind(this),
                function (error) {
                    console.log("Error fetching shows: " + error)
                });
            },      
            deleteShow: function (show) {
                ShowService.delete(show.id).then(function (res) {
                    if (res) {
                        var index = this.shows.indexOf(show);
                        this.shows.splice(index, 1);
                    }
                    else {
                        console.log("Failed to delete show.");
                    }
                }.bind(this), function (error) {
                    console.log("Error deleting show: " + error)
                })
            }        
        }, 
        created: function () {
            this.fetchShows();

            system.events.$on("show:added", function () {
                this.fetchShows();
            }.bind(this))
        },
        components: {
            "app-icon": Icon 
        }
    }
</script>
<style>
    a {
        color: inherit;
    }
</style>