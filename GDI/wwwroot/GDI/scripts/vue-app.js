Array.prototype.includes || Object.defineProperty(Array.prototype, "includes", {
    value: function (n, t) {
        function e(n, t) {
            return n === t || typeof n == "number" && typeof t == "number" && isNaN(n) && isNaN(t)
        }
        var f, i, r, u;
        if (this == null)
            throw new TypeError('"this" is null or not defined');
        if (f = Object(this),
            i = f.length >>> 0,
            i === 0)
            return !1;
        for (r = t | 0,
            u = Math.max(r >= 0 ? r : i - Math.abs(r), 0); u < i;) {
            if (e(f[u], n))
                return !0;
            u++
        }
        return !1
    }
});
"use strict";
var app = new Vue({
    el: "#vue-app",
    data: {
        clearFilters: !1,
        counter: 15,
        currentProduct: null,
        currentModalFlag: window.sessionStorage.getItem("Current Modal Flag") ? JSON.parse(window.sessionStorage.getItem("Current Modal Flag")) : !1,
        domain: "",
        filtersList: [],
        filtersObject: {},
        filterChipps: [],
        loadButton: null,
        mobileFilter: !1,
        noProducts: !1,
        origin: "",
        products: [],
        path: "",
        productsLength: "",
        sampleCheese: JSON.parse(window.localStorage.getItem("cheese")) || !1,
        sampleDairy: JSON.parse(window.localStorage.getItem("dairy")) || !1,
        sampleSeasoning: JSON.parse(window.localStorage.getItem("seasoning")) || !1,
        selectedProductArray: window.localStorage.getItem("Bucket") && JSON.parse(window.localStorage.getItem("Bucket")).length > 0 ? JSON.parse(window.localStorage.getItem("Bucket")) : [],
        selectedProductArrayLength: window.localStorage.getItem("Bucket") ? JSON.parse(window.localStorage.getItem("Bucket")).length : 0,
        temp: []
    },
    mounted: function () {
        var n = this;
        axios.get("/api/FetchProductList?param=" + this.path).then(function (t) {
            n.noProducts = t.data.length ? !1 : !0;
            n.products = t.data;
            n.productsLength = n.products.length;
            n.loadButton = n.productsLength > 15 ? !0 : !1
        });
        axios.get("/api/FetchFilterJson?param=" + this.path).then(function (t) {
            n.filtersList = t.data;
            n.temp = t.data;
            for (var i = 0; i < n.temp.length; i++)
                app.$set(app.filtersObject, n.temp[i].FilterFiledName, []);
            localStorage.getItem("selectedFilters") && n.isFilterExists(n.origin)
        })
    },
    created: function () {
        this.domain = window.location.origin;
        this.path = window.location.pathname;
        this.path = this.path.split("/");
        this.path = this.origin = this.path[this.path.length - 2];
        this.path = this.path.split("-").join(" ");
        window.localStorage.setItem("selectedFilters", JSON.stringify(JSON.parse(window.localStorage.getItem("selectedFilters")) || {}))
    },
    computed: {
        filterProducts: function () {
            var n = this.products
                , t = this;
            this.origin === "cheese-powders" && (this.sampleCheese == !0 ? (this.clearFilters = !0,
                window.localStorage.setItem("cheese", !0),
                n = n.filter(function (n) {
                    return n.GDIProductSampleAvail
                })) : window.localStorage.setItem("cheese", this.sampleCheese));
            this.origin === "seasoning-blends" && (this.sampleSeasoning == !0 ? (this.clearFilters = !0,
                window.localStorage.setItem("seasoning", !0),
                n = n.filter(function (n) {
                    return n.GDIProductSampleAvail
                })) : window.localStorage.setItem("seasoning", this.sampleSeasoning));
            this.origin === "dairy-powders" && (this.sampleDairy == !0 ? (this.clearFilters = !0,
                window.localStorage.setItem("dairy", !0),
                n = n.filter(function (n) {
                    return n.GDIProductSampleAvail
                })) : window.localStorage.setItem("dairy", this.sampleDairy));
            for (let i in this.filtersObject)
                this.filtersObject[i] && this.filtersObject[i].length && (i === "CheesePowderType" || i === "DairyPowderType" || i === "SeasoningBlendType") ? n = n.filter(function (n) {
                    return n[i] === t.filtersObject[i]
                }) : this.filtersObject[i].length > 0 && (n = i === "GDIProductClaims" || i === "CheesePowderFavorTones" || i === "CheesePowderCheddarIntensity" || i === "CheesePowderVarietalFlavor" || i === "CheesePowderTaste" || i === "SeasoningBlendFlavor" || i === "SeasoningBlendTaste" || i === "SeasoningBlendSmokyTone" || i === "SeasoningBlendSpicyFlavor" || i === "DairyPowderFlavors" || i === "DairyPowderTate" ? n.filter(function (n) {
                    return t.filtersObject[i].includes(app.filterPipes(t.filtersObject[i], n[i]))
                }) : n.filter(function (n) {
                    return t.filtersObject[i].includes(n[i])
                }));
            return n.map(function (n) {
                return t.isExists(n)
            }),
                this.productsLength = n.length,
                this.productsLength > 15 ? (this.counter = 15,
                    this.loadButton = !0) : this.loadButton = !1,
                this.noProducts = n.length ? !1 : !0,
                n
        },
        ChippsFiltering: function () {
            var t, n, i;
            this.origin === "cheese-powders" && (this.filterChipps = this.sampleCheese == !0 ? [{
                value: "Sample is Available",
                category: "GDIProductSampleAvail"
            }] : []);
            this.origin === "seasoning-blends" && (this.filterChipps = this.sampleSeasoning == !0 ? [{
                value: "Sample is Available",
                category: "GDIProductSampleAvail"
            }] : []);
            this.origin === "dairy-powders" && (this.filterChipps = this.sampleDairy == !0 ? [{
                value: "Sample is Available",
                category: "GDIProductSampleAvail"
            }] : []);
            t = {
                value: "",
                category: ""
            };
            for (n in this.filtersObject)
                if (this.filtersObject[n].length > 0 && (n === "CheesePowderType" || n === "DairyPowderType" || n === "SeasoningBlendType"))
                    t.value = this.filtersObject[n],
                        t.category = n,
                        this.filterChipps.push(t);
                else if (this.filtersObject[n].length > 0)
                    for (i = 0; i < this.filtersObject[n].length; i++)
                        t = {
                            value: "",
                            category: ""
                        },
                            t.value = this.filtersObject[n][i],
                            t.category = n,
                            this.filterChipps.push(t);
            return this.clearFilters = this.filterChipps.length > 0 ? !0 : !1,
                console.log("this.filterChipps", this.filterChipps),
                this.filterChipps
        }
    },
    updated: function () {
        $("#mycart").on("reloadProducts", function () {
            app.selectedProductArray = window.localStorage.getItem("Bucket") && JSON.parse(window.localStorage.getItem("Bucket")).length > 0 ? JSON.parse(window.localStorage.getItem("Bucket")) : [];
            app.selectedProductArrayLength = JSON.parse(window.localStorage.getItem("Bucket")).length
        });
        $(".productList").on("reloadProducts", function (n, t) {
            app.removeProduct("", t)
        });
        var filterAccordions = $('.accordion')
        filterAccordions = new Foundation.Accordion($('.accordion'), {});
        //new Foundation.reInit($('.equalizer'))
        //new Foundation.reInit($('.accordion'))
        /*  new Foundation.reInit($('.equalizer'), $('.accordion'))*/
    },
    methods: {
        showMore: function () {
            this.counter = this.counter + 15;
            this.counter > this.productsLength && (this.counter = this.productsLength,
                this.loadButton = !1);
            setTimeout(function () {
                new Foundation.reInit($('[data-equalizer]'))
            }, 100)
        },
        reinitequ: function () {
            setTimeout(function () {
                Foundation.reInit($('[data-equalizer]'));
                console.log("helloworld");
            }, 500)
        },

        saveProduct: function (n, t) {
            t.isSaved = !0;
            this.currentProduct = t;
            this.selectedProductArray = window.localStorage.getItem("Bucket") && JSON.parse(window.localStorage.getItem("Bucket")).length > 0 ? JSON.parse(window.localStorage.getItem("Bucket")) : [];
            this.selectedProductArray.push(t);
            window.localStorage.setItem("Bucket", JSON.stringify(this.selectedProductArray));
            this.selectedProductArrayLength = this.selectedProductArray.length;
            window.localStorage.setItem("Current Product", JSON.stringify(this.currentProduct));
            this.currentModalFlag || $("#currentModal").foundation("open");
            //console.log("Product added", t);
            $(".cart-value").text(this.selectedProductArray.length)

        },
        removeProduct: function (n, t) {
            t.isSaved = !1;
            this.selectedProductArray = window.localStorage.getItem("Bucket") && JSON.parse(window.localStorage.getItem("Bucket")).length > 0 ? JSON.parse(window.localStorage.getItem("Bucket")) : [];
            this.selectedProductArray = this.selectedProductArray.filter(function (n) {
                return n.GDIProductItemOrder !== t.GDIProductItemOrder
            });
            window.localStorage.setItem("Bucket", JSON.stringify(this.selectedProductArray));
            this.selectedProductArrayLength = this.selectedProductArray.length;
            console.log("Product Removed", t);
            $(".cart-value").text(this.selectedProductArray.length)
            t.isSaved = !1;
            if (this.selectedProductArray.length === 0) {
                window.location.reload();
            }
        },
        isFilterExists: function (n) {
            var i = JSON.parse(window.localStorage.getItem("selectedFilters")), t, r;
            for (t in i)
                if (t === n)
                    for (r in i[t])
                        this.filtersObject[r] = i[t][r];
                else
                    continue;
            console.log("FILTER EXISTS", this.filtersObject)
        },
        flagset: function () {
            this.currentModalFlag != !0 && (this.currentModalFlag = !0,
                window.sessionStorage.setItem("Current Modal Flag", this.currentModalFlag))
        },
        viewCart: function () {
            $("#mycart").trigger("click")
        },
        filtersRemove: function (n, t) {
            if (n.category === "GDIProductSampleAvail") {
                this.origin === "cheese-powders" && (this.sampleCheese = !1);
                this.origin === "seasoning-blends" && (this.sampleSeasoning = !1);
                this.origin === "dairy-powders" && (this.sampleDairy = !1);
                this.filterChipps.splice(t, 1);
                console.log("CHIP FILTER REMOVED", this.filterChipps);
                return
            }
            n.category === "CheesePowderType" || n.category === "DairyPowderType" || n.category === "SeasoningBlendType" ? this.filtersObject[n.category] = "" : this.filtersObject[n.category].length > 0 && (this.filtersObject[n.category] = this.filtersObject[n.category].filter(function (t) {
                return t != n.value
            }));
            this.filterChipps.splice(t, 1);
            this.updateFilters();
            console.log("CHIP FILTER REMOVED", this.filterChipps)
        },
        isExists: function (n) {
            if (this.selectedProductArray.length)
                for (var t = 0; t < this.selectedProductArray.length; t++) {
                    if (this.selectedProductArray[t].GDIProductCode === n.GDIProductCode)
                        return n.isSaved = !0,
                            n;
                    n.isSaved = !1
                }
            else
                return n
        },
        updateFilters: function () {
            console.log("origin", this.origin);
            var n = window.localStorage.getItem("selectedFilters") ? JSON.parse(window.localStorage.getItem("selectedFilters")) : {};
            this.origin === "cheese-powders" && (n["cheese-powders"] = this.filtersObject,
                window.localStorage.setItem("selectedFilters", JSON.stringify(n)));
            this.origin === "seasoning-blends" && (n["seasoning-blends"] = this.filtersObject,
                window.localStorage.setItem("selectedFilters", JSON.stringify(n)));
            this.origin === "dairy-powders" && (n["dairy-powders"] = this.filtersObject,
                window.localStorage.setItem("selectedFilters", JSON.stringify(n)));
            setTimeout(function () {
                new Foundation.reInit($('[data-equalizer]'))
            }, 500)
        },
        filterPipes: function (n, t) {
            if (t) {
                var r = ""
                    , u = t.split("|");
                for (i = 0; i < n.length; i++)
                    if (u.includes(n[i])) {
                        r = n[i];
                        break
                    }
                return r
            }
            return
        },
        clearPageFilters: function () {
            if (this.clearFilters) {
                var n = window.localStorage.getItem("selectedFilters") ? JSON.parse(window.localStorage.getItem("selectedFilters")) : {};
                if (this.origin === "cheese-powders") {
                    this.clearFilters = !1;
                    this.sampleCheese = !1;
                    for (key in n["cheese-powders"])
                        n["cheese-powders"][key] = [];
                    this.filtersObject = n["cheese-powders"];
                    window.localStorage.setItem("selectedFilters", JSON.stringify(n));
                    window.localStorage.setItem("cheese", this.sampleCheese)
                }
                if (this.origin === "seasoning-blends") {
                    this.clearFilters = !1;
                    this.sampleSeasoning = !1;
                    for (key in n["seasoning-blends"])
                        n["seasoning-blends"][key] = [];
                    this.filtersObject = n["seasoning-blends"];
                    window.localStorage.setItem("selectedFilters", JSON.stringify(n));
                    window.localStorage.setItem("seasoning", this.sampleSeasoning)
                }
                if (this.origin === "dairy-powders") {
                    this.clearFilters = !1;
                    this.sampleDairy = !1;
                    for (key in n["dairy-powders"])
                        n["dairy-powders"][key] = [];
                    this.filtersObject = n["dairy-powders"];
                    window.localStorage.setItem("selectedFilters", JSON.stringify(n));
                    window.localStorage.setItem("dairy", this.sampleDairy)
                }
                setTimeout(function () {
                    new Foundation.reInit($('[data-equalizer]'))
                }, 500)
            }
        }
    },
    filters: {
        AbsoluteUrl: function (n) {
            var t = "";
            if (n)
                return t = n.split("~")[1],
                    window.location.origin + t
        }
    }
});
AOS.init();
$(document).ready(function () {
    setTimeout(function () {
        new Foundation.reInit($('[data-equalizer]'))
    }, 100);
    function n() {
        var n = $(window).width();
        n < 1024 ? ($(".list").removeClass("products-list"),
            $(".list").addClass("products-grid"),
            $(".list-view").removeClass("active"),
            $(".grid-view").addClass("active")) : ($(".list").removeClass("products-grid"),
                $(".list").addClass("products-list"),
                $(".grid-view").removeClass("active"),
                $(".list-view").addClass("active"))
    }
    n();
    $(window).resize(n)

    ////Equal heights
    //$('.grid-view-wrapper').click(function () {
    //    Foundation.reInit($('[data-equalizer]'));
    //});
    ////Tab and mobile overlapping fix
    $('.hide-for-large button').click(function () {
        setTimeout(function () {
            Foundation.reInit($('[data-equalizer]'));
        }, 500)
    });
    if ($(window).width() < 1400) {
        setTimeout(function () {
            new Foundation.reInit($('[data-equalizer]'))
        }, 500)
    }

})
// for cart count error 
window.addEventListener('load', function () {
    setTimeout(function () {
        var spanElement = document.querySelector('.cart-items');
        var chevronElement = document.querySelector('.cart-products');
        spanElement.style.display = 'inline-block';
        chevronElement.style.display = 'inline-block';
    }, 5);
});