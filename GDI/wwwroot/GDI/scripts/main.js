$(function () {
    $(document).foundation();
    $('.search-open-mobile').click(function () {
        $('.nav-mobile-shadow').show();
        $('.search-panal').addClass('open-mobile-search');
    })
    $('.search-close').click(function () {
        $('.search-panal').removeClass('open-mobile-search');
        $('.nav-mobile-shadow').hide();
    });

    $('.hamburgerMenu').click(function () {
        var body = $('body');
        if (body.hasClass('mobile-menu-open')) {
            body.removeClass('mobile-menu-open');
            $('.top-menu').removeClass('navopen');
            $('.nav-mobile-shadow').hide();

        } else {
            body.addClass('mobile-menu-open');
            $('.top-menu').addClass('navopen');
            $('.nav-mobile-shadow').show();
        }
    });

    $('.list-view').click(function () {
        $(this).addClass('active');
        $('.grid-view').removeClass('active');
        $('.list').removeClass('products-grid');
        $('.list').addClass('products-list');
        setTimeout(function () {
            new Foundation.reInit(["equalizer"]);
        }, 500);
    });

    $('.grid-view').click(function () {

        $(this).addClass('active');
        $('.list-view').removeClass('active');
        $('.list').removeClass('products-list');
        $('.list').addClass('products-grid');
        setTimeout(function () {
            AOS.init();
            new Foundation.reInit(["equalizer"]);
        }, 500);
    });


    $('.reuqestPageForm').on('click', function () {
        window.location.href = window.location.origin + $('#hdnproductrequesturl').val();
    });

    $('.speciality').on('click', function () {
        window.location.href = window.location.origin + $('#hdnspecialtypowderurl').val();
    });

    $('#mycart, #vue-cart').click(function () {
        var cartvalue = (localStorage.getItem('Bucket')) ? JSON.parse(localStorage.getItem('Bucket')).length : 0;
        $('.cart-value').text(cartvalue);
        var noProductElement = $('#no-product');
        if (cartvalue > 0) {
            ShowProductList();
            noProductElement.addClass('hide').siblings().removeClass('hide');
        }
        else {
            noProductElement.removeClass('hide').siblings('#cart-list').addClass('hide');
        }
        $('#cart').foundation('open');
        $(this).trigger('reloadProducts');
    });

    updatecartvalue();


});

function ShowProductList() {
    $('.productList').empty();
    var productListObj = JSON.parse(window.localStorage.getItem('Bucket'));
    var str = "", sampleAvailable = '', text = '', altText = '';
    for (var keys in productListObj) {
        if (productListObj[keys].GDIProductSampleAvail) {
            sampleAvailable = '<span class="bar sample-bar">Sample is Available</span>';
            text = "Sample is Available";
        } else {
            sampleAvailable = '<span class="bar info-bar">Information is Available</span>';
            text = "Information is Available";
        }
        altText = productListObj[keys].GDIProductName + ', ' + productListObj[keys].GDIProductCode + ', ' + text;
        console.log('alt', altText);

        str += '<div class="grid-x grid-margin-x align-middle grid-margin-y"><div class="cell small-4 medium-3"><div class="relative">'//+ sampleAvailable 
        str += '<img src=' + productListObj[keys].GDIProductImgSM.replace('~', '') + ' alt="' + altText + '"/></div></div>';
        str += '<div class="cell small-8 medium-6"><h4>' + productListObj[keys].GDIProductDisplayName + '</h4>';
        str += '</div><div class="cell small-12 medium-3"><button class="button blackBtn small" onclick="removeProductRow(' + keys + ', event)"> Remove </button></div>';
        str += '</div></div>'

    }
    $('.productList').append(str);
}

function updatecartvalue() {
    var cartItems = (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0) ? JSON.parse(window.localStorage.getItem('Bucket')).length : 0;
    $('.cart-value').text(cartItems);
}


function removeProductRow(index, event) {
    $('.cart-list').removeClass('hide').siblings('.no-product').addClass('hide');
    var updatecartList = JSON.parse(window.localStorage.getItem('Bucket'));
    var product = updatecartList.splice(index, 1);

    localStorage.setItem('Bucket', JSON.stringify(updatecartList));
    updatecartvalue();
    ShowProductList();

    if (JSON.parse(window.localStorage.getItem('Bucket')).length > 0) {
        $('#cart-list').removeClass('hide').siblings('.no-product').addClass('hide');
    }
    else {
        $('#cart-list').addClass('hide').siblings().removeClass('hide');
    }

    $('.productList').trigger('reloadProducts', product);
}

function resetCartValue() {
    //window.localStorage.clear();
    var cartvalue = (localStorage.getItem('Bucket')) ? JSON.parse(localStorage.getItem('Bucket')).length : 0;
    $('.cart-value').text(cartvalue);
}

$(document).ready(function () {
    new GDIPageInit();
});
var GDIPageInit = function (params) {
    this.pageInit(this);
}

GDIPageInit.prototype.pageInit = function (configObj) {

    var searchValue = null;
    jQuery(window).on("keypress", function (e) {
        if ((jQuery(e.target).attr('id') == 'searchPageInput' || jQuery(e.target).attr('class') == 'input-search') && e.keyCode === 13) {
            if (jQuery('#' + jQuery(e.target).attr('id')).val() != undefined
                && jQuery('#' + jQuery(e.target).attr('id')).val() != ''
                && jQuery('#' + jQuery(e.target).attr('id')).val() != configObj.prevValue) {
                searchValue = jQuery('#' + jQuery(e.target).attr('id')).val();
                configObj.searchValue = searchValue.replace(/[<>]/g, '');
                configObj.searchSelector = '#' + jQuery(e.target).attr('id');
                configObj.searchModule(configObj);

            }
            e.preventDefault();
        }
    });
    $(".gdi-search").click(function () {
        searchValue = null;
        var element = jQuery(this).parent('a').siblings('input');
        if (jQuery(element).val() != '' && jQuery(element).val() != configObj.prevValue) {
            searchValue = jQuery(element).val();
            configObj.searchValue = searchValue.replace(/[<>]/g, '');
            configObj.searchSelector = element;
            configObj.searchModule(configObj);
        }
    });
}

GDIPageInit.prototype.searchModule = function (configObj) {
    if (!configObj.facetSelector) {
        configObj.facetSelector = "All";
    }
    configObj.urlArr = window.location.href.split("/");
    configObj.prevValue = jQuery(configObj.searchSelector).val();

    window.location.href = configObj.urlArr[0] + "//" + configObj.urlArr[2] + "/search/?s=" + encodeURIComponent(configObj.searchValue) + "&f=" + encodeURIComponent(configObj.facetSelector);
}
// Back button was clicked
// Perform your desired action here
window.onpageshow = function (event) {
    if (event.persisted) {
        window.location.reload(); // Reload the page
    }
};