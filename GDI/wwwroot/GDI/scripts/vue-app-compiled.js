// https://tc39.github.io/ecma262/#sec-array.prototype.includes

/**-------------------------------------- **/
/** Js Include Function Polyfill for IE11 **/
/**-------------------------------------- **/

if (!Array.prototype.includes) {
    Object.defineProperty(Array.prototype, 'includes', {
        value: function (searchElement, fromIndex) {

            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            // 1. Let O be ? ToObject(this value).
            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If len is 0, return false.
            if (len === 0) {
                return false;
            }

            // 4. Let n be ? ToInteger(fromIndex).
            //    (If fromIndex is undefined, this step produces the value 0.)
            var n = fromIndex | 0;

            // 5. If n ≥ 0, then
            //  a. Let k be n.
            // 6. Else n < 0,
            //  a. Let k be len + n.
            //  b. If k < 0, let k be 0.
            var k = Math.max(n >= 0 ? n : len - Math.abs(n), 0);

            function sameValueZero(x, y) {
                return x === y || (typeof x === 'number' && typeof y === 'number' && isNaN(x) && isNaN(y));
            }

            // 7. Repeat, while k < len
            while (k < len) {
                // a. Let elementK be the result of ? Get(O, ! ToString(k)).
                // b. If SameValueZero(searchElement, elementK) is true, return true.
                if (sameValueZero(o[k], searchElement)) {
                    return true;
                }
                // c. Increase k by 1. 
                k++;
            }

            // 8. Return false
            return false;
        }
    });
}


/**-------------------------------------- **/
/** VUE APP INSTANCE **/
/**-------------------------------------- **/

"use strict";
var app = new Vue({
    el: '#vue-app',
    data: {
        clearFilters: false,
        counter: 15,
        currentProduct: null,
        currentModalFlag: (window.sessionStorage.getItem("Current Modal Flag")) ? JSON.parse(window.sessionStorage.getItem("Current Modal Flag")) : false,
        domain: '',
        filtersList: [],
        filtersObject: {},
        filterChipps: [],
        loadButton: null,
        mobileFilter: false,
        noProducts: false,
        origin: '',
        products: [],
        path: '',
        productsLength: '',
        sampleCheese: JSON.parse(window.localStorage.getItem('cheese')) || false,
        sampleDairy: JSON.parse(window.localStorage.getItem('dairy')) || false,
        sampleSeasoning: JSON.parse(window.localStorage.getItem('seasoning')) || false,
        selectedProductArray: (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0) ? JSON.parse(window.localStorage.getItem('Bucket')) : [],
        selectedProductArrayLength: (window.localStorage.getItem('Bucket')) ? JSON.parse(window.localStorage.getItem('Bucket')).length : 0,
        temp: [],
    },

    mounted: function mounted() {
        var _this = this; 
        var lastPart = window.location.href.split("/");
        var part = lastPart[4];
        if (part == 'cheese-powders') {
            /** Products API call **/
            axios
                .get('/api/FetchProductList?param=cheese%20powders')
                .then(function (response) {
                    var results = JSON.parse(response.data.value);

                    /* alert(results.GDIProducts.length);*/

                    (!results.length) ? _this.noProducts = true : _this.noProducts = false;
                    _this.products = results;
                    _this.productsLength = _this.products.length;
                    (_this.productsLength > 15) ? _this.loadButton = true : _this.loadButton = false;
                });

            /** Filters call **/
            axios
                .get('/api/FetchFilterJson?param=cheese%20powders')
                .then(function (response) {
                    var result = JSON.parse(response.data.value);
                    _this.filtersList = result.Filters;
                    _this.temp = result;
                    for (var i = 0; i < result.Filters; i++) {
                        app.$set(app.filtersObject, result.Filters[i]['FilterFiledName'], []);
                    }
                    if (localStorage.getItem('selectedFilters')) {
                        _this.isFilterExists(_this.origin);
                    }
                });
        }
        else if (part == 'seasoning-blends') {
            /** Products API call **/
            axios
                .get('/api/FetchProductList?param=seasoning%20blends')
                .then(function (response) {
                    var results = JSON.parse(response.data.value);

                    /* alert(results.GDIProducts.length);*/

                    (!results.length) ? _this.noProducts = true : _this.noProducts = false;
                    _this.products = results;
                    _this.productsLength = _this.products.length;
                    (_this.productsLength > 15) ? _this.loadButton = true : _this.loadButton = false;
                });

            /** Filters call **/
            axios
                .get('/api/FetchFilterJson?param=seasoning%20blends')
                .then(function (response) {
                    var result = JSON.parse(response.data.value);
                    _this.filtersList = result.Filters;
                    _this.temp = result;
                    for (var i = 0; i < result.Filters; i++) {
                        app.$set(app.filtersObject, result.Filters[i]['FilterFiledName'], []);
                    }
                    if (localStorage.getItem('selectedFilters')) {
                        _this.isFilterExists(_this.origin);
                    }
                });
        }
        else {
            /** Products API call **/
            axios
                .get('/api/FetchProductList?param=dairy%20powders')
                .then(function (response) {
                    var results = JSON.parse(response.data.value);

                    /* alert(results.GDIProducts.length);*/

                    (!results.length) ? _this.noProducts = true : _this.noProducts = false;
                    _this.products = results;
                    _this.productsLength = _this.products.length;
                    (_this.productsLength > 15) ? _this.loadButton = true : _this.loadButton = false;
                });

            /** Filters call **/
            axios
                .get('/api/FetchFilterJson?param=dairy%20powders')
                .then(function (response) {
                    var result = JSON.parse(response.data.value);
                    _this.filtersList = result.Filters;
                    _this.temp = result;
                    for (var i = 0; i < result.Filters; i++) {
                        app.$set(app.filtersObject, result.Filters[i]['FilterFiledName'], []);
                    }
                    if (localStorage.getItem('selectedFilters')) {
                        _this.isFilterExists(_this.origin);
                    }
                });
        }
    },

    /** Lifecycle Hook **/
    created: function created() {
        this.domain = window.location.origin;
        this.path = window.location.pathname;
        this.path = this.path.split('/');
        this.path = this.origin = this.path[this.path.length - 2];
        this.path = this.path.split('-').join(' ');

        //set filter object on localstorage
        window.localStorage.setItem('selectedFilters', JSON.stringify(JSON.parse(window.localStorage.getItem('selectedFilters')) || {}));
    },

    computed: {
        filterProducts: function filterProducts() {
            var filteredProducts = this.products;
            var _this = this;
            if (this.origin === 'cheese-powders') {
                if (this.sampleCheese == true) {
                    this.clearFilters = true;
                    window.localStorage.setItem('cheese', true);
                    filteredProducts = filteredProducts.filter(function (product) { return product['GDIProductSampleAvail'] });
                } else {
                    window.localStorage.setItem('cheese', this.sampleCheese);
                }
            }

            if (this.origin === 'seasoning-blends') {
                if (this.sampleSeasoning == true) {
                    this.clearFilters = true;
                    window.localStorage.setItem('seasoning', true);
                    filteredProducts = filteredProducts.filter(function (product) { return product['GDIProductSampleAvail'] });
                } else {
                    window.localStorage.setItem('seasoning', this.sampleSeasoning);
                }
            }

            if (this.origin === 'dairy-powders') {
                if (this.sampleDairy == false) {
                    this.clearFilters = true;
                    window.localStorage.setItem('dairy', true);
                    filteredProducts = filteredProducts.filter(function (product) { return product['GDIProductSampleAvail'] });
                } else {
                    window.localStorage.setItem('dairy', this.sampleDairy);
                }
            }

            for (let key in this.filtersObject) {
                if (this.filtersObject[key] && this.filtersObject[key].length && (key === "CheesePowderType" || key === "DairyPowderType" || key === "SeasoningBlendType")) {
                    filteredProducts = filteredProducts.filter(function (product) { return product[key] === _this.filtersObject[key] });
                } else {
                    if (this.filtersObject[key].length > 0) {
                        if (key === 'GDIProductClaims' || key === 'CheesePowderFavorTones' || key === 'CheesePowderCheddarIntensity' || key === 'CheesePowderVarietalFlavor' || key === 'CheesePowderTaste'
                            || key === 'SeasoningBlendFlavor' || key === 'SeasoningBlendTaste' || key === 'SeasoningBlendSmokyTone' || key === 'SeasoningBlendSpicyFlavor'
                            || key === 'DairyPowderFlavors' || key === 'DairyPowderTate') {
                            filteredProducts = filteredProducts.filter(function (product) { return _this.filtersObject[key].includes(app.filterPipes(_this.filtersObject[key], product[key])) });
                        } else {
                            filteredProducts = filteredProducts.filter(function (product) { return _this.filtersObject[key].includes(product[key]) });
                        }
                    }
                }
            }

            filteredProducts.map(function (product) { return _this.isExists(product) });
            //Loadmore button Handling
            this.productsLength = filteredProducts.length;
            if (this.productsLength > 15) {
                this.counter = 15;
                this.loadButton = true;
            } else {
                this.loadButton = false;
            }
            if (!filteredProducts.length) { this.noProducts = true; } else { this.noProducts = false; }
            return filteredProducts;
        },

        ChippsFiltering: function ChippsFiltering() {
            if (this.origin === 'cheese-powders') {
                if (this.sampleCheese == true) {
                    this.filterChipps = [{
                        value: 'Sample is Available',
                        category: 'GDIProductSampleAvail'
                    }];
                } else {
                    this.filterChipps = [];
                }
            }

            if (this.origin === 'seasoning-blends') {
                if (this.sampleSeasoning == true) {
                    this.filterChipps = [{
                        value: 'Sample is Available',
                        category: 'GDIProductSampleAvail'
                    }];
                } else {
                    this.filterChipps = [];
                }
            }

            if (this.origin === 'dairy-powders') {
                if (this.sampleDairy == true) {
                    this.filterChipps = [{
                        value: 'Sample is Available',
                        category: 'GDIProductSampleAvail'
                    }];
                } else {
                    this.filterChipps = [];
                }
            }

            var tempObj = {
                value: '',
                category: ''
            };
            for (var filter in this.filtersObject) {
                if (this.filtersObject[filter].length > 0 && (filter === "CheesePowderType" || filter === "DairyPowderType" || filter === "SeasoningBlendType")) {
                    tempObj.value = this.filtersObject[filter];
                    tempObj.category = filter;
                    this.filterChipps.push(tempObj);
                }
                else {
                    if (this.filtersObject[filter].length > 0) {
                        for (var i = 0; i < this.filtersObject[filter].length; i++) {
                            tempObj = {
                                value: '',
                                category: ''
                            };
                            tempObj.value = this.filtersObject[filter][i];
                            tempObj.category = filter;
                            this.filterChipps.push(tempObj);
                        }
                    }
                }
            }
            if (this.filterChipps.length > 0) {
                this.clearFilters = true;
            } else {
                this.clearFilters = false;
            }
            console.log("this.filterChipps", this.filterChipps);

            return this.filterChipps;
        }
    },
    /** Lifecyle Hook **/
    updated: function updated() {
        $('#mycart').on('reloadProducts', function () {
            app.selectedProductArray = (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0) ? JSON.parse(window.localStorage.getItem('Bucket')) : [];
            app.selectedProductArrayLength = JSON.parse(window.localStorage.getItem('Bucket')).length;
        });

        $('.productList').on('reloadProducts', function (event, product) {
            app.removeProduct('', product);
        });

       /* new Foundation.reInit(["equalizer", "accordion"]);*/
    },

    methods: {
        // Load More Show hide 
        showMore: function showMore() {
            this.counter = this.counter + 15;
            if (this.counter > this.productsLength) {
                this.counter = this.productsLength;
                this.loadButton = false;
            }
        },

        // Product Saved
        saveProduct: function saveProduct(el, product) {
            product['isSaved'] = true;
            this.currentProduct = product;
            this.selectedProductArray = (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0) ? JSON.parse(window.localStorage.getItem('Bucket')) : [];
            this.selectedProductArray.push(product);
            window.localStorage.setItem('Bucket', JSON.stringify(this.selectedProductArray));
            this.selectedProductArrayLength = this.selectedProductArray.length;
            window.localStorage.setItem('Current Product', JSON.stringify(this.currentProduct));
            if (!this.currentModalFlag) {
                $('#currentModal').foundation('open');
            }
            $('.cart-value').text(this.selectedProductArray.length);
        },

        // Product Removed 
        removeProduct: function removeProduct(el, productRemove) {
            productRemove['isSaved'] = false;
            this.selectedProductArray = (window.localStorage.getItem('Bucket') && JSON.parse(window.localStorage.getItem('Bucket')).length > 0) ? JSON.parse(window.localStorage.getItem('Bucket')) : [];
            this.selectedProductArray = this.selectedProductArray.filter(function (product) { return (product.GDIProductItemOrder !== productRemove.GDIProductItemOrder) });
            window.localStorage.setItem('Bucket', JSON.stringify(this.selectedProductArray));
            this.selectedProductArrayLength = this.selectedProductArray.length;
            console.log('Product Removed', productRemove);
            $('.cart-value').text(this.selectedProductArray.length);
        },

        // Fliter on reload
        isFilterExists: function isFilterExists(orig) {
            var selectedFilters = JSON.parse(window.localStorage.getItem('selectedFilters'));
            for (var selected in selectedFilters) {
                if (selected === orig) {
                    for (var key in selectedFilters[selected]) {
                        this.filtersObject[key] = selectedFilters[selected][key];
                    }
                } else {
                    continue;
                }
            }
            console.log('FILTER EXISTS', this.filtersObject);
        },

        // Current Product modal should be shown or not
        flagset: function flagset() {
            if (this.currentModalFlag != true) {
                this.currentModalFlag = true;
                window.sessionStorage.setItem('Current Modal Flag', this.currentModalFlag);
            }
        },

        // Trigger Cart Modal on  cart icon click
        viewCart: function viewCart() {
            $("#mycart").trigger("click");
        },

        // Filter remove
        filtersRemove: function filtersRemove(chip, index) {
            if (chip.category === "GDIProductSampleAvail") {
                if (this.origin === 'cheese-powders') {
                    this.sampleCheese = false;
                }
                if (this.origin === 'seasoning-blends') {
                    this.sampleSeasoning = false;
                }

                if (this.origin === 'dairy-powders') {
                    this.sampleDairy = false;
                }
                this.filterChipps.splice(index, 1);
                console.log('CHIP FILTER REMOVED', this.filterChipps);
                return;
            }
            if (chip.category === "CheesePowderType" || chip.category === "DairyPowderType" || chip.category === "SeasoningBlendType") {
                this.filtersObject[chip.category] = '';
            } else {
                if (this.filtersObject[chip.category].length > 0) {
                    this.filtersObject[chip.category] = this.filtersObject[chip.category].filter(function (item) { return item != chip.value });
                }
            }

            this.filterChipps.splice(index, 1);
            this.updateFilters();
            console.log('CHIP FILTER REMOVED', this.filterChipps);
        },

        // product already exists in localstorage or not.
        isExists: function isExists(product) {
            if (this.selectedProductArray.length) {
                for (var i = 0; i < this.selectedProductArray.length; i++) {
                    if (this.selectedProductArray[i]['GDIProductCode'] === product['GDIProductCode']) {
                        product['isSaved'] = true;
                        return product;
                    } else {
                        product['isSaved'] = false;
                    }
                }
            } else {
                return product;
            }
        },


        updateFilters: function updateFilters() {
            console.log('origin', this.origin);
            var temp = window.localStorage.getItem('selectedFilters') ? JSON.parse(window.localStorage.getItem('selectedFilters')) : {};
            if (this.origin === 'cheese-powders') {
                temp['cheese-powders'] = this.filtersObject;
                window.localStorage.setItem('selectedFilters', JSON.stringify(temp));
            }
            if (this.origin === 'seasoning-blends') {
                temp['seasoning-blends'] = this.filtersObject;
                window.localStorage.setItem('selectedFilters', JSON.stringify(temp));
            }
            if (this.origin === 'dairy-powders') {
                temp['dairy-powders'] = this.filtersObject;
                window.localStorage.setItem('selectedFilters', JSON.stringify(temp));
            }
            setTimeout(function () {
                new Foundation.reInit(["equalizer"]);
            }, 500);
        },

        // Filter having values pipe seperated
        filterPipes: function filterPipes(ar, str) {
            if (str) {
                var a = '';
                var strArr = str.split('|');
                for (i = 0; i < ar.length; i++) {
                    if (strArr.includes(ar[i])) {
                        a = ar[i];
                        break;
                    }
                }
                return a;
            }
            return;
        },
        // Clear page filters
        clearPageFilters: function clearPageFilters() {
            if (this.clearFilters) {
                var temp = JSON.parse(window.localStorage.getItem('selectedFilters'));
                if (this.origin === 'cheese-powders') {
                    this.clearFilters = false;
                    this.sampleCheese = false;
                    for (key in temp['cheese-powders']) {
                        temp['cheese-powders'][key] = [];
                    }
                    this.filtersObject = temp['cheese-powders'];
                    window.localStorage.setItem('selectedFilters', JSON.stringify(temp));
                    window.localStorage.setItem('cheese', this.sampleCheese);
                }
                if (this.origin === 'seasoning-blends') {
                    this.clearFilters = false;
                    this.sampleSeasoning = false;
                    for (key in temp['seasoning-blends']) {
                        temp['seasoning-blends'][key] = [];
                    }
                    this.filtersObject = temp['seasoning-blends'];
                    window.localStorage.setItem('selectedFilters', JSON.stringify(temp));
                    window.localStorage.setItem('seasoning', this.sampleSeasoning);
                }
                if (this.origin === 'dairy-powders') {
                    this.clearFilters = false;
                    this.sampleDairy = false;
                    for (key in temp['dairy-powders']) {
                        temp['dairy-powders'][key] = [];
                    }
                    this.filtersObject = temp['dairy-powders'];
                    window.localStorage.setItem('selectedFilters', JSON.stringify(temp));
                    window.localStorage.setItem('dairy', this.sampleDairy);
                }
                setTimeout(function () {
                    new Foundation.reInit(["equalizer"]);
                }, 500);
            }
        }
    },

    // Having image url
    filters: {
        AbsoluteUrl: function AbsoluteUrl(value) {
            var val = '';
            if (!value) return;
            val = value.split('~')[1];
            return window.location.origin + val;
        }
    }
});

// For Animation of Product
AOS.init();

// Grid or list View
$(document).ready(function () {
    function checkWidth() {

        //new Foundation.reInit(["equalizer"]);
        var windowsize = $(window).width();
        if (windowsize < 1024) {
            $('.list').removeClass('products-list');
            $('.list').addClass('products-grid');
            $('.list-view').removeClass('active');
            $('.grid-view').addClass('active');
        } else {
            $('.list').removeClass('products-grid');
            $('.list').addClass('products-list');
            $('.grid-view').removeClass('active');
            $('.list-view').addClass('active');
        }
    }
    // Execute on load
    checkWidth();
    // Bind event listener
    $(window).resize(checkWidth);

});
