import Q from "q"

var baseUrl = ($("#apiUrl").val() || "");

//If the server returned a 200, just return the results to the caller
function successHandler(deferred, data) {
    deferred.resolve(data);
}

//If the server return a non 200, return the error message to the caller
function errorHandler(deferred, jqXHR, textStatus, error) {
    var body = JSON.parse(jqXHR.responseText);
    if (body && body.error) {
        deferred.reject(body.error);
    } else {
        deferred.reject(textStatus);
    }
}

function ajax(options) {
    var def = Q.defer();
    var defaults = {
        async: true,
        contentType: "application/json",
        data: null,
        method: "GET",
        success: function (data, textStatus, jqXHR) {
            successHandler(def, data);
        },
        error: function (jqXHR, textStatus, error) {
            errorHandler(def, jqXHR, textStatus, error);
        }
    };
    var ajaxOptions = $.extend(defaults, options);

    $.ajax(ajaxOptions);
    return def.promise;
}

function getAbsolute(url) {
    return ajax({
        url: url,
        method: "GET"
    });
}

function get(relativeUrl) {
    return getAbsolute(baseUrl + relativeUrl);
}

function getUrlValue(val) {
    if (val instanceof Date) {
        return val.valueOf();
    }
    return val;
}

function getPaged(relativeUrl, skip, take, sort, filter) {
    var sortString = "";
    var filterString = "";
    if (sort && sort.propertyId) {
        sortString = "&columnName=" + sort.propertyId + "&ascending=" + (sort.ascending ? "true" : "false");
    }
    if (filter) {
        $.each(filter, function (property, filters) {
            $.each(filters, function (op, val) {
                filterString += "&" + property + "__" + op + "=" + getUrlValue(val);
            });
        });
    }
    return get(relativeUrl + "?skip=" + skip + "&take=" + take + sortString + filterString);
}

function postAbsolute(url, body) {
    return ajax({
        url: url,
        data: JSON.stringify(body),
        method: "POST"
    });
}

function post(relativeUrl, body) {
    return postAbsolute(baseUrl + relativeUrl, body);
}

function putAbsolute(url, body) {
    return ajax({
        url: url,
        data: JSON.stringify(body),
        method: "PUT"
    });
}

function put(relativeUrl, body) {
    return putAbsolute(baseUrl + relativeUrl, body);
};

function deleteAbsolute(url, body) {
    return ajax({
        url: url,
        data: JSON.stringify(body),
        method: "DELETE"
    });
}

function del(relativeUrl, body) {
    return deleteAbsolute(baseUrl + relativeUrl, body);
}

export default {
    getAbsolute: getAbsolute,
    get: get,
    getPaged: getPaged,
    postAbsolute: postAbsolute,
    post: post,
    putAbsolute: putAbsolute,
    put: put,
    deleteAbsolute: deleteAbsolute,
    delete: del
};

