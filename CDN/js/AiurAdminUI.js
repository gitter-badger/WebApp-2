﻿!
function (t) {
    "use strict";
    var e = {};
    NProgress.start(),
    t(document).ready(function () {
        return e.module.init(),
        e.plugin.init(),
        t('[data-toggle="tooltip"]').length && t('[data-toggle="tooltip"]').tooltip(),
        !1
    }),
    t(window).bind("load", function () {
        return e.plugin.isotope.init(),
        e.func.resizeNotice(),
        NProgress.done(),
        !1
    }),
    t(window).on("resize", function () {
        return e.func.resizeNotice(),
        e.func.getChart(),
        !1
    }),
    e.module = {
        init: function () {
            return e.module.accordion(),
            e.module.card(),
            e.module.css(t(".js__width"), "width"),
            e.module.dropDown("js__drop_down", !1),
            e.module.logout(),
            e.module.menu(),
            e.module.tab(".js__tab", "li"),
            e.module.toggle(),
            e.module.todo(),
            !1
        },
        accordion: function () {
            return t(".js__accordion").each(function () {
                var e = t(this);
                e.find(".js__control").on("click", function (n) {
                    if (n.preventDefault(), t(this).parent().hasClass("active")) t(this).parent().removeClass("active"),
                    t(this).next().stop().slideUp(400);
                    else {
                        var i = t(this);
                        e.find(".active").children(".js__content").stop().slideUp(400),
                        e.find(".active").removeClass("active"),
                        t(this).parent().addClass("active"),
                        t(this).next(".js__content").slideDown(400, function () {
                            e.parents(".main-menu").length && t(".main-menu .content").mCustomScrollbar("scrollTo", i, {
                                timeout: 0,
                                scrollInertia: 0
                            })
                        })
                    }
                })
            }),
            !1
        },
        card: function () {
            return t(".js__card").each(function () {
                var e = t(this);
                e.on("click", ".js__card_minus", function () {
                    e.toggleClass("card-closed"),
                    e.find(".js__card_content").stop().slideToggle(400)
                }),
                e.on("click", ".js__card_remove", function () {
                    e.slideUp(400)
                })
            }),
            !1
        },
        css: function (e, n, i) {
            return i || (i = n),
            e.each(function () {
                var e = t(this).data(i);
                if (e) {
                    var o = {};
                    o[n] = e,
                    t(this).css(o)
                }
            }),
            !1
        },
        dropDown: function (e, n) {
            var i = t("." + e);
            return i.each(function () {
                var e = t(this);
                e.on("click", ".js__drop_down_button", function (o) {
                    return o.preventDefault(),
                    (t(window).width() < 1025 || 0 == n) && (e.hasClass("active") ? e.removeClass("active") : (i.removeClass("active"), e.addClass("active"))),
                    !1
                })
            }),
            t("html").on("click", function (n) {
                var i = t(n.target);
                i.hasClass(e) || i.parents("." + e).length || t("." + e + ".active").removeClass("active")
            }),
            !1
        },
        logout: function () {
            t(".js__logout").on("click", function (t) {
                return t.preventDefault(),
                swal({
                    title: "Logout?",
                    text: "Are you sure you want to logout?",
                    type: "warning",
                    showCancelButton: !0,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, I'm out!",
                    cancelButtonText: "No, stay plx!",
                    closeOnConfirm: !1,
                    closeOnCancel: !0,
                    confirmButtonColor: "#f60e0e"
                }, function (t) {
                    t && swal({
                        title: "Logout success",
                        text: "See you later!",
                        type: "success",
                        confirmButtonColor: "#304ffe"
                    })
                }),
                !1
            })
        },
        menu: function () {
            return t(".js__menu_mobile").on("click", function () {
                t("html").toggleClass("menu-active"),
                t(window).trigger("resize")
            }),
            t(".js__menu_close").on("click", function () {
                t("html").removeClass("menu-active")
            }),
            t("body").on("click", function (e) {
                if (t("html.menu-active").length && t(window).width() < 800) {
                    var n = t(e.target);
                    n.hasClass("main-menu") || n.hasClass("js__menu_mobile") || n.parents(".main-menu").length || n.parents(".js__menu_mobile").length || t("html").removeClass("menu-active")
                }
            }),
            !1
        },
        tab: function (e, n) {
            return t(".js__tab").each(function () {
                var e = t(this);
                e.on("click", ".js__tab_control", function (i) {
                    var o = t(this).data("target");
                    if (i.preventDefault(), e.find(".js__tab_content").removeClass("js__active"), e.find(".js__tab_control").removeClass("js__active"), t(this).addClass("js__active"), o) t(o).addClass("js__active");
                    else {
                        var a;
                        a = n ? t(this).parents(n).first().index() : t(this).index(),
                        e.find(".js__tab_content").eq(a).addClass("js__active")
                    }
                    return !1
                })
            }),
            !1
        },
        todo: function () {
            return t(".js__todo_widget").each(function () {
                var e = (t(this), t(this).find(".js__todo_list")),
                    n = t(this).find(".js__todo_value"),
                    i = t(this).find(".js__todo_button");
                i.on("click", function () {
                    if ("" != n.val()) {
                        var t = Math.floor(1e8 * Math.random() + 1);
                        e.append('<div class="todo-item"><div class="checkbox"><input type="checkbox" id="todo-' + t + '"><label for="todo-' + t + '">' + n.val() + "</label></div></div>"),
                        n.val("")
                    } else alert("You must enter task name.");
                    return !1
                })
            }),
            !1
        },
        toggle: function () {
            return t(".js__toggle_open").on("click", function (e) {
                return e.preventDefault(),
                t(t(this).data("target")).hasClass("active") || t(".js__toggle").removeClass("active"),
                t(t(this).data("target")).toggleClass("active"),
                !1
            }),
            t(".js__toggle_close").on("click", function (e) {
                return e.preventDefault(),
                t(this).parents(".js__toggle").removeClass("active"),
                !1
            }),
            t("body").on("click", function (e) {
                if (t(".js__toggle").hasClass("active")) {
                    var n = t(e.target);
                    n.hasClass("js__toggle_open") || n.hasClass("js__toggle") || n.parents(".js__toggle_open").length || n.parents(".js__toggle").length || t(".js__toggle").removeClass("active")
                }
            }),
            !1
        }
    },
    e.func = {
        childReturnWidth: function (n, i) {
            if (n.children("li").children(".sub-menu").length) {
                var o = 0;
                return n.children("li").children(".sub-menu").each(function () {
                    var n = e.func.childReturnWidth(t(this), i + t(this).outerWidth());
                    n > o && (o = n)
                }),
                o
            }
            return i
        },
        getResponsiveSettings: function (e) {
            var n = e.data("responsive"),
                i = [];
            if (n) {
                for (; n.indexOf("'") > -1;) n = n.replace("'", '"');
                var o = JSON.parse(n);
                t.each(o, function (t, e) {
                    i[i.length] = {
                        breakpoint: t,
                        settings: {
                            slidesToShow: e,
                            slidesToScroll: e
                        }
                    }
                })
            }
            return i
        },
        getChart: function () {
            t(".js__chart").each(function () {
                var e, n, i, o = t(this),
                    a = o.data("chart"),
                    s = [],
                    r = o.attr("id"),
                    l = o.data("type");
                if (a) {
                    var c, u, d = a.split("|");
                    for (c = 0; c < d.length; c++) for (d[c] = d[c].trim(), s[c] = d[c].split("/"), u = 0; u < s[c].length; u++) if (s[c][u].indexOf("'") > -1) {
                        for (; s[c][u].indexOf("'") > -1;) s[c][u] = s[c][u].replace("'", "");
                        s[c][u] = s[c][u].trim()
                    } else s[c][u].indexOf(".") > -1 ? s[c][u] = parseFloat(s[c][u]) : s[c][u] = parseInt(s[c][u], 10);
                    switch (n = google.visualization.arrayToDataTable(s), l) {
                        case "circle":
                            e = {
                                chartArea: {
                                    left: 0,
                                    top: 0,
                                    width: "100%",
                                    height: "75%"
                                },
                                legend: {
                                    position: "bottom"
                                },
                                colors: ["#304ffe", "#f60e0e", "#ffa000"],
                                fontName: "Poppins"
                            },
                            i = new google.visualization.PieChart(document.getElementById(r));
                            break;
                        case "donut":
                            e = {
                                pieHole: .3,
                                chartArea: {
                                    left: 0,
                                    top: 0,
                                    width: "100%",
                                    height: "75%"
                                },
                                legend: {
                                    position: "bottom"
                                },
                                colors: ["#304ffe", "#f60e0e", "#ffa000"],
                                fontName: "Poppins"
                            },
                            i = new google.visualization.PieChart(document.getElementById(r));
                            break;
                        case "column":
                            e = {
                                chartArea: {
                                    left: 30,
                                    top: 10,
                                    width: "100%",
                                    height: "80%"
                                },
                                colors: ["#304ffe"],
                                fontName: "Poppins"
                            },
                            i = new google.visualization.ColumnChart(document.getElementById(r));
                            break;
                        case "curve":
                            e = {
                                chartArea: {
                                    left: 30,
                                    top: 10,
                                    width: "90%",
                                    height: "80%"
                                },
                                curveType: "function",
                                colors: ["#304ffe", "#f60e0e", "#ffa000"],
                                fontName: "Poppins"
                            },
                            i = new google.visualization.LineChart(document.getElementById(r));
                            break;
                        case "line":
                            e = {
                                chartArea: {
                                    left: 30,
                                    top: 10,
                                    width: "90%",
                                    height: "80%"
                                },
                                fontName: "Poppins"
                            },
                            i = new google.visualization.LineChart(document.getElementById(r));
                            break;
                        case "area":
                            e = {
                                chartArea: {
                                    left: 50,
                                    top: 20,
                                    width: "100%",
                                    height: "70%"
                                },
                                legend: {
                                    position: "bottom"
                                },
                                fontName: "Poppins"
                            },
                            i = new google.visualization.AreaChart(document.getElementById(r))
                    }
                    i.draw(n, e)
                }
            })
        },
        resizeNotice: function () {
            t(".notice-popup").each(function () {
                var e = t(this),
                    n = parseInt(e.data("space"), 10) > 0 ? parseInt(e.data("space"), 10) : 75,
                    i = t(window).height() - n;
                e.attr("style", ""),
                e.height() > i && e.css({
                    height: i
                })
            })
        }
    },
    e.plugin = {
        init: function () {
            return e.plugin.chart(),
            e.plugin.mCustomScrollbar(),
            e.plugin.select2(),
            e.plugin.ui.accordion(),
            e.plugin.ui.slider(),
            e.plugin.ui.sortable(),
            e.plugin.ui.tabs(),
            e.plugin.waves(),
            e.plugin.isotope.filter(),
            !1
        },
        chart: function () {
            return t(".js__chart").length && (google.charts.load("current", {
                packages: ["corechart"]
            }), google.charts.setOnLoadCallback(e.func.getChart)),
            !1
        },
        isotope: {
            init: function () {
                return setTimeout(function () {
                    t(".js__filter_isotope").each(function () {
                        var e = t(this);
                        e.find(".js__isotope_items").isotope({
                            itemSelector: ".js__isotope_item",
                            layoutMode: "cellsByRow"
                        })
                    })
                }, 100),
                !1
            },
            filter: function () {
                return t(".js__filter_isotope").each(function () {
                    var e = t(this);
                    e.on("click", ".js__filter_control", function (n) {
                        return n.preventDefault(),
                        t(this).hasClass(".js__active") || (e.find(".js__filter_control").removeClass("js__active"), t(this).addClass("js__active"), e.find(".js__isotope_items").isotope({
                            filter: t(this).data("filter")
                        })),
                        !1
                    })
                }),
                !1
            }
        },
        mCustomScrollbar: function () {
            return t(".main-menu").length && t(".main-menu .content").mCustomScrollbar(),
            t(".notice-popup").length && t(".notice-popup .content").mCustomScrollbar(),
            !1
        },
        select2: function () {
            return t(".js__select2").each(function () {
                var e = t(this).data("min-results"),
                    n = t(this).data("container-class");
                e ? ("Infinity" == e ? t(this).select2({
                    minimumResultsForSearch: 1 / 0
                }) : t(this).select2({
                    minimumResultsForSearch: parseInt(e, 10)
                }), n && t(this).on("select2:open", function () {
                    return t(".select2-container--open").addClass(n),
                    !1
                })) : t(this).select2()
            }),
            !1
        },
        ui: {
            accordion: function () {
                return t(".js__ui_accordion").length && t(".js__ui_accordion").accordion({
                    heightStyle: "content",
                    collapsible: !0
                }),
                !1
            },
            slider: function () {
                return t(".js__ui_slider").each(function () {
                    var e = t(this),
                        n = e.find(".js__slider_range"),
                        i = e.find(".js__slider_amount"),
                        o = parseInt(e.data("min"), 10),
                        a = parseInt(e.data("max"), 10),
                        s = parseInt(e.data("value-1"), 10),
                        r = parseInt(e.data("value-2"), 10),
                        l = e.data("range");
                    r > 0 ? (n.slider({
                        range: !0,
                        min: o,
                        max: a,
                        values: [s, r],
                        slide: function (t, e) {
                            i.val("$" + e.values[0] + " - $" + e.values[1])
                        }
                    }), i.val("$" + n.slider("values", 0) + " - $" + n.slider("values", 1))) : (n.slider({
                        range: l,
                        min: o,
                        max: a,
                        value: s,
                        slide: function (t, e) {
                            i.val("$" + e.value)
                        }
                    }), i.val("$" + n.slider("value")))
                }),
                !1
            },
            sortable: function () {
                return t(".js__sortable").length && t(".js__sortable").sortable({
                    revert: !0,
                    start: function (t, e) {
                        e.placeholder.height(e.item.height() - 20),
                        e.placeholder.css("visibility", "visible")
                    }
                }),
                !1
            },
            tabs: function () {
                return t(".js__ui_tab").length && t(".js__ui_tab").tabs(),
                !1
            }
        },
        waves: function () {
            return t(".js__control").length && (Waves.attach(".js__control"), Waves.init()),
            !1
        }
    }
}(jQuery);

$(document).ready(function () {
    var table = $('.datatable');
    if (table != null) {
        table.dataTable({
            bLengthChange: true,
            searching: true
        });
    }
});