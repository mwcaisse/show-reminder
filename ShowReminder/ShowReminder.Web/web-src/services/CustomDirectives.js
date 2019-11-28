import Vue from "vue"
import Moment from "moment"

import Util from "services/Util.js"

function formatDateFilter(value, formatString) {
    if (typeof value === "undefined" || null == value) {
        return "";
    }
    if (typeof value.format !== "function") {
        value = Moment(value);
    }
    return Util.formatDateTime(value, formatString);
}

Vue.filter("formatDateTime", function (value, formatString) {
    return formatDateFilter(value, formatString);
});

Vue.filter("formatDate", function (value, formatString) {
    if (Util.isStringNullOrBlank(formatString)) {
        formatString = "YYYY-MM-DD";
    }
    return formatDateFilter(value, formatString);
}); 

Vue.filter("formatBoolean", function (value, nullIsBlank) {
    if (value === true) {
        return "Yes";
    }
    else if (value !== false && nullIsBlank) {
        return "";
    }
    return "No";
});

Vue.filter("round", function (value, places) {
    if (typeof value === "undefined") {
        return "";
    }

    if (typeof places === "undefined") {
        places = 2;
    }
    return Util.round(value, places);

});