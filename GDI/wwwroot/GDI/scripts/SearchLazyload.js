var searchResultsPageInit = function (params) {
    this.pageLoad(this);
}
searchResultsPageInit.prototype = jQuery.extend({}, searchResultsPageInit.prototype);

searchResultsPageInit.prototype.pageLoad = function (configObj) {
    configObj.init(configObj);
}

searchResultsPageInit.prototype.init = function (configObj) {
    configObj.searchtext = this.getUrlParameter(configObj, 's');
    configObj.filter = this.getUrlParameter(configObj, 'f');
    configObj.loading = false;

    jQuery(window).scroll(function () {
        if ((($(window).height() + $(window).scrollTop() + 200) > $('.lazyLoading').offset().top) && (!configObj.loading)) {
            configObj.lazyLoadCall(configObj);
        }
    });
}

searchResultsPageInit.prototype.lazyLoadCall = function (configObj) {
    configObj.totalcount = $('.search-filter .active span').text().replace(/["'()]/g, "");
    if (parseInt($('.resultMainContainer').children().length) < configObj.totalcount) {
        configObj.loading = true;
        $('.resultMainContainer').fadeIn();
        $('.lazyLoading').addClass('loading');
        configObj.lastPostFunc(configObj);
    }
}

searchResultsPageInit.prototype.lastPostFunc = function (configObj) {
    //send a query to server side to present new content
    if (configObj.loading) {
        configObj.skip = jQuery('.resultMainContainer').children().length;
        $.ajax({
            type: "POST",
            url: "/SearchResults/GetResults",
            data: "{'searchtext':'" + configObj.searchtext + "'," + "'skip':'" + configObj.skip + "'," + "'filter':'" + configObj.filter + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    if (data.d != "") {
                        $('.resultMainContainer').append(data.d.replace(/\u00ae/g, "<sup>&reg;</sup>"));
                    }
                }
                setTimeout(function () {
                    $('.resultMainContainer').fadeIn('fast');
                    $('.lazyLoading').removeClass('loading');
                    configObj.loading = false;
                }, 300);
            },
            error: function (n) {
                console.log(n);
            }
        });
    }
}

searchResultsPageInit.prototype.getUrlParameter = function (configObj, name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

function showProductModal(id, event) {
    var productListObj = (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0 ? JSON.parse(window.localStorage.getItem('Bucket')) : null);
    var flagData = false;
    var ProductlistElement = $('#searchModal');
    ProductlistElement.empty();
    var sampleAvailable = '', text = '', altText = '';
    if (productListObj) {
        for (var keys in productListObj) {
            var fileImageIndex = (productListObj[keys].GDIProductImgSM).lastIndexOf("~") + 1;
            if (id == productListObj[keys]['GDIProductCode']) {
                var prodStr = "";
                flagData = true;
                if (productListObj[keys].GDIProductSampleAvail) {
                    sampleAvailable = '<span class="bar sample-bar">Sample is Available</span>';
                    text = "Sample is Available";
                } else {
                    sampleAvailable = '<span class="bar info-bar">Information is Available</span>';
                    text = "Information is Available";
                }
                altText = productListObj[keys].GDIProductName + ', ' + productListObj[keys].GDIProductCode + ', ' + text;
                var productImageName = productListObj[keys].GDIProductImgSM.substr(fileImageIndex);
                prodStr += '<button class="close-button " data-close="" aria-label="Close modal" type="button"><span aria-hidden="true">X</span><span class="show-for-medium">close</span></button>';
                prodStr += '<div class="product-display searched-product" id="productmodal">';
                prodStr += '<div class="grid-x grid-padding-x">';
                prodStr += '<div class="cell small-4 text-center"><div class="relative">' + sampleAvailable + '<img src="' + productImageName + '" alt="' + altText + '" /></div></div>';
                prodStr += '<div class="cell small-8"><h4>' + productListObj[keys].GDIProductDisplayName + '</h4><p>' + productListObj[keys].GDIProductDescription + '</p><p>' + productListObj[keys].GDIProductCode + '</p></div></div>';
                prodStr += '</div>';
                prodStr += '<div class="content-footer"><button class="button blackBtn" data-close type="button" onclick="productRemoveRow(' + keys + ', event)">REMOVE</button></div>';

                ProductlistElement.append(prodStr);
            }
        }
    }
    if (!flagData) {
        $.ajax({
            type: "GET",
            url: "/api/gdiproduct/" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != '') {
                    $('#searchModal').empty();
                    productListObj = data;
                    var obj = data;
                    /*for (var keys in productListObj) {*/
                        var fileImageIndex = (data.GDIProductImgSM).lastIndexOf("~") + 1;
                        var prodStr = "";
                        if (data.GDIProductSampleAvail) {
                            sampleAvailable = '<span class="bar sample-bar">Sample is Available</span>';
                            text = "Sample is Available";
                        } else {
                            sampleAvailable = '<span class="bar info-bar">Information is Available</span>';
                            text = "Information is Available";
                        }
                        altText = data.GDIProductName + ', ' + data.GDIProductCode + ', ' + text;
                        var productImageName = data.GDIProductImgSM.substr(fileImageIndex);
                        prodStr += '<button class="close-button " data-close="" aria-label="Close modal" type="button"><span aria-hidden="true">X</span><span class="show-for-medium">close</span></button>';
                        prodStr += '<div class="product-display searched-product" id="productmodal">';
                        prodStr += '<div class="grid-x grid-padding-x">';
                        prodStr += '<div class="cell small-4 text-center"><div class="relative">' + sampleAvailable + '<img src="' + productImageName + '" alt="' + altText + '" /></div></div>';
                        prodStr += '<div class="cell small-8"><h4>' + data.GDIProductDisplayName + '</h4><p>' + data.GDIProductDescription + '</p><p>' + data.GDIProductCode + '</p></div></div>';
                        prodStr += '</div>';
                        prodStr += '<div class="content-footer"><button class="button blackBtn save-product" data-close type="button">SAVE TO CART</button></div>';
                   ProductlistElement.append(prodStr);
                  /*  }*/
                    $('.save-product').on('click', function () {
                        productAdd(obj);
                    });
                }
            },
            error: function (n) {
                console.log(n);
            }
        });
    }

    element = event.currentTarget;
    console.log(event);

}

var element;
$('#searchModal').on('open.zf.reveal', function (event) {
    console.log('open');
});

$('#searchModal').on('closed.zf.reveal', function (event) {
    setTimeout(function (e) {
        $('[data-open="searchModal"]').blur();
        $('html,body').scrollTop($(element).offset().top - 500);
    }, 10);
    console.log('closed');
});


function productRemoveRow(index, event) {
    var updatecartList = JSON.parse(window.localStorage.getItem('Bucket'));
    updatecartList.splice(index, 1);
    localStorage.setItem('Bucket', JSON.stringify(updatecartList));
    updatecartvalue();
}

function productAdd(product) {
    var productList = (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0 ? JSON.parse(window.localStorage.getItem('Bucket')) : []);
    productList.push(product);
    window.localStorage.setItem('Bucket', JSON.stringify(productList));
    console.log('product added');
    updatecartvalue();

}

new searchResultsPageInit();