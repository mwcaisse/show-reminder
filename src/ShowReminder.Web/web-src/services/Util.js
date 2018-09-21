function getURLParameter(name, def) {
    if (typeof def === "undefined") {
        def = "";
    }
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? def : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function isStringNullOrBlank(string) {
    return (typeof string === "undefined" ||
        string === null ||
        typeof string !== "string" ||
        string.length === 0 ||
        string.trim().length === 0);
};

function formatDateTime(date, formatString) {
    if (typeof formatString === "undefined") {
        formatString = "YYYY-MM-DD HH:mm:ss";
    }
    if (date && date.isValid()) {
        return date.format(formatString);
    }
    return "";
};

function round(num, places) {
    if (!places) {
        places = 2;
    }
    return parseFloat(num).toFixed(places);
};

export default {
    getURLParameter: getURLParameter,
    isStringNullOrBlank: isStringNullOrBlank,
    formatDateTime: formatDateTime,
    round: round
}