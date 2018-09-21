<template>
    <div>
        <div class="box">
            <p class="subtitle">
                Add Show                
            </p>
            <div class="field has-addons">
                <div class="control has-icons-left is-expanded">
                    <input class="input is-medium" type="text" placeholder="Archer" v-model="searchTerms" />
                    <span class="icon is-medium is-left">
                        <app-icon icon="search" :action="false"></app-icon>
                    </span>                    
                </div>
                <div class="control">
                    <button class="button is-info is-medium" v-on:click="search">
                        Search
                    </button>
                </div>
            </div>
            <ul v-if="searchResults.length > 0">
                <li class="box" v-for="result in searchResults">
                    <div class="subtitle">
                        <span>{{ result.name }}</span>
                        <span v-if="result.firstAired">({{ result.firstAired | formatDate("YYYY")}})</span>
                        <span class="is-pulled-right">
                            <app-icon icon="plus" :action="true" v-on:click.native="addShow(result)"></app-icon>
                        </span>
                    </div>                  
                    <span>{{ result.overview }}</span>                     
                </li>
            </ul>
            <p v-if="previousSearchTerms !== null && searchResults.length == 0" class="has-text-centered">
                No results!
            </p>
        </div>
    </div>
</template>
<script>
    import system from "services/System.js"
    import { ShowService } from "services/ApplicationProxy.js"
    import Icon from "components/Common/Icon.vue"

    export default {
        name: "add-show",
        data: function () {   
            return {
                searchTerms: "",               
                searchResults: [],
                previousSearchTerms: null
            }
        },
        methods: {
            search: function () {
                if (this.searchTerms === this.previousSearchTerms) {
                    return
                }                
                ShowService.searchShows(this.searchTerms).then(function (data) {
                    this.searchResults = data.data;
                    this.previousSearchTerms = this.searchTerms;
                }.bind(this),
                function (error) {
                    console.log("Error searching for shows: " + error)
                });                
            },
            addShow: function (show) {
                ShowService.addShow(show.id).then(function (data) {
                    system.events.$emit("show:added", {});
                },
                function (error) {
                    console.log("Error searching for shows: " + error)
                });
            }
        },
        components: {
            "app-icon": Icon
        }
    }
</script>
