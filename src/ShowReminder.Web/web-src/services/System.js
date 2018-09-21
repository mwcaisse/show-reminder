import Vue from "vue"
import VueConfig from "services/VueCommon.js"
import "services/CustomDirectives.js"

class System {
    constructor() {
        this._events = new Vue();

        VueConfig();
    }

    get events() {
        return this._events;
    }
 
}

export default new System();