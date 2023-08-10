$(document).ready(function () {
    $('article.Specialty-powder-form-A').css('display', 'block');
    $('.Specialty-powder-form-B').css('display', 'none');
    $('.Specialty-thank-you-page').css('display', 'none');
    $('.nextButton').click(function () {
        $('#description').val('');
        let productReqList = JSON.parse(window.localStorage.getItem('Bucket'));
        let reqstr = "";
        for (let keys in productReqList) {
            reqstr += productReqList[keys].GDIProductName + ' - ' + productReqList[keys].GDIProductCode + ';';
        }
        reqstr = reqstr.slice(0, -1);
        $('#description').val(reqstr);
        $('article.Specialty-powder-form-A').css('display', 'none');
        $('article.Specialty-powder-form-B').css('display', 'block');
        let top = $('article').position().top;
        $('html, body').animate({
            scrollTop: top
        }, 800);
    });
});

/*===Specialty powders request form=== */
$(function () {
    powdersRequestformList();
    $('#mycart').click(function () {
        powdersRequestformList();
    });
});

function powdersRequestformList() {
    let ProductlistElement = $('#ProductList');
    ProductlistElement.empty();
    $('.Specialty-powder-form-A #no-products-template').hide();
    $('.Specialty-powder-form-A #product-request').hide();
    let productListObj = JSON.parse(window.localStorage.getItem('Bucket'));
    let prodStr = "";
    let sampleAvailable = '', text = '', altText = '';
    if (productListObj != null) {
        if (productListObj.length > 0) {
            $('.Specialty-powder-form-A #product-request').show();
            for (let keys in productListObj) {
                let fileImageIndex = (productListObj[keys].GDIProductImgSM).lastIndexOf("~") + 1;
                let className = (productListObj[keys]['CheesePowderColor']) ? productListObj[keys]['CheesePowderColor'].toLowerCase().replace(/-/g, ' ') : '';
                if (productListObj[keys].GDIProductSampleAvail) {
                    sampleAvailable = '<span class="bar sample-bar">Sample is Available</span>';
                    text = "Sample is Available";
                } else {
                    sampleAvailable = '<span class="bar info-bar">Information is Available</span>';
                    text = "Information is Available";
                }
                let productImageName = window.location.origin + productListObj[keys].GDIProductImgSM.substr(fileImageIndex);
                prodStr += '<div class="cell"><div class="product-card grid-x grid-margin-x"><div class="cell small-4 medium-3"><div class="relative text-center">' + sampleAvailable + '<img src="' + productImageName + '" alt="' + altText + '" /></div></div>';
                prodStr += '<div class="cell small-8 medium-6"><div class="grid-x"><div class="cell ' + className + '"><h3>' + productListObj[keys].GDIProductDisplayName + '</h3>';
                prodStr += '<p class="product-id"><span>' + productListObj[keys].GDIProductCode + '</span></p> </div></div></div>';
                prodStr += '<div class="cell small-12 medium-3 product-footer text-right align-self-bottom"><a href="javascript:void(0)" class="save" role="button" onclick="productRemoveRow(' + keys + ', event)">remove</a>';
                prodStr += '</div></div></div>'
                // products.push(data[keys]);
            }
            ProductlistElement.append(prodStr);
        }

        else {
            $('.Specialty-powder-form-A #no-products-template').show();
        }
    }
    else {
        $('.Specialty-powder-form-A #no-products-template').show();
    }
}

function productRemoveRow(index, event) {
    let updatecartList = JSON.parse(window.localStorage.getItem('Bucket'));
    updatecartList.splice(index, 1);
    localStorage.setItem('Bucket', JSON.stringify(updatecartList));
    if (updatecartList.length > 0) {
        $('.Specialty-powder-form-A #product-request').show();
        $('.Specialty-powder-form-A #no-products-template').hide();
    }
    else {
        $('.Specialty-powder-form-A #product-request').hide();
        $('.Specialty-powder-form-A #no-products-template').show();
    }
    powdersRequestformList();
    updatecartvalue();
}

function OnSuccesSpecialtyProductRequestForm() {
    Success();
}
function OnCompleteSpecialtyProductRequestForm() {
    Success();
}
function OnFailureSpecialtyProductRequestForm() {
    $('#frmSpeialityProductRequest').trigger("reset");
    $('article.Specialty-powder-form-B').css('display', 'block');
    $('article.Specialty-thank-you-page').css('display', 'none');
}
function Success() {
    $('article.Specialty-powder-form-B').css('display', 'none');
    $('article.Specialty-thank-you-page').css('display', 'block');
    let top = $('article').position().top;
    $('html, body').animate({ scrollTop: top }, 800);
    resetCartValue();
}