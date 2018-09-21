import Proxy from "services/Proxy.js"

var show = {
    searchShows: function(terms) {
        return Proxy.get("show/search?terms=" + terms);
    },
    getShow: function(id) {
        return Proxy.get("show/" + id)
    },
    getAll: function() {
        return Proxy.get("show/tracked/");
    },
    addShow: function(id) {
        return Proxy.post("show/tracked/add/" + id, {});
    },
    delete: function(id) {
        return Proxy.del("show/tracked/" + id);
    }
};

export {
    show as ShowService
}