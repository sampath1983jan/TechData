﻿/**
 * Copyright (c) 2016-2019, The Cytoscape Consortium.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the “Software”), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
! function (e, t) {
	"object" == typeof exports && "undefined" != typeof module ? module.exports = t() : "function" == typeof define && define.amd ? define(t) : (e = e || self).cytoscape = t()
}(this, function () {
	"use strict";

	function e(t) {
		return (e = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (e) {
			return typeof e
		} : function (e) {
			return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
		})(t)
	}

	function t(e, t) {
		if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function")
	}

	function n(e, t) {
		for (var n = 0; n < t.length; n++) {
			var r = t[n];
			r.enumerable = r.enumerable || !1, r.configurable = !0, "value" in r && (r.writable = !0), Object.defineProperty(e, r.key, r)
		}
	}

	function r(e, t, r) {
		return t && n(e.prototype, t), r && n(e, r), e
	}

	function i(e, t) {
		return function (e) {
			if (Array.isArray(e)) return e
		}(e) || function (e, t) {
			var n = [],
				r = !0,
				i = !1,
				a = void 0;
			try {
				for (var o, s = e[Symbol.iterator](); !(r = (o = s.next()).done) && (n.push(o.value), !t || n.length !== t); r = !0);
			} catch (e) {
				i = !0, a = e
			} finally {
				try {
					r || null == s.return || s.return()
				} finally {
					if (i) throw a
				}
			}
			return n
		}(e, t) || function () {
			throw new TypeError("Invalid attempt to destructure non-iterable instance")
		}()
	}
	var a = "undefined" == typeof window ? null : window,
		o = a ? a.navigator : null,
		s = (a && a.document, e("")),
		l = e({}),
		u = e(function () { }),
		c = "undefined" == typeof HTMLElement ? "undefined" : e(HTMLElement),
		h = function (e) {
			return e && e.instanceString && p(e.instanceString) ? e.instanceString() : null
		},
		d = function (t) {
			return null != t && e(t) == s
		},
		p = function (t) {
			return null != t && e(t) === u
		},
		f = function (e) {
			return Array.isArray ? Array.isArray(e) : null != e && e instanceof Array
		},
		g = function (t) {
			return null != t && e(t) === l && !f(t) && t.constructor === Object
		},
		v = function (t) {
			return null != t && e(t) === e(1) && !isNaN(t)
		},
		y = function (e) {
			return "undefined" === c ? void 0 : null != e && e instanceof HTMLElement
		},
		m = function (e) {
			return b(e) || x(e)
		},
		b = function (e) {
			return "collection" === h(e) && e._private.single
		},
		x = function (e) {
			return "collection" === h(e) && !e._private.single
		},
		w = function (e) {
			return "core" === h(e)
		},
		E = function (e) {
			return "stylesheet" === h(e)
		},
		k = function (e) {
			return null == e || !("" !== e && !e.match(/^\s+$/))
		},
		C = function (t) {
			return function (t) {
				return null != t && e(t) === l
			}(t) && p(t.then)
		},
		S = function () {
			return o && o.userAgent.match(/msie|trident|edge/i)
		},
		D = function (e, t) {
			t || (t = function () {
				if (1 === arguments.length) return arguments[0];
				if (0 === arguments.length) return "undefined";
				for (var e = [], t = 0; t < arguments.length; t++) e.push(arguments[t]);
				return e.join("$")
			});
			var n = function n() {
				var r, i = arguments,
					a = t.apply(this, i),
					o = n.cache;
				return (r = o[a]) || (r = o[a] = e.apply(this, i)), r
			};
			return n.cache = {}, n
		},
		P = D(function (e) {
			return e.replace(/([A-Z])/g, function (e) {
				return "-" + e.toLowerCase()
			})
		}),
		T = D(function (e) {
			return e.replace(/(-\w)/g, function (e) {
				return e[1].toUpperCase()
			})
		}),
		M = D(function (e, t) {
			return e + t[0].toUpperCase() + t.substring(1)
		}, function (e, t) {
			return e + "$" + t
		}),
		B = function (e) {
			return k(e) ? e : e.charAt(0).toUpperCase() + e.substring(1)
		},
		_ = "(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))",
		N = function (e, t) {
			return e < t ? -1 : e > t ? 1 : 0
		},
		I = null != Object.assign ? Object.assign.bind(Object) : function (e) {
			for (var t = arguments, n = 1; n < t.length; n++) {
				var r = t[n];
				if (null != r)
					for (var i = Object.keys(r), a = 0; a < i.length; a++) {
						var o = i[a];
						e[o] = r[o]
					}
			}
			return e
		},
		z = function (e) {
			return (f(e) ? e : null) || function (e) {
				return L[e.toLowerCase()]
			}(e) || function (e) {
				if ((4 === e.length || 7 === e.length) && "#" === e[0]) {
					var t, n, r;
					return 4 === e.length ? (t = parseInt(e[1] + e[1], 16), n = parseInt(e[2] + e[2], 16), r = parseInt(e[3] + e[3], 16)) : (t = parseInt(e[1] + e[2], 16), n = parseInt(e[3] + e[4], 16), r = parseInt(e[5] + e[6], 16)), [t, n, r]
				}
			}(e) || function (e) {
				var t, n = new RegExp("^rgb[a]?\\(((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%]?)\\s*,\\s*((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%]?)\\s*,\\s*((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%]?)(?:\\s*,\\s*((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))))?\\)$").exec(e);
				if (n) {
					t = [];
					for (var r = [], i = 1; i <= 3; i++) {
						var a = n[i];
						if ("%" === a[a.length - 1] && (r[i] = !0), a = parseFloat(a), r[i] && (a = a / 100 * 255), a < 0 || a > 255) return;
						t.push(Math.floor(a))
					}
					var o = r[1] || r[2] || r[3],
						s = r[1] && r[2] && r[3];
					if (o && !s) return;
					var l = n[4];
					if (void 0 !== l) {
						if ((l = parseFloat(l)) < 0 || l > 1) return;
						t.push(l)
					}
				}
				return t
			}(e) || function (e) {
				var t, n, r, i, a, o, s, l;

				function u(e, t, n) {
					return n < 0 && (n += 1), n > 1 && (n -= 1), n < 1 / 6 ? e + 6 * (t - e) * n : n < .5 ? t : n < 2 / 3 ? e + (t - e) * (2 / 3 - n) * 6 : e
				}
				var c = new RegExp("^hsl[a]?\\(((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?)))\\s*,\\s*((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%])\\s*,\\s*((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%])(?:\\s*,\\s*((?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))))?\\)$").exec(e);
				if (c) {
					if ((n = parseInt(c[1])) < 0 ? n = (360 - -1 * n % 360) % 360 : n > 360 && (n %= 360), n /= 360, (r = parseFloat(c[2])) < 0 || r > 100) return;
					if (r /= 100, (i = parseFloat(c[3])) < 0 || i > 100) return;
					if (i /= 100, void 0 !== (a = c[4]) && ((a = parseFloat(a)) < 0 || a > 1)) return;
					if (0 === r) o = s = l = Math.round(255 * i);
					else {
						var h = i < .5 ? i * (1 + r) : i + r - i * r,
							d = 2 * i - h;
						o = Math.round(255 * u(d, h, n + 1 / 3)), s = Math.round(255 * u(d, h, n)), l = Math.round(255 * u(d, h, n - 1 / 3))
					}
					t = [o, s, l, a]
				}
				return t
			}(e)
		},
		L = {
			transparent: [0, 0, 0, 0],
			aliceblue: [240, 248, 255],
			antiquewhite: [250, 235, 215],
			aqua: [0, 255, 255],
			aquamarine: [127, 255, 212],
			azure: [240, 255, 255],
			beige: [245, 245, 220],
			bisque: [255, 228, 196],
			black: [0, 0, 0],
			blanchedalmond: [255, 235, 205],
			blue: [0, 0, 255],
			blueviolet: [138, 43, 226],
			brown: [165, 42, 42],
			burlywood: [222, 184, 135],
			cadetblue: [95, 158, 160],
			chartreuse: [127, 255, 0],
			chocolate: [210, 105, 30],
			coral: [255, 127, 80],
			cornflowerblue: [100, 149, 237],
			cornsilk: [255, 248, 220],
			crimson: [220, 20, 60],
			cyan: [0, 255, 255],
			darkblue: [0, 0, 139],
			darkcyan: [0, 139, 139],
			darkgoldenrod: [184, 134, 11],
			darkgray: [169, 169, 169],
			darkgreen: [0, 100, 0],
			darkgrey: [169, 169, 169],
			darkkhaki: [189, 183, 107],
			darkmagenta: [139, 0, 139],
			darkolivegreen: [85, 107, 47],
			darkorange: [255, 140, 0],
			darkorchid: [153, 50, 204],
			darkred: [139, 0, 0],
			darksalmon: [233, 150, 122],
			darkseagreen: [143, 188, 143],
			darkslateblue: [72, 61, 139],
			darkslategray: [47, 79, 79],
			darkslategrey: [47, 79, 79],
			darkturquoise: [0, 206, 209],
			darkviolet: [148, 0, 211],
			deeppink: [255, 20, 147],
			deepskyblue: [0, 191, 255],
			dimgray: [105, 105, 105],
			dimgrey: [105, 105, 105],
			dodgerblue: [30, 144, 255],
			firebrick: [178, 34, 34],
			floralwhite: [255, 250, 240],
			forestgreen: [34, 139, 34],
			fuchsia: [255, 0, 255],
			gainsboro: [220, 220, 220],
			ghostwhite: [248, 248, 255],
			gold: [255, 215, 0],
			goldenrod: [218, 165, 32],
			gray: [128, 128, 128],
			grey: [128, 128, 128],
			green: [0, 128, 0],
			greenyellow: [173, 255, 47],
			honeydew: [240, 255, 240],
			hotpink: [255, 105, 180],
			indianred: [205, 92, 92],
			indigo: [75, 0, 130],
			ivory: [255, 255, 240],
			khaki: [240, 230, 140],
			lavender: [230, 230, 250],
			lavenderblush: [255, 240, 245],
			lawngreen: [124, 252, 0],
			lemonchiffon: [255, 250, 205],
			lightblue: [173, 216, 230],
			lightcoral: [240, 128, 128],
			lightcyan: [224, 255, 255],
			lightgoldenrodyellow: [250, 250, 210],
			lightgray: [211, 211, 211],
			lightgreen: [144, 238, 144],
			lightgrey: [211, 211, 211],
			lightpink: [255, 182, 193],
			lightsalmon: [255, 160, 122],
			lightseagreen: [32, 178, 170],
			lightskyblue: [135, 206, 250],
			lightslategray: [119, 136, 153],
			lightslategrey: [119, 136, 153],
			lightsteelblue: [176, 196, 222],
			lightyellow: [255, 255, 224],
			lime: [0, 255, 0],
			limegreen: [50, 205, 50],
			linen: [250, 240, 230],
			magenta: [255, 0, 255],
			maroon: [128, 0, 0],
			mediumaquamarine: [102, 205, 170],
			mediumblue: [0, 0, 205],
			mediumorchid: [186, 85, 211],
			mediumpurple: [147, 112, 219],
			mediumseagreen: [60, 179, 113],
			mediumslateblue: [123, 104, 238],
			mediumspringgreen: [0, 250, 154],
			mediumturquoise: [72, 209, 204],
			mediumvioletred: [199, 21, 133],
			midnightblue: [25, 25, 112],
			mintcream: [245, 255, 250],
			mistyrose: [255, 228, 225],
			moccasin: [255, 228, 181],
			navajowhite: [255, 222, 173],
			navy: [0, 0, 128],
			oldlace: [253, 245, 230],
			olive: [128, 128, 0],
			olivedrab: [107, 142, 35],
			orange: [255, 165, 0],
			orangered: [255, 69, 0],
			orchid: [218, 112, 214],
			palegoldenrod: [238, 232, 170],
			palegreen: [152, 251, 152],
			paleturquoise: [175, 238, 238],
			palevioletred: [219, 112, 147],
			papayawhip: [255, 239, 213],
			peachpuff: [255, 218, 185],
			peru: [205, 133, 63],
			pink: [255, 192, 203],
			plum: [221, 160, 221],
			powderblue: [176, 224, 230],
			purple: [128, 0, 128],
			red: [255, 0, 0],
			rosybrown: [188, 143, 143],
			royalblue: [65, 105, 225],
			saddlebrown: [139, 69, 19],
			salmon: [250, 128, 114],
			sandybrown: [244, 164, 96],
			seagreen: [46, 139, 87],
			seashell: [255, 245, 238],
			sienna: [160, 82, 45],
			silver: [192, 192, 192],
			skyblue: [135, 206, 235],
			slateblue: [106, 90, 205],
			slategray: [112, 128, 144],
			slategrey: [112, 128, 144],
			snow: [255, 250, 250],
			springgreen: [0, 255, 127],
			steelblue: [70, 130, 180],
			tan: [210, 180, 140],
			teal: [0, 128, 128],
			thistle: [216, 191, 216],
			tomato: [255, 99, 71],
			turquoise: [64, 224, 208],
			violet: [238, 130, 238],
			wheat: [245, 222, 179],
			white: [255, 255, 255],
			whitesmoke: [245, 245, 245],
			yellow: [255, 255, 0],
			yellowgreen: [154, 205, 50]
		},
		A = function (e) {
			for (var t = e.map, n = e.keys, r = n.length, i = 0; i < r; i++) {
				var a = n[i];
				if (g(a)) throw Error("Tried to set map with object key");
				i < n.length - 1 ? (null == t[a] && (t[a] = {}), t = t[a]) : t[a] = e.value
			}
		},
		O = function (e) {
			for (var t = e.map, n = e.keys, r = n.length, i = 0; i < r; i++) {
				var a = n[i];
				if (g(a)) throw Error("Tried to get map with object key");
				if (null == (t = t[a])) return t
			}
			return t
		},
		R = "undefined" != typeof globalThis ? globalThis : "undefined" != typeof window ? window : "undefined" != typeof global ? global : "undefined" != typeof self ? self : {};
	var V = "Expected a function",
		F = NaN,
		q = "[object Symbol]",
		Y = /^\s+|\s+$/g,
		j = /^[-+]0x[0-9a-f]+$/i,
		X = /^0b[01]+$/i,
		W = /^0o[0-7]+$/i,
		H = parseInt,
		K = "object" == typeof R && R && R.Object === Object && R,
		G = "object" == typeof self && self && self.Object === Object && self,
		Z = K || G || Function("return this")(),
		U = Object.prototype.toString,
		$ = Math.max,
		Q = Math.min,
		J = function () {
			return Z.Date.now()
		};

	function ee(e) {
		var t = typeof e;
		return !!e && ("object" == t || "function" == t)
	}

	function te(e) {
		if ("number" == typeof e) return e;
		if (function (e) {
			return "symbol" == typeof e || function (e) {
				return !!e && "object" == typeof e
			}(e) && U.call(e) == q
		}(e)) return F;
		if (ee(e)) {
			var t = "function" == typeof e.valueOf ? e.valueOf() : e;
			e = ee(t) ? t + "" : t
		}
		if ("string" != typeof e) return 0 === e ? e : +e;
		e = e.replace(Y, "");
		var n = X.test(e);
		return n || W.test(e) ? H(e.slice(2), n ? 2 : 8) : j.test(e) ? F : +e
	}
	var ne = function (e, t, n) {
		var r, i, a, o, s, l, u = 0,
			c = !1,
			h = !1,
			d = !0;
		if ("function" != typeof e) throw new TypeError(V);

		function p(t) {
			var n = r,
				a = i;
			return r = i = void 0, u = t, o = e.apply(a, n)
		}

		function f(e) {
			var n = e - l;
			return void 0 === l || n >= t || n < 0 || h && e - u >= a
		}

		function g() {
			var e = J();
			if (f(e)) return v(e);
			s = setTimeout(g, function (e) {
				var n = t - (e - l);
				return h ? Q(n, a - (e - u)) : n
			}(e))
		}

		function v(e) {
			return s = void 0, d && r ? p(e) : (r = i = void 0, o)
		}

		function y() {
			var e = J(),
				n = f(e);
			if (r = arguments, i = this, l = e, n) {
				if (void 0 === s) return function (e) {
					return u = e, s = setTimeout(g, t), c ? p(e) : o
				}(l);
				if (h) return s = setTimeout(g, t), p(l)
			}
			return void 0 === s && (s = setTimeout(g, t)), o
		}
		return t = te(t) || 0, ee(n) && (c = !!n.leading, a = (h = "maxWait" in n) ? $(te(n.maxWait) || 0, t) : a, d = "trailing" in n ? !!n.trailing : d), y.cancel = function () {
			void 0 !== s && clearTimeout(s), u = 0, r = l = i = s = void 0
		}, y.flush = function () {
			return void 0 === s ? o : v(J())
		}, y
	},
		re = a ? a.performance : null,
		ie = re && re.now ? function () {
			return re.now()
		} : function () {
			return Date.now()
		},
		ae = function () {
			if (a) {
				if (a.requestAnimationFrame) return function (e) {
					a.requestAnimationFrame(e)
				};
				if (a.mozRequestAnimationFrame) return function (e) {
					a.mozRequestAnimationFrame(e)
				};
				if (a.webkitRequestAnimationFrame) return function (e) {
					a.webkitRequestAnimationFrame(e)
				};
				if (a.msRequestAnimationFrame) return function (e) {
					a.msRequestAnimationFrame(e)
				}
			}
			return function (e) {
				e && setTimeout(function () {
					e(ie())
				}, 1e3 / 60)
			}
		}(),
		oe = function (e) {
			return ae(e)
		},
		se = ie,
		le = function (e) {
			for (var t, n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 5381; !(t = e.next()).done;) n = 33 * n ^ t.value;
			return n >>> 0
		},
		ue = function (e) {
			return (33 * (arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 5381) ^ e) >>> 0
		},
		ce = function (e, t) {
			var n = {
				value: 0,
				done: !1
			},
				r = 0,
				i = e.length;
			return le({
				next: function () {
					return r < i ? n.value = e[r++] : n.done = !0, n
				}
			}, t)
		},
		he = function (e, t) {
			var n = {
				value: 0,
				done: !1
			},
				r = 0,
				i = e.length;
			return le({
				next: function () {
					return r < i ? n.value = e.charCodeAt(r++) : n.done = !0, n
				}
			}, t)
		},
		de = function () {
			return pe(arguments)
		},
		pe = function (e) {
			for (var t, n = 0; n < e.length; n++) {
				var r = e[n];
				t = 0 === n ? he(r) : he(r, t)
			}
			return t
		},
		fe = !0,
		ge = null != console.warn,
		ve = null != console.trace,
		ye = Number.MAX_SAFE_INTEGER || 9007199254740991,
		me = function () {
			return !0
		},
		be = function () {
			return !1
		},
		xe = function () {
			return 0
		},
		we = function () { },
		Ee = function (e) {
			throw new Error(e)
		},
		ke = function (e) {
			if (void 0 === e) return fe;
			fe = !!e
		},
		Ce = function (e) {
			ke() && (ge ? console.warn(e) : (console.log(e), ve && console.trace()))
		},
		Se = function (e) {
			return null == e ? e : f(e) ? e.slice() : g(e) ? function (e) {
				return I({}, e)
			}(e) : e
		},
		De = function (e, t) {
			for (t = e = ""; e++ < 36; t += 51 * e & 52 ? (15 ^ e ? 8 ^ Math.random() * (20 ^ e ? 16 : 4) : 4).toString(16) : "-");
			return t
		},
		Pe = {},
		Te = function () {
			return Pe
		},
		Me = function (e) {
			var t = Object.keys(e);
			return function (n) {
				for (var r = {}, i = 0; i < t.length; i++) {
					var a = t[i],
						o = null == n ? void 0 : n[a];
					r[a] = void 0 === o ? e[a] : o
				}
				return r
			}
		},
		Be = function (e, t, n) {
			for (var r = e.length; r >= 0 && (e[r] !== t || (e.splice(r, 1), n)); r--);
		},
		_e = function (e) {
			e.splice(0, e.length)
		},
		Ne = function (e, t, n) {
			return n && (t = M(n, t)), e[t]
		},
		Ie = function (e, t, n, r) {
			n && (t = M(n, t)), e[t] = r
		},
		ze = "undefined" != typeof Map ? Map : function () {
			function e() {
				t(this, e), this._obj = {}
			}
			return r(e, [{
				key: "set",
				value: function (e, t) {
					return this._obj[e] = t, this
				}
			}, {
				key: "delete",
				value: function (e) {
					return this._obj[e] = void 0, this
				}
			}, {
				key: "clear",
				value: function () {
					this._obj = {}
				}
			}, {
				key: "has",
				value: function (e) {
					return void 0 !== this._obj[e]
				}
			}, {
				key: "get",
				value: function (e) {
					return this._obj[e]
				}
			}]), e
		}(),
		Le = function () {
			function e(n) {
				if (t(this, e), this._obj = Object.create(null), this.size = 0, null != n) {
					var r;
					r = null != n.instanceString && n.instanceString() === this.instanceString() ? n.toArray() : n;
					for (var i = 0; i < r.length; i++) this.add(r[i])
				}
			}
			return r(e, [{
				key: "instanceString",
				value: function () {
					return "set"
				}
			}, {
				key: "add",
				value: function (e) {
					var t = this._obj;
					1 !== t[e] && (t[e] = 1, this.size++)
				}
			}, {
				key: "delete",
				value: function (e) {
					var t = this._obj;
					1 === t[e] && (t[e] = 0, this.size--)
				}
			}, {
				key: "clear",
				value: function () {
					this._obj = Object.create(null)
				}
			}, {
				key: "has",
				value: function (e) {
					return 1 === this._obj[e]
				}
			}, {
				key: "toArray",
				value: function () {
					var e = this;
					return Object.keys(this._obj).filter(function (t) {
						return e.has(t)
					})
				}
			}, {
				key: "forEach",
				value: function (e, t) {
					return this.toArray().forEach(e, t)
				}
			}]), e
		}(),
		Ae = "undefined" !== ("undefined" == typeof Set ? "undefined" : e(Set)) ? Set : Le,
		Oe = function (e, t, n) {
			if (n = !(void 0 !== n && !n), void 0 !== e && void 0 !== t && w(e)) {
				var r = t.group;
				if (null == r && (r = t.data && null != t.data.source && null != t.data.target ? "edges" : "nodes"), "nodes" === r || "edges" === r) {
					this.length = 1, this[0] = this;
					var i = this._private = {
						cy: e,
						single: !0,
						data: t.data || {},
						position: t.position || {
							x: 0,
							y: 0
						},
						autoWidth: void 0,
						autoHeight: void 0,
						autoPadding: void 0,
						compoundBoundsClean: !1,
						listeners: [],
						group: r,
						style: {},
						rstyle: {},
						styleCxts: [],
						styleKeys: {},
						removed: !0,
						selected: !!t.selected,
						selectable: void 0 === t.selectable || !!t.selectable,
						locked: !!t.locked,
						grabbed: !1,
						grabbable: void 0 === t.grabbable || !!t.grabbable,
						pannable: void 0 === t.pannable ? "edges" === r : !!t.pannable,
						active: !1,
						classes: new Ae,
						animation: {
							current: [],
							queue: []
						},
						rscratch: {},
						scratch: t.scratch || {},
						edges: [],
						children: [],
						parent: null,
						traversalCache: {},
						backgrounding: !1,
						bbCache: null,
						bbCacheShift: {
							x: 0,
							y: 0
						},
						bodyBounds: null,
						overlayBounds: null,
						labelBounds: {
							all: null,
							source: null,
							target: null,
							main: null
						}
					};
					if (null == i.position.x && (i.position.x = 0), null == i.position.y && (i.position.y = 0), t.renderedPosition) {
						var a = t.renderedPosition,
							o = e.pan(),
							s = e.zoom();
						i.position = {
							x: (a.x - o.x) / s,
							y: (a.y - o.y) / s
						}
					}
					var l = [];
					f(t.classes) ? l = t.classes : d(t.classes) && (l = t.classes.split(/\s+/));
					for (var u = 0, c = l.length; u < c; u++) {
						var h = l[u];
						h && "" !== h && i.classes.add(h)
					}
					this.createEmitter();
					var p = t.style || t.css;
					p && (Ce("Setting a `style` bypass at element creation is deprecated"), this.style(p)), (void 0 === n || n) && this.restore()
				} else Ee("An element must be of type `nodes` or `edges`; you specified `" + r + "`")
			} else Ee("An element must have a core reference and parameters set")
		},
		Re = function (e) {
			return e = {
				bfs: e.bfs || !e.dfs,
				dfs: e.dfs || !e.bfs
			},
				function (t, n, r) {
					var i;
					g(t) && !m(t) && (t = (i = t).roots || i.root, n = i.visit, r = i.directed), r = 2 !== arguments.length || p(n) ? r : n, n = p(n) ? n : function () { };
					for (var a, o = this._private.cy, s = t = d(t) ? this.filter(t) : t, l = [], u = [], c = {}, h = {}, f = {}, v = 0, y = this.byGroup(), b = y.nodes, x = y.edges, w = 0; w < s.length; w++) {
						var E = s[w],
							k = E.id();
						E.isNode() && (l.unshift(E), e.bfs && (f[k] = !0, u.push(E)), h[k] = 0)
					}
					var C = function () {
						var t = e.bfs ? l.shift() : l.pop(),
							i = t.id();
						if (e.dfs) {
							if (f[i]) return "continue";
							f[i] = !0, u.push(t)
						}
						var o, s = h[i],
							d = c[i],
							p = null != d ? d.source() : null,
							g = null != d ? d.target() : null,
							y = null == d ? void 0 : t.same(p) ? g[0] : p[0];
						if (!0 === (o = n(t, d, y, v++, s))) return a = t, "break";
						if (!1 === o) return "break";
						for (var m = t.connectedEdges().filter(function (e) {
							return (!r || e.source().same(t)) && x.has(e)
						}), w = 0; w < m.length; w++) {
							var E = m[w],
								k = E.connectedNodes().filter(function (e) {
									return !e.same(t) && b.has(e)
								}),
								C = k.id();
							0 === k.length || f[C] || (k = k[0], l.push(k), e.bfs && (f[C] = !0, u.push(k)), c[C] = E, h[C] = h[i] + 1)
						}
					};
					e: for (; 0 !== l.length;) {
						switch (C()) {
							case "continue":
								continue;
							case "break":
								break e
						}
					}
					for (var S = o.collection(), D = 0; D < u.length; D++) {
						var P = u[D],
							T = c[P.id()];
						null != T && S.merge(T), S.merge(P)
					}
					return {
						path: o.collection(S),
						found: o.collection(a)
					}
				}
		},
		Ve = {
			breadthFirstSearch: Re({
				bfs: !0
			}),
			depthFirstSearch: Re({
				dfs: !0
			})
		};
	Ve.bfs = Ve.breadthFirstSearch, Ve.dfs = Ve.depthFirstSearch;
	var Fe = function (e, t) {
		return e(t = {
			exports: {}
		}, t.exports), t.exports
	}(function (e, t) {
		(function () {
			var t, n, r, i, a, o, s, l, u, c, h, d, p, f, g;
			r = Math.floor, c = Math.min, n = function (e, t) {
				return e < t ? -1 : e > t ? 1 : 0
			}, u = function (e, t, i, a, o) {
				var s;
				if (null == i && (i = 0), null == o && (o = n), i < 0) throw new Error("lo must be non-negative");
				for (null == a && (a = e.length); i < a;) o(t, e[s = r((i + a) / 2)]) < 0 ? a = s : i = s + 1;
				return [].splice.apply(e, [i, i - i].concat(t)), t
			}, o = function (e, t, r) {
				return null == r && (r = n), e.push(t), f(e, 0, e.length - 1, r)
			}, a = function (e, t) {
				var r, i;
				return null == t && (t = n), r = e.pop(), e.length ? (i = e[0], e[0] = r, g(e, 0, t)) : i = r, i
			}, l = function (e, t, r) {
				var i;
				return null == r && (r = n), i = e[0], e[0] = t, g(e, 0, r), i
			}, s = function (e, t, r) {
				var i;
				return null == r && (r = n), e.length && r(e[0], t) < 0 && (t = (i = [e[0], t])[0], e[0] = i[1], g(e, 0, r)), t
			}, i = function (e, t) {
				var i, a, o, s, l, u;
				for (null == t && (t = n), l = [], a = 0, o = (s = function () {
					u = [];
					for (var t = 0, n = r(e.length / 2); 0 <= n ? t < n : t > n; 0 <= n ? t++ : t--) u.push(t);
					return u
				}.apply(this).reverse()).length; a < o; a++) i = s[a], l.push(g(e, i, t));
				return l
			}, p = function (e, t, r) {
				var i;
				if (null == r && (r = n), -1 !== (i = e.indexOf(t))) return f(e, 0, i, r), g(e, i, r)
			}, h = function (e, t, r) {
				var a, o, l, u, c;
				if (null == r && (r = n), !(o = e.slice(0, t)).length) return o;
				for (i(o, r), l = 0, u = (c = e.slice(t)).length; l < u; l++) a = c[l], s(o, a, r);
				return o.sort(r).reverse()
			}, d = function (e, t, r) {
				var o, s, l, h, d, p, f, g, v;
				if (null == r && (r = n), 10 * t <= e.length) {
					if (!(l = e.slice(0, t).sort(r)).length) return l;
					for (s = l[l.length - 1], h = 0, p = (f = e.slice(t)).length; h < p; h++) r(o = f[h], s) < 0 && (u(l, o, 0, null, r), l.pop(), s = l[l.length - 1]);
					return l
				}
				for (i(e, r), v = [], d = 0, g = c(t, e.length); 0 <= g ? d < g : d > g; 0 <= g ? ++d : --d) v.push(a(e, r));
				return v
			}, f = function (e, t, r, i) {
				var a, o, s;
				for (null == i && (i = n), a = e[r]; r > t && i(a, o = e[s = r - 1 >> 1]) < 0;) e[r] = o, r = s;
				return e[r] = a
			}, g = function (e, t, r) {
				var i, a, o, s, l;
				for (null == r && (r = n), a = e.length, l = t, o = e[t], i = 2 * t + 1; i < a;)(s = i + 1) < a && !(r(e[i], e[s]) < 0) && (i = s), e[t] = e[i], i = 2 * (t = i) + 1;
				return e[t] = o, f(e, l, t, r)
			}, t = function () {
				function e(e) {
					this.cmp = null != e ? e : n, this.nodes = []
				}
				return e.push = o, e.pop = a, e.replace = l, e.pushpop = s, e.heapify = i, e.updateItem = p, e.nlargest = h, e.nsmallest = d, e.prototype.push = function (e) {
					return o(this.nodes, e, this.cmp)
				}, e.prototype.pop = function () {
					return a(this.nodes, this.cmp)
				}, e.prototype.peek = function () {
					return this.nodes[0]
				}, e.prototype.contains = function (e) {
					return -1 !== this.nodes.indexOf(e)
				}, e.prototype.replace = function (e) {
					return l(this.nodes, e, this.cmp)
				}, e.prototype.pushpop = function (e) {
					return s(this.nodes, e, this.cmp)
				}, e.prototype.heapify = function () {
					return i(this.nodes, this.cmp)
				}, e.prototype.updateItem = function (e) {
					return p(this.nodes, e, this.cmp)
				}, e.prototype.clear = function () {
					return this.nodes = []
				}, e.prototype.empty = function () {
					return 0 === this.nodes.length
				}, e.prototype.size = function () {
					return this.nodes.length
				}, e.prototype.clone = function () {
					var t;
					return (t = new e).nodes = this.nodes.slice(0), t
				}, e.prototype.toArray = function () {
					return this.nodes.slice(0)
				}, e.prototype.insert = e.prototype.push, e.prototype.top = e.prototype.peek, e.prototype.front = e.prototype.peek, e.prototype.has = e.prototype.contains, e.prototype.copy = e.prototype.clone, e
			}(), e.exports = t
		}).call(R)
	}),
		qe = Me({
			root: null,
			weight: function (e) {
				return 1
			},
			directed: !1
		}),
		Ye = {
			dijkstra: function (e) {
				if (!g(e)) {
					var t = arguments;
					e = {
						root: t[0],
						weight: t[1],
						directed: t[2]
					}
				}
				var n = qe(e),
					r = n.root,
					i = n.weight,
					a = n.directed,
					o = this,
					s = i,
					l = d(r) ? this.filter(r)[0] : r[0],
					u = {},
					c = {},
					h = {},
					p = this.byGroup(),
					f = p.nodes,
					v = p.edges;
				v.unmergeBy(function (e) {
					return e.isLoop()
				});
				for (var y = function (e) {
					return u[e.id()]
				}, m = function (e, t) {
					u[e.id()] = t, b.updateItem(e)
				}, b = new Fe(function (e, t) {
					return y(e) - y(t)
				}), x = 0; x < f.length; x++) {
					var w = f[x];
					u[w.id()] = w.same(l) ? 0 : 1 / 0, b.push(w)
				}
				for (var E = function (e, t) {
					for (var n, r = (a ? e.edgesTo(t) : e.edgesWith(t)).intersect(v), i = 1 / 0, o = 0; o < r.length; o++) {
						var l = r[o],
							u = s(l);
						(u < i || !n) && (i = u, n = l)
					}
					return {
						edge: n,
						dist: i
					}
				}; b.size() > 0;) {
					var k = b.pop(),
						C = y(k),
						S = k.id();
					if (h[S] = C, C !== 1 / 0)
						for (var D = k.neighborhood().intersect(f), P = 0; P < D.length; P++) {
							var T = D[P],
								M = T.id(),
								B = E(k, T),
								_ = C + B.dist;
							_ < y(T) && (m(T, _), c[M] = {
								node: k,
								edge: B.edge
							})
						}
				}
				return {
					distanceTo: function (e) {
						var t = d(e) ? f.filter(e)[0] : e[0];
						return h[t.id()]
					},
					pathTo: function (e) {
						var t = d(e) ? f.filter(e)[0] : e[0],
							n = [],
							r = t,
							i = r.id();
						if (t.length > 0)
							for (n.unshift(t); c[i];) {
								var a = c[i];
								n.unshift(a.edge), n.unshift(a.node), i = (r = a.node).id()
							}
						return o.spawn(n)
					}
				}
			}
		},
		je = {
			kruskal: function (e) {
				e = e || function (e) {
					return 1
				};
				for (var t = this.byGroup(), n = t.nodes, r = t.edges, i = n.length, a = new Array(i), o = n, s = function (e) {
					for (var t = 0; t < a.length; t++) {
						if (a[t].has(e)) return t
					}
				}, l = 0; l < i; l++) a[l] = this.spawn(n[l]);
				for (var u = r.sort(function (t, n) {
					return e(t) - e(n)
				}), c = 0; c < u.length; c++) {
					var h = u[c],
						d = h.source()[0],
						p = h.target()[0],
						f = s(d),
						g = s(p),
						v = a[f],
						y = a[g];
					f !== g && (o.merge(h), v.merge(y), a.splice(g, 1))
				}
				return o
			}
		},
		Xe = Me({
			root: null,
			goal: null,
			weight: function (e) {
				return 1
			},
			heuristic: function (e) {
				return 0
			},
			directed: !1
		}),
		We = {
			aStar: function (e) {
				var t = this.cy(),
					n = Xe(e),
					r = n.root,
					i = n.goal,
					a = n.heuristic,
					o = n.directed,
					s = n.weight;
				r = t.collection(r)[0], i = t.collection(i)[0];
				var l, u, c = r.id(),
					h = i.id(),
					d = {},
					p = {},
					f = {},
					g = new Fe(function (e, t) {
						return p[e.id()] - p[t.id()]
					}),
					v = new Ae,
					y = {},
					m = {},
					b = function (e, t) {
						g.push(e), v.add(t)
					};
				b(r, c), d[c] = 0, p[c] = a(r);
				for (var x, w = 0; g.size() > 0;) {
					if (l = g.pop(), u = l.id(), v.delete(u), w++ , u === h) {
						for (var E = [], k = i, C = h, S = m[C]; E.unshift(k), null != S && E.unshift(S), null != (k = y[C]);) S = m[C = k.id()];
						return {
							found: !0,
							distance: d[u],
							path: this.spawn(E),
							steps: w
						}
					}
					f[u] = !0;
					for (var D = l._private.edges, P = 0; P < D.length; P++) {
						var T = D[P];
						if (this.hasElementWithId(T.id()) && (!o || T.data("source") === u)) {
							var M = T.source(),
								B = T.target(),
								_ = M.id() !== u ? M : B,
								N = _.id();
							if (this.hasElementWithId(N) && !f[N]) {
								var I = d[u] + s(T);
								x = N, v.has(x) ? I < d[N] && (d[N] = I, p[N] = I + a(_), y[N] = l) : (d[N] = I, p[N] = I + a(_), b(_, N), y[N] = l, m[N] = T)
							}
						}
					}
				}
				return {
					found: !1,
					distance: void 0,
					path: void 0,
					steps: w
				}
			}
		},
		He = Me({
			weight: function (e) {
				return 1
			},
			directed: !1
		}),
		Ke = {
			floydWarshall: function (e) {
				for (var t = this.cy(), n = He(e), r = n.weight, i = n.directed, a = r, o = this.byGroup(), s = o.nodes, l = o.edges, u = s.length, c = u * u, h = function (e) {
					return s.indexOf(e)
				}, p = function (e) {
					return s[e]
				}, f = new Array(c), g = 0; g < c; g++) {
					var v = g % u,
						y = (g - v) / u;
					f[g] = y === v ? 0 : 1 / 0
				}
				for (var m = new Array(c), b = new Array(c), x = 0; x < l.length; x++) {
					var w = l[x],
						E = w.source()[0],
						k = w.target()[0];
					if (E !== k) {
						var C = h(E),
							S = h(k),
							D = C * u + S,
							P = a(w);
						if (f[D] > P && (f[D] = P, m[D] = S, b[D] = w), !i) {
							var T = S * u + C;
							!i && f[T] > P && (f[T] = P, m[T] = C, b[T] = w)
						}
					}
				}
				for (var M = 0; M < u; M++)
					for (var B = 0; B < u; B++)
						for (var _ = B * u + M, N = 0; N < u; N++) {
							var I = B * u + N,
								z = M * u + N;
							f[_] + f[z] < f[I] && (f[I] = f[_] + f[z], m[I] = m[_])
						}
				var L = function (e) {
					return h(function (e) {
						return (d(e) ? t.filter(e) : e)[0]
					}(e))
				};
				return {
					distance: function (e, t) {
						var n = L(e),
							r = L(t);
						return f[n * u + r]
					},
					path: function (e, n) {
						var r = L(e),
							i = L(n),
							a = p(r);
						if (r === i) return a.collection();
						if (null == m[r * u + i]) return t.collection();
						var o, s = t.collection(),
							l = r;
						for (s.merge(a); r !== i;) l = r, r = m[r * u + i], o = b[l * u + r], s.merge(o), s.merge(p(r));
						return s
					}
				}
			}
		},
		Ge = Me({
			weight: function (e) {
				return 1
			},
			directed: !1,
			root: null
		}),
		Ze = {
			bellmanFord: function (e) {
				var t = this,
					n = Ge(e),
					r = n.weight,
					i = n.directed,
					a = n.root,
					o = r,
					s = this,
					l = this.cy(),
					u = this.byGroup(),
					c = u.edges,
					h = u.nodes,
					p = h.length,
					f = new ze,
					g = !1;
				a = l.collection(a)[0], c.unmergeBy(function (e) {
					return e.isLoop()
				});
				for (var v = c.length, y = function (e) {
					var t = f.get(e.id());
					return t || (t = {}, f.set(e.id(), t)), t
				}, m = function (e) {
					return (d(e) ? l.$(e) : e)[0]
				}, b = 0; b < p; b++) {
					var x = h[b],
						w = y(x);
					x.same(a) ? w.dist = 0 : w.dist = 1 / 0, w.pred = null, w.edge = null
				}
				for (var E = !1, k = function (e, t, n, r, i, a) {
					var o = r.dist + a;
					o < i.dist && !n.same(r.edge) && (i.dist = o, i.pred = e, i.edge = n, E = !0)
				}, C = 1; C < p; C++) {
					E = !1;
					for (var S = 0; S < v; S++) {
						var D = c[S],
							P = D.source(),
							T = D.target(),
							M = o(D),
							B = y(P),
							_ = y(T);
						k(P, 0, D, B, _, M), i || k(T, 0, D, _, B, M)
					}
					if (!E) break
				}
				if (E)
					for (var N = 0; N < v; N++) {
						var I = c[N],
							z = I.source(),
							L = I.target(),
							A = o(I),
							O = y(z).dist,
							R = y(L).dist;
						if (O + A < R || !i && R + A < O) {
							Ce("Graph contains a negative weight cycle for Bellman-Ford"), g = !0;
							break
						}
					}
				return {
					distanceTo: function (e) {
						return y(m(e)).dist
					},
					pathTo: function (e) {
						for (var n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : a, r = [], i = m(e); ;) {
							if (null == i) return t.spawn();
							var o = y(i),
								l = o.edge,
								u = o.pred;
							if (r.unshift(i[0]), i.same(n) && r.length > 0) break;
							null != l && r.unshift(l), i = u
						}
						return s.spawn(r)
					},
					hasNegativeWeightCycle: g,
					negativeWeightCycles: []
				}
			}
		},
		Ue = Math.sqrt(2),
		$e = function (e, t, n) {
			0 === n.length && Ee("Karger-Stein must be run on a connected (sub)graph");
			for (var r = n[e], i = r[1], a = r[2], o = t[i], s = t[a], l = n, u = l.length - 1; u >= 0; u--) {
				var c = l[u],
					h = c[1],
					d = c[2];
				(t[h] === o && t[d] === s || t[h] === s && t[d] === o) && l.splice(u, 1)
			}
			for (var p = 0; p < l.length; p++) {
				var f = l[p];
				f[1] === s ? (l[p] = f.slice(), l[p][1] = o) : f[2] === s && (l[p] = f.slice(), l[p][2] = o)
			}
			for (var g = 0; g < t.length; g++) t[g] === s && (t[g] = o);
			return l
		},
		Qe = function (e, t, n, r) {
			for (; n > r;) {
				var i = Math.floor(Math.random() * t.length);
				t = $e(i, e, t), n--
			}
			return t
		},
		Je = {
			kargerStein: function () {
				var e = this.byGroup(),
					t = e.nodes,
					n = e.edges;
				n.unmergeBy(function (e) {
					return e.isLoop()
				});
				var r = t.length,
					i = n.length,
					a = Math.ceil(Math.pow(Math.log(r) / Math.LN2, 2)),
					o = Math.floor(r / Ue);
				if (!(r < 2)) {
					for (var s = [], l = 0; l < i; l++) {
						var u = n[l];
						s.push([l, t.indexOf(u.source()), t.indexOf(u.target())])
					}
					for (var c = 1 / 0, h = [], d = new Array(r), p = new Array(r), f = new Array(r), g = function (e, t) {
						for (var n = 0; n < r; n++) t[n] = e[n]
					}, v = 0; v <= a; v++) {
						for (var y = 0; y < r; y++) p[y] = y;
						var m = Qe(p, s.slice(), r, o),
							b = m.slice();
						g(p, f);
						var x = Qe(p, m, o, 2),
							w = Qe(f, b, o, 2);
						x.length <= w.length && x.length < c ? (c = x.length, h = x, g(p, d)) : w.length <= x.length && w.length < c && (c = w.length, h = w, g(f, d))
					}
					for (var E = this.spawn(h.map(function (e) {
						return n[e[0]]
					})), k = this.spawn(), C = this.spawn(), S = d[0], D = 0; D < d.length; D++) {
						var P = d[D],
							T = t[D];
						P === S ? k.merge(T) : C.merge(T)
					}
					return {
						cut: E,
						partition1: k,
						partition2: C
					}
				}
				Ee("At least 2 nodes are required for Karger-Stein algorithm")
			}
		},
		et = function (e) {
			return {
				x: e.x,
				y: e.y
			}
		},
		tt = function (e, t, n) {
			return {
				x: e.x * t + n.x,
				y: e.y * t + n.y
			}
		},
		nt = function (e, t, n) {
			return {
				x: (e.x - n.x) / t,
				y: (e.y - n.y) / t
			}
		},
		rt = function (e) {
			return {
				x: e[0],
				y: e[1]
			}
		},
		it = function (e, t) {
			return Math.atan2(t, e) - Math.PI / 2
		},
		at = Math.log2 || function (e) {
			return Math.log(e) / Math.log(2)
		},
		ot = function (e) {
			return e > 0 ? 1 : e < 0 ? -1 : 0
		},
		st = function (e, t) {
			return Math.sqrt(lt(e, t))
		},
		lt = function (e, t) {
			var n = t.x - e.x,
				r = t.y - e.y;
			return n * n + r * r
		},
		ut = function (e) {
			for (var t = e.length, n = 0, r = 0; r < t; r++) n += e[r];
			for (var i = 0; i < t; i++) e[i] = e[i] / n;
			return e
		},
		ct = function (e, t, n, r) {
			return (1 - r) * (1 - r) * e + 2 * (1 - r) * r * t + r * r * n
		},
		ht = function (e, t, n, r) {
			return {
				x: ct(e.x, t.x, n.x, r),
				y: ct(e.y, t.y, n.y, r)
			}
		},
		dt = function (e, t, n) {
			return Math.max(e, Math.min(n, t))
		},
		pt = function (e) {
			if (null == e) return {
				x1: 1 / 0,
				y1: 1 / 0,
				x2: -1 / 0,
				y2: -1 / 0,
				w: 0,
				h: 0
			};
			if (null != e.x1 && null != e.y1) {
				if (null != e.x2 && null != e.y2 && e.x2 >= e.x1 && e.y2 >= e.y1) return {
					x1: e.x1,
					y1: e.y1,
					x2: e.x2,
					y2: e.y2,
					w: e.x2 - e.x1,
					h: e.y2 - e.y1
				};
				if (null != e.w && null != e.h && e.w >= 0 && e.h >= 0) return {
					x1: e.x1,
					y1: e.y1,
					x2: e.x1 + e.w,
					y2: e.y1 + e.h,
					w: e.w,
					h: e.h
				}
			}
		},
		ft = function (e, t, n) {
			e.x1 = Math.min(e.x1, t), e.x2 = Math.max(e.x2, t), e.w = e.x2 - e.x1, e.y1 = Math.min(e.y1, n), e.y2 = Math.max(e.y2, n), e.h = e.y2 - e.y1
		},
		gt = function (e) {
			var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0;
			return e.x1 -= t, e.x2 += t, e.y1 -= t, e.y2 += t, e.w = e.x2 - e.x1, e.h = e.y2 - e.y1, e
		},
		vt = function (e, t) {
			e.x1 = t.x1, e.y1 = t.y1, e.x2 = t.x2, e.y2 = t.y2, e.w = e.x2 - e.x1, e.h = e.y2 - e.y1
		},
		yt = function (e, t) {
			e.x1 += t.x, e.x2 += t.x, e.y1 += t.y, e.y2 += t.y
		},
		mt = function (e, t) {
			return !(e.x1 > t.x2) && (!(t.x1 > e.x2) && (!(e.x2 < t.x1) && (!(t.x2 < e.x1) && (!(e.y2 < t.y1) && (!(t.y2 < e.y1) && (!(e.y1 > t.y2) && !(t.y1 > e.y2)))))))
		},
		bt = function (e, t, n) {
			return e.x1 <= t && t <= e.x2 && e.y1 <= n && n <= e.y2
		},
		xt = function (e, t) {
			return bt(e, t.x1, t.y1) && bt(e, t.x2, t.y2)
		},
		wt = function (e, t, n, r, i, a, o) {
			var s, l = Vt(i, a),
				u = i / 2,
				c = a / 2,
				h = r - c - o;
			if ((s = It(e, t, n, r, n - u + l - o, h, n + u - l + o, h, !1)).length > 0) return s;
			var d = n + u + o;
			if ((s = It(e, t, n, r, d, r - c + l - o, d, r + c - l + o, !1)).length > 0) return s;
			var p = r + c + o;
			if ((s = It(e, t, n, r, n - u + l - o, p, n + u - l + o, p, !1)).length > 0) return s;
			var f, g = n - u - o;
			if ((s = It(e, t, n, r, g, r - c + l - o, g, r + c - l + o, !1)).length > 0) return s;
			var v = n - u + l,
				y = r - c + l;
			if ((f = _t(e, t, n, r, v, y, l + o)).length > 0 && f[0] <= v && f[1] <= y) return [f[0], f[1]];
			var m = n + u - l,
				b = r - c + l;
			if ((f = _t(e, t, n, r, m, b, l + o)).length > 0 && f[0] >= m && f[1] <= b) return [f[0], f[1]];
			var x = n + u - l,
				w = r + c - l;
			if ((f = _t(e, t, n, r, x, w, l + o)).length > 0 && f[0] >= x && f[1] >= w) return [f[0], f[1]];
			var E = n - u + l,
				k = r + c - l;
			return (f = _t(e, t, n, r, E, k, l + o)).length > 0 && f[0] <= E && f[1] >= k ? [f[0], f[1]] : []
		},
		Et = function (e, t, n, r, i, a, o) {
			var s = o,
				l = Math.min(n, i),
				u = Math.max(n, i),
				c = Math.min(r, a),
				h = Math.max(r, a);
			return l - s <= e && e <= u + s && c - s <= t && t <= h + s
		},
		kt = function (e, t, n, r, i, a, o, s, l) {
			var u = Math.min(n, o, i) - l,
				c = Math.max(n, o, i) + l,
				h = Math.min(r, s, a) - l,
				d = Math.max(r, s, a) + l;
			return !(e < u || e > c || t < h || t > d)
		},
		Ct = function (e, t, n, r, i, a, o, s) {
			var l = [];
			! function (e, t, n, r, i) {
				var a, o, s, l, u, c, h, d;
				s = -27 * (r /= e) + (t /= e) * (9 * (n /= e) - t * t * 2), a = (o = (3 * n - t * t) / 9) * o * o + (s /= 54) * s, i[1] = 0, h = t / 3, a > 0 ? (u = (u = s + Math.sqrt(a)) < 0 ? -Math.pow(-u, 1 / 3) : Math.pow(u, 1 / 3), c = (c = s - Math.sqrt(a)) < 0 ? -Math.pow(-c, 1 / 3) : Math.pow(c, 1 / 3), i[0] = -h + u + c, h += (u + c) / 2, i[4] = i[2] = -h, h = Math.sqrt(3) * (-c + u) / 2, i[3] = h, i[5] = -h) : (i[5] = i[3] = 0, 0 === a ? (d = s < 0 ? -Math.pow(-s, 1 / 3) : Math.pow(s, 1 / 3), i[0] = 2 * d - h, i[4] = i[2] = -(d + h)) : (l = (o = -o) * o * o, l = Math.acos(s / Math.sqrt(l)), d = 2 * Math.sqrt(o), i[0] = -h + d * Math.cos(l / 3), i[2] = -h + d * Math.cos((l + 2 * Math.PI) / 3), i[4] = -h + d * Math.cos((l + 4 * Math.PI) / 3)))
			}(1 * n * n - 4 * n * i + 2 * n * o + 4 * i * i - 4 * i * o + o * o + r * r - 4 * r * a + 2 * r * s + 4 * a * a - 4 * a * s + s * s, 9 * n * i - 3 * n * n - 3 * n * o - 6 * i * i + 3 * i * o + 9 * r * a - 3 * r * r - 3 * r * s - 6 * a * a + 3 * a * s, 3 * n * n - 6 * n * i + n * o - n * e + 2 * i * i + 2 * i * e - o * e + 3 * r * r - 6 * r * a + r * s - r * t + 2 * a * a + 2 * a * t - s * t, 1 * n * i - n * n + n * e - i * e + r * a - r * r + r * t - a * t, l);
			for (var u = [], c = 0; c < 6; c += 2) Math.abs(l[c + 1]) < 1e-7 && l[c] >= 0 && l[c] <= 1 && u.push(l[c]);
			u.push(1), u.push(0);
			for (var h, d, p, f = -1, g = 0; g < u.length; g++) h = Math.pow(1 - u[g], 2) * n + 2 * (1 - u[g]) * u[g] * i + u[g] * u[g] * o, d = Math.pow(1 - u[g], 2) * r + 2 * (1 - u[g]) * u[g] * a + u[g] * u[g] * s, p = Math.pow(h - e, 2) + Math.pow(d - t, 2), f >= 0 ? p < f && (f = p) : f = p;
			return f
		},
		St = function (e, t, n, r, i, a) {
			var o = [e - n, t - r],
				s = [i - n, a - r],
				l = s[0] * s[0] + s[1] * s[1],
				u = o[0] * o[0] + o[1] * o[1],
				c = o[0] * s[0] + o[1] * s[1],
				h = c * c / l;
			return c < 0 ? u : h > l ? (e - i) * (e - i) + (t - a) * (t - a) : u - h
		},
		Dt = function (e, t, n) {
			for (var r, i, a, o, s = 0, l = 0; l < n.length / 2; l++)
				if (r = n[2 * l], i = n[2 * l + 1], l + 1 < n.length / 2 ? (a = n[2 * (l + 1)], o = n[2 * (l + 1) + 1]) : (a = n[2 * (l + 1 - n.length / 2)], o = n[2 * (l + 1 - n.length / 2) + 1]), r == e && a == e);
				else {
					if (!(r >= e && e >= a || r <= e && e <= a)) continue;
					(e - r) / (a - r) * (o - i) + i > t && s++
				}
			return s % 2 != 0
		},
		Pt = function (e, t, n, r, i, a, o, s, l) {
			var u, c = new Array(n.length);
			null != s[0] ? (u = Math.atan(s[1] / s[0]), s[0] < 0 ? u += Math.PI / 2 : u = -u - Math.PI / 2) : u = s;
			for (var h, d = Math.cos(-u), p = Math.sin(-u), f = 0; f < c.length / 2; f++) c[2 * f] = a / 2 * (n[2 * f] * d - n[2 * f + 1] * p), c[2 * f + 1] = o / 2 * (n[2 * f + 1] * d + n[2 * f] * p), c[2 * f] += r, c[2 * f + 1] += i;
			if (l > 0) {
				var g = Mt(c, -l);
				h = Tt(g)
			} else h = c;
			return Dt(e, t, h)
		},
		Tt = function (e) {
			for (var t, n, r, i, a, o, s, l, u = new Array(e.length / 2), c = 0; c < e.length / 4; c++) {
				t = e[4 * c], n = e[4 * c + 1], r = e[4 * c + 2], i = e[4 * c + 3], c < e.length / 4 - 1 ? (a = e[4 * (c + 1)], o = e[4 * (c + 1) + 1], s = e[4 * (c + 1) + 2], l = e[4 * (c + 1) + 3]) : (a = e[0], o = e[1], s = e[2], l = e[3]);
				var h = It(t, n, r, i, a, o, s, l, !0);
				u[2 * c] = h[0], u[2 * c + 1] = h[1]
			}
			return u
		},
		Mt = function (e, t) {
			for (var n, r, i, a, o = new Array(2 * e.length), s = 0; s < e.length / 2; s++) {
				n = e[2 * s], r = e[2 * s + 1], s < e.length / 2 - 1 ? (i = e[2 * (s + 1)], a = e[2 * (s + 1) + 1]) : (i = e[0], a = e[1]);
				var l = a - r,
					u = -(i - n),
					c = Math.sqrt(l * l + u * u),
					h = l / c,
					d = u / c;
				o[4 * s] = n + h * t, o[4 * s + 1] = r + d * t, o[4 * s + 2] = i + h * t, o[4 * s + 3] = a + d * t
			}
			return o
		},
		Bt = function (e, t, n, r, i, a, o) {
			return e -= i, t -= a, (e /= n / 2 + o) * e + (t /= r / 2 + o) * t <= 1
		},
		_t = function (e, t, n, r, i, a, o) {
			var s = [n - e, r - t],
				l = [e - i, t - a],
				u = s[0] * s[0] + s[1] * s[1],
				c = 2 * (l[0] * s[0] + l[1] * s[1]),
				h = c * c - 4 * u * (l[0] * l[0] + l[1] * l[1] - o * o);
			if (h < 0) return [];
			var d = (-c + Math.sqrt(h)) / (2 * u),
				p = (-c - Math.sqrt(h)) / (2 * u),
				f = Math.min(d, p),
				g = Math.max(d, p),
				v = [];
			if (f >= 0 && f <= 1 && v.push(f), g >= 0 && g <= 1 && v.push(g), 0 === v.length) return [];
			var y = v[0] * s[0] + e,
				m = v[0] * s[1] + t;
			return v.length > 1 ? v[0] == v[1] ? [y, m] : [y, m, v[1] * s[0] + e, v[1] * s[1] + t] : [y, m]
		},
		Nt = function (e, t, n) {
			return t <= e && e <= n || n <= e && e <= t ? e : e <= t && t <= n || n <= t && t <= e ? t : n
		},
		It = function (e, t, n, r, i, a, o, s, l) {
			var u = e - i,
				c = n - e,
				h = o - i,
				d = t - a,
				p = r - t,
				f = s - a,
				g = h * d - f * u,
				v = c * d - p * u,
				y = f * c - h * p;
			if (0 !== y) {
				var m = g / y,
					b = v / y;
				return -.001 <= m && m <= 1.001 && -.001 <= b && b <= 1.001 ? [e + m * c, t + m * p] : l ? [e + m * c, t + m * p] : []
			}
			return 0 === g || 0 === v ? Nt(e, n, o) === o ? [o, s] : Nt(e, n, i) === i ? [i, a] : Nt(i, o, n) === n ? [n, r] : [] : []
		},
		zt = function (e, t, n, r, i, a, o, s) {
			var l, u, c, h, d, p, f = [],
				g = new Array(n.length),
				v = !0;
			if (null == a && (v = !1), v) {
				for (var y = 0; y < g.length / 2; y++) g[2 * y] = n[2 * y] * a + r, g[2 * y + 1] = n[2 * y + 1] * o + i;
				if (s > 0) {
					var m = Mt(g, -s);
					u = Tt(m)
				} else u = g
			} else u = n;
			for (var b = 0; b < u.length / 2; b++) c = u[2 * b], h = u[2 * b + 1], b < u.length / 2 - 1 ? (d = u[2 * (b + 1)], p = u[2 * (b + 1) + 1]) : (d = u[0], p = u[1]), 0 !== (l = It(e, t, r, i, c, h, d, p)).length && f.push(l[0], l[1]);
			return f
		},
		Lt = function (e, t, n) {
			var r = [e[0] - t[0], e[1] - t[1]],
				i = Math.sqrt(r[0] * r[0] + r[1] * r[1]),
				a = (i - n) / i;
			return a < 0 && (a = 1e-5), [t[0] + a * r[0], t[1] + a * r[1]]
		},
		At = function (e, t) {
			var n = Rt(e, t);
			return n = Ot(n)
		},
		Ot = function (e) {
			for (var t, n, r = e.length / 2, i = 1 / 0, a = 1 / 0, o = -1 / 0, s = -1 / 0, l = 0; l < r; l++) t = e[2 * l], n = e[2 * l + 1], i = Math.min(i, t), o = Math.max(o, t), a = Math.min(a, n), s = Math.max(s, n);
			for (var u = 2 / (o - i), c = 2 / (s - a), h = 0; h < r; h++) t = e[2 * h] = e[2 * h] * u, n = e[2 * h + 1] = e[2 * h + 1] * c, i = Math.min(i, t), o = Math.max(o, t), a = Math.min(a, n), s = Math.max(s, n);
			if (a < -1)
				for (var d = 0; d < r; d++) n = e[2 * d + 1] = e[2 * d + 1] + (-1 - a);
			return e
		},
		Rt = function (e, t) {
			var n = 1 / e * 2 * Math.PI,
				r = e % 2 == 0 ? Math.PI / 2 + n / 2 : Math.PI / 2;
			r += t;
			for (var i, a = new Array(2 * e), o = 0; o < e; o++) i = o * n + r, a[2 * o] = Math.cos(i), a[2 * o + 1] = Math.sin(-i);
			return a
		},
		Vt = function (e, t) {
			return Math.min(e / 4, t / 4, 8)
		},
		Ft = function (e, t) {
			return {
				heightOffset: Math.min(15, .05 * t),
				widthOffset: Math.min(100, .25 * e),
				ctrlPtOffsetPct: .05
			}
		},
		qt = Me({
			dampingFactor: .8,
			precision: 1e-6,
			iterations: 200,
			weight: function (e) {
				return 1
			}
		}),
		Yt = {
			pageRank: function (e) {
				for (var t = qt(e), n = t.dampingFactor, r = t.precision, i = t.iterations, a = t.weight, o = this._private.cy, s = this.byGroup(), l = s.nodes, u = s.edges, c = l.length, h = c * c, d = u.length, p = new Array(h), f = new Array(c), g = (1 - n) / c, v = 0; v < c; v++) {
					for (var y = 0; y < c; y++) {
						p[v * c + y] = 0
					}
					f[v] = 0
				}
				for (var m = 0; m < d; m++) {
					var b = u[m],
						x = b.data("source"),
						w = b.data("target");
					if (x !== w) {
						var E = l.indexOfId(x),
							k = l.indexOfId(w),
							C = a(b);
						p[k * c + E] += C, f[E] += C
					}
				}
				for (var S = 1 / c + g, D = 0; D < c; D++)
					if (0 === f[D])
						for (var P = 0; P < c; P++) {
							p[P * c + D] = S
						} else
						for (var T = 0; T < c; T++) {
							var M = T * c + D;
							p[M] = p[M] / f[D] + g
						}
				for (var B, _ = new Array(c), N = new Array(c), I = 0; I < c; I++) _[I] = 1;
				for (var z = 0; z < i; z++) {
					for (var L = 0; L < c; L++) N[L] = 0;
					for (var A = 0; A < c; A++)
						for (var O = 0; O < c; O++) {
							var R = A * c + O;
							N[A] += p[R] * _[O]
						}
					ut(N), B = _, _ = N, N = B;
					for (var V = 0, F = 0; F < c; F++) {
						var q = B[F] - _[F];
						V += q * q
					}
					if (V < r) break
				}
				return {
					rank: function (e) {
						return e = o.collection(e)[0], _[l.indexOf(e)]
					}
				}
			}
		},
		jt = Me({
			root: null,
			weight: function (e) {
				return 1
			},
			directed: !1,
			alpha: 0
		}),
		Xt = {
			degreeCentralityNormalized: function (e) {
				e = jt(e);
				var t = this.cy(),
					n = this.nodes(),
					r = n.length;
				if (e.directed) {
					for (var i = {}, a = {}, o = 0, s = 0, l = 0; l < r; l++) {
						var u = n[l],
							c = u.id();
						e.root = u;
						var h = this.degreeCentrality(e);
						o < h.indegree && (o = h.indegree), s < h.outdegree && (s = h.outdegree), i[c] = h.indegree, a[c] = h.outdegree
					}
					return {
						indegree: function (e) {
							return 0 == o ? 0 : (d(e) && (e = t.filter(e)), i[e.id()] / o)
						},
						outdegree: function (e) {
							return 0 === s ? 0 : (d(e) && (e = t.filter(e)), a[e.id()] / s)
						}
					}
				}
				for (var p = {}, f = 0, g = 0; g < r; g++) {
					var v = n[g];
					e.root = v;
					var y = this.degreeCentrality(e);
					f < y.degree && (f = y.degree), p[v.id()] = y.degree
				}
				return {
					degree: function (e) {
						return 0 === f ? 0 : (d(e) && (e = t.filter(e)), p[e.id()] / f)
					}
				}
			},
			degreeCentrality: function (e) {
				e = jt(e);
				var t = this.cy(),
					n = this,
					r = e,
					i = r.root,
					a = r.weight,
					o = r.directed,
					s = r.alpha;
				if (i = t.collection(i)[0], o) {
					for (var l = i.connectedEdges(), u = l.filter(function (e) {
						return e.target().same(i) && n.has(e)
					}), c = l.filter(function (e) {
						return e.source().same(i) && n.has(e)
					}), h = u.length, d = c.length, p = 0, f = 0, g = 0; g < u.length; g++) p += a(u[g]);
					for (var v = 0; v < c.length; v++) f += a(c[v]);
					return {
						indegree: Math.pow(h, 1 - s) * Math.pow(p, s),
						outdegree: Math.pow(d, 1 - s) * Math.pow(f, s)
					}
				}
				for (var y = i.connectedEdges().intersection(n), m = y.length, b = 0, x = 0; x < y.length; x++) b += a(y[x]);
				return {
					degree: Math.pow(m, 1 - s) * Math.pow(b, s)
				}
			}
		};
	Xt.dc = Xt.degreeCentrality, Xt.dcn = Xt.degreeCentralityNormalised = Xt.degreeCentralityNormalized;
	var Wt = Me({
		harmonic: !0,
		weight: function () {
			return 1
		},
		directed: !1,
		root: null
	}),
		Ht = {
			closenessCentralityNormalized: function (e) {
				for (var t = Wt(e), n = t.harmonic, r = t.weight, i = t.directed, a = this.cy(), o = {}, s = 0, l = this.nodes(), u = this.floydWarshall({
					weight: r,
					directed: i
				}), c = 0; c < l.length; c++) {
					for (var h = 0, p = l[c], f = 0; f < l.length; f++)
						if (c !== f) {
							var g = u.distance(p, l[f]);
							h += n ? 1 / g : g
						}
					n || (h = 1 / h), s < h && (s = h), o[p.id()] = h
				}
				return {
					closeness: function (e) {
						return 0 == s ? 0 : (e = d(e) ? a.filter(e)[0].id() : e.id(), o[e] / s)
					}
				}
			},
			closenessCentrality: function (e) {
				var t = Wt(e),
					n = t.root,
					r = t.weight,
					i = t.directed,
					a = t.harmonic;
				n = this.filter(n)[0];
				for (var o = this.dijkstra({
					root: n,
					weight: r,
					directed: i
				}), s = 0, l = this.nodes(), u = 0; u < l.length; u++) {
					var c = l[u];
					if (!c.same(n)) {
						var h = o.distanceTo(c);
						s += a ? 1 / h : h
					}
				}
				return a ? s : 1 / s
			}
		};
	Ht.cc = Ht.closenessCentrality, Ht.ccn = Ht.closenessCentralityNormalised = Ht.closenessCentralityNormalized;
	var Kt = Me({
		weight: null,
		directed: !1
	}),
		Gt = {
			betweennessCentrality: function (e) {
				for (var t = Kt(e), n = t.directed, r = t.weight, i = null != r, a = this.cy(), o = this.nodes(), s = {}, l = {}, u = 0, c = function (e, t) {
					l[e] = t, t > u && (u = t)
				}, h = function (e) {
					return l[e]
				}, d = 0; d < o.length; d++) {
					var p = o[d],
						f = p.id();
					s[f] = n ? p.outgoers().nodes() : p.openNeighborhood().nodes(), c(f, 0)
				}
				for (var g = function (e) {
					for (var t = o[e].id(), n = [], l = {}, u = {}, d = {}, p = new Fe(function (e, t) {
						return d[e] - d[t]
					}), f = 0; f < o.length; f++) {
						var g = o[f].id();
						l[g] = [], u[g] = 0, d[g] = 1 / 0
					}
					for (u[t] = 1, d[t] = 0, p.push(t); !p.empty();) {
						var v = p.pop();
						if (n.push(v), i)
							for (var y = 0; y < s[v].length; y++) {
								var m = s[v][y],
									b = a.getElementById(v),
									x = void 0;
								x = b.edgesTo(m).length > 0 ? b.edgesTo(m)[0] : m.edgesTo(b)[0];
								var w = r(x);
								m = m.id(), d[m] > d[v] + w && (d[m] = d[v] + w, p.nodes.indexOf(m) < 0 ? p.push(m) : p.updateItem(m), u[m] = 0, l[m] = []), d[m] == d[v] + w && (u[m] = u[m] + u[v], l[m].push(v))
							} else
							for (var E = 0; E < s[v].length; E++) {
								var k = s[v][E].id();
								d[k] == 1 / 0 && (p.push(k), d[k] = d[v] + 1), d[k] == d[v] + 1 && (u[k] = u[k] + u[v], l[k].push(v))
							}
					}
					for (var C = {}, S = 0; S < o.length; S++) C[o[S].id()] = 0;
					for (; n.length > 0;)
						for (var D = n.pop(), P = 0; P < l[D].length; P++) {
							var T = l[D][P];
							C[T] = C[T] + u[T] / u[D] * (1 + C[D]), D != o[e].id() && c(D, h(D) + C[D])
						}
				}, v = 0; v < o.length; v++) g(v);
				var y = {
					betweenness: function (e) {
						var t = a.collection(e).id();
						return h(t)
					},
					betweennessNormalized: function (e) {
						if (0 == u) return 0;
						var t = a.collection(e).id();
						return h(t) / u
					}
				};
				return y.betweennessNormalised = y.betweennessNormalized, y
			}
		};
	Gt.bc = Gt.betweennessCentrality;
	var Zt = Me({
		expandFactor: 2,
		inflateFactor: 2,
		multFactor: 1,
		maxIterations: 20,
		attributes: [
			function (e) {
				return 1
			}
		]
	}),
		Ut = function (e, t) {
			for (var n = 0, r = 0; r < t.length; r++) n += t[r](e);
			return n
		},
		$t = function (e, t) {
			for (var n, r = 0; r < t; r++) {
				n = 0;
				for (var i = 0; i < t; i++) n += e[i * t + r];
				for (var a = 0; a < t; a++) e[a * t + r] = e[a * t + r] / n
			}
		},
		Qt = function (e, t, n) {
			for (var r = new Array(n * n), i = 0; i < n; i++) {
				for (var a = 0; a < n; a++) r[i * n + a] = 0;
				for (var o = 0; o < n; o++)
					for (var s = 0; s < n; s++) r[i * n + s] += e[i * n + o] * t[o * n + s]
			}
			return r
		},
		Jt = function (e, t, n) {
			for (var r = e.slice(0), i = 1; i < n; i++) e = Qt(e, r, t);
			return e
		},
		en = function (e, t, n) {
			for (var r = new Array(t * t), i = 0; i < t * t; i++) r[i] = Math.pow(e[i], n);
			return $t(r, t), r
		},
		tn = function (e, t, n, r) {
			for (var i = 0; i < n; i++) {
				if (Math.round(e[i] * Math.pow(10, r)) / Math.pow(10, r) !== Math.round(t[i] * Math.pow(10, r)) / Math.pow(10, r)) return !1
			}
			return !0
		},
		nn = function (e, t) {
			for (var n = 0; n < e.length; n++)
				if (!t[n] || e[n].id() !== t[n].id()) return !1;
			return !0
		},
		rn = function (e) {
			for (var t = this.nodes(), n = this.edges(), r = this.cy(), i = function (e) {
				return Zt(e)
			}(e), a = {}, o = 0; o < t.length; o++) a[t[o].id()] = o;
			for (var s, l = t.length, u = l * l, c = new Array(u), h = 0; h < u; h++) c[h] = 0;
			for (var d = 0; d < n.length; d++) {
				var p = n[d],
					f = a[p.source().id()],
					g = a[p.target().id()],
					v = Ut(p, i.attributes);
				c[f * l + g] += v, c[g * l + f] += v
			} ! function (e, t, n) {
				for (var r = 0; r < t; r++) e[r * t + r] = n
			}(c, l, i.multFactor), $t(c, l);
			for (var y = !0, m = 0; y && m < i.maxIterations;) y = !1, s = Jt(c, l, i.expandFactor), c = en(s, l, i.inflateFactor), tn(c, s, u, 4) || (y = !0), m++;
			var b = function (e, t, n, r) {
				for (var i = [], a = 0; a < t; a++) {
					for (var o = [], s = 0; s < t; s++) Math.round(1e3 * e[a * t + s]) / 1e3 > 0 && o.push(n[s]);
					0 !== o.length && i.push(r.collection(o))
				}
				return i
			}(c, l, t, r);
			return b = function (e) {
				for (var t = 0; t < e.length; t++)
					for (var n = 0; n < e.length; n++) t != n && nn(e[t], e[n]) && e.splice(n, 1);
				return e
			}(b)
		},
		an = {
			markovClustering: rn,
			mcl: rn
		},
		on = function (e) {
			return e
		},
		sn = function (e, t) {
			return Math.abs(t - e)
		},
		ln = function (e, t, n) {
			return e + sn(t, n)
		},
		un = function (e, t, n) {
			return e + Math.pow(n - t, 2)
		},
		cn = function (e) {
			return Math.sqrt(e)
		},
		hn = function (e, t, n) {
			return Math.max(e, sn(t, n))
		},
		dn = function (e, t, n, r, i) {
			for (var a = arguments.length > 5 && void 0 !== arguments[5] ? arguments[5] : on, o = r, s = 0; s < e; s++) o = i(o, t(s), n(s));
			return a(o)
		},
		pn = {
			euclidean: function (e, t, n) {
				return e >= 2 ? dn(e, t, n, 0, un, cn) : dn(e, t, n, 0, ln)
			},
			squaredEuclidean: function (e, t, n) {
				return dn(e, t, n, 0, un)
			},
			manhattan: function (e, t, n) {
				return dn(e, t, n, 0, ln)
			},
			max: function (e, t, n) {
				return dn(e, t, n, -1 / 0, hn)
			}
		};

	function fn(e, t, n, r, i, a) {
		var o;
		return o = p(e) ? e : pn[e] || pn.euclidean, 0 === t && p(e) ? o(i, a) : o(t, n, r, i, a)
	}
	pn["squared-euclidean"] = pn.squaredEuclidean, pn.squaredeuclidean = pn.squaredEuclidean;
	var gn = Me({
		k: 2,
		m: 2,
		sensitivityThreshold: 1e-4,
		distance: "euclidean",
		maxIterations: 10,
		attributes: [],
		testMode: !1,
		testCentroids: null
	}),
		vn = function (e) {
			return gn(e)
		},
		yn = function (e, t, n, r, i) {
			var a = "kMedoids" !== i ? function (e) {
				return n[e]
			} : function (e) {
				return r[e](n)
			},
				o = n,
				s = t;
			return fn(e, r.length, a, function (e) {
				return r[e](t)
			}, o, s)
		},
		mn = function (e, t, n) {
			for (var r = n.length, i = new Array(r), a = new Array(r), o = new Array(t), s = null, l = 0; l < r; l++) i[l] = e.min(n[l]).value, a[l] = e.max(n[l]).value;
			for (var u = 0; u < t; u++) {
				s = [];
				for (var c = 0; c < r; c++) s[c] = Math.random() * (a[c] - i[c]) + i[c];
				o[u] = s
			}
			return o
		},
		bn = function (e, t, n, r, i) {
			for (var a = 1 / 0, o = 0, s = 0; s < t.length; s++) {
				var l = yn(n, e, t[s], r, i);
				l < a && (a = l, o = s)
			}
			return o
		},
		xn = function (e, t, n) {
			for (var r = [], i = null, a = 0; a < t.length; a++) n[(i = t[a]).id()] === e && r.push(i);
			return r
		},
		wn = function (e, t, n) {
			for (var r = 0; r < e.length; r++)
				for (var i = 0; i < e[r].length; i++) {
					if (Math.abs(e[r][i] - t[r][i]) > n) return !1
				}
			return !0
		},
		En = function (e, t, n) {
			for (var r = 0; r < n; r++)
				if (e === t[r]) return !0;
			return !1
		},
		kn = function (e, t) {
			var n = new Array(t);
			if (e.length < 50)
				for (var r = 0; r < t; r++) {
					for (var i = e[Math.floor(Math.random() * e.length)]; En(i, n, r);) i = e[Math.floor(Math.random() * e.length)];
					n[r] = i
				} else
				for (var a = 0; a < t; a++) n[a] = e[Math.floor(Math.random() * e.length)];
			return n
		},
		Cn = function (e, t, n) {
			for (var r = 0, i = 0; i < t.length; i++) r += yn("manhattan", t[i], e, n, "kMedoids");
			return r
		},
		Sn = function (e, t, n, r, i) {
			for (var a, o, s = 0; s < t.length; s++)
				for (var l = 0; l < e.length; l++) r[s][l] = Math.pow(n[s][l], i.m);
			for (var u = 0; u < e.length; u++)
				for (var c = 0; c < i.attributes.length; c++) {
					a = 0, o = 0;
					for (var h = 0; h < t.length; h++) a += r[h][u] * i.attributes[c](t[h]), o += r[h][u];
					e[u][c] = a / o
				}
		},
		Dn = function (e, t, n, r, i) {
			for (var a = 0; a < e.length; a++) t[a] = e[a].slice();
			for (var o, s, l, u = 2 / (i.m - 1), c = 0; c < n.length; c++)
				for (var h = 0; h < r.length; h++) {
					o = 0;
					for (var d = 0; d < n.length; d++) s = yn(i.distance, r[h], n[c], i.attributes, "cmeans"), l = yn(i.distance, r[h], n[d], i.attributes, "cmeans"), o += Math.pow(s / l, u);
					e[h][c] = 1 / o
				}
		},
		Pn = function (e) {
			var t, n, r, i, a = this.cy(),
				o = this.nodes(),
				s = vn(e);
			r = new Array(o.length);
			for (var l = 0; l < o.length; l++) r[l] = new Array(s.k);
			n = new Array(o.length);
			for (var u = 0; u < o.length; u++) n[u] = new Array(s.k);
			for (var c = 0; c < o.length; c++) {
				for (var h = 0, d = 0; d < s.k; d++) n[c][d] = Math.random(), h += n[c][d];
				for (var p = 0; p < s.k; p++) n[c][p] = n[c][p] / h
			}
			t = new Array(s.k);
			for (var f = 0; f < s.k; f++) t[f] = new Array(s.attributes.length);
			i = new Array(o.length);
			for (var g = 0; g < o.length; g++) i[g] = new Array(s.k);
			for (var v = !0, y = 0; v && y < s.maxIterations;) v = !1, Sn(t, o, n, i, s), Dn(n, r, t, o, s), wn(n, r, s.sensitivityThreshold) || (v = !0), y++;
			return {
				clusters: function (e, t, n, r) {
					for (var i, a, o = new Array(n.k), s = 0; s < o.length; s++) o[s] = [];
					for (var l = 0; l < t.length; l++) {
						i = -1 / 0, a = -1;
						for (var u = 0; u < t[0].length; u++) t[l][u] > i && (i = t[l][u], a = u);
						o[a].push(e[l])
					}
					for (var c = 0; c < o.length; c++) o[c] = r.collection(o[c]);
					return o
				}(o, n, s, a),
				degreeOfMembership: n
			}
		},
		Tn = {
			kMeans: function (t) {
				var n, r = this.cy(),
					i = this.nodes(),
					a = null,
					o = vn(t),
					s = new Array(o.k),
					l = {};
				n = o.testMode ? "number" == typeof o.testCentroids ? mn(i, o.k, o.attributes) : "object" === e(o.testCentroids) ? o.testCentroids : mn(i, o.k, o.attributes) : mn(i, o.k, o.attributes);
				for (var u, c, h, d = !0, p = 0; d && p < o.maxIterations;) {
					for (var f = 0; f < i.length; f++) l[(a = i[f]).id()] = bn(a, n, o.distance, o.attributes, "kMeans");
					d = !1;
					for (var g = 0; g < o.k; g++) {
						var v = xn(g, i, l);
						if (0 !== v.length) {
							for (var y = o.attributes.length, m = n[g], b = new Array(y), x = new Array(y), w = 0; w < y; w++) {
								x[w] = 0;
								for (var E = 0; E < v.length; E++) a = v[E], x[w] += o.attributes[w](a);
								b[w] = x[w] / v.length, u = b[w], c = m[w], h = o.sensitivityThreshold, Math.abs(c - u) <= h || (d = !0)
							}
							n[g] = b, s[g] = r.collection(v)
						}
					}
					p++
				}
				return s
			},
			kMedoids: function (t) {
				var n, r, i = this.cy(),
					a = this.nodes(),
					o = null,
					s = vn(t),
					l = new Array(s.k),
					u = {},
					c = new Array(s.k);
				s.testMode ? "number" == typeof s.testCentroids || (n = "object" === e(s.testCentroids) ? s.testCentroids : kn(a, s.k)) : n = kn(a, s.k);
				for (var h = !0, d = 0; h && d < s.maxIterations;) {
					for (var p = 0; p < a.length; p++) u[(o = a[p]).id()] = bn(o, n, s.distance, s.attributes, "kMedoids");
					h = !1;
					for (var f = 0; f < n.length; f++) {
						var g = xn(f, a, u);
						if (0 !== g.length) {
							c[f] = Cn(n[f], g, s.attributes);
							for (var v = 0; v < g.length; v++)(r = Cn(g[v], g, s.attributes)) < c[f] && (c[f] = r, n[f] = g[v], h = !0);
							l[f] = i.collection(g)
						}
					}
					d++
				}
				return l
			},
			fuzzyCMeans: Pn,
			fcm: Pn
		},
		Mn = Me({
			distance: "euclidean",
			linkage: "min",
			mode: "threshold",
			threshold: 1 / 0,
			addDendrogram: !1,
			dendrogramDepth: 0,
			attributes: []
		}),
		Bn = {
			single: "min",
			complete: "max"
		},
		_n = function (e, t, n, r, i) {
			for (var a, o = 0, s = 1 / 0, l = i.attributes, u = function (e, t) {
				return fn(i.distance, l.length, function (t) {
					return l[t](e)
				}, function (e) {
					return l[e](t)
				}, e, t)
			}, c = 0; c < e.length; c++) {
				var h = e[c].key,
					d = n[h][r[h]];
				d < s && (o = h, s = d)
			}
			if ("threshold" === i.mode && s >= i.threshold || "dendrogram" === i.mode && 1 === e.length) return !1;
			var p, f = t[o],
				g = t[r[o]];
			p = "dendrogram" === i.mode ? {
				left: f,
				right: g,
				key: f.key
			} : {
					value: f.value.concat(g.value),
					key: f.key
				}, e[f.index] = p, e.splice(g.index, 1), t[f.key] = p;
			for (var v = 0; v < e.length; v++) {
				var y = e[v];
				f.key === y.key ? a = 1 / 0 : "min" === i.linkage ? (a = n[f.key][y.key], n[f.key][y.key] > n[g.key][y.key] && (a = n[g.key][y.key])) : "max" === i.linkage ? (a = n[f.key][y.key], n[f.key][y.key] < n[g.key][y.key] && (a = n[g.key][y.key])) : a = "mean" === i.linkage ? (n[f.key][y.key] * f.size + n[g.key][y.key] * g.size) / (f.size + g.size) : "dendrogram" === i.mode ? u(y.value, f.value) : u(y.value[0], f.value[0]), n[f.key][y.key] = n[y.key][f.key] = a
			}
			for (var m = 0; m < e.length; m++) {
				var b = e[m].key;
				if (r[b] === f.key || r[b] === g.key) {
					for (var x = b, w = 0; w < e.length; w++) {
						var E = e[w].key;
						n[b][E] < n[b][x] && (x = E)
					}
					r[b] = x
				}
				e[m].index = m
			}
			return f.key = g.key = f.index = g.index = null, !0
		},
		Nn = function e(t, n, r) {
			t && (t.value ? n.push(t.value) : (t.left && e(t.left, n), t.right && e(t.right, n)))
		},
		In = function (e) {
			for (var t = this.cy(), n = this.nodes(), r = function (e) {
				var t = Mn(e),
					n = Bn[t.linkage];
				return null != n && (t.linkage = n), t
			}(e), i = r.attributes, a = function (e, t) {
				return fn(r.distance, i.length, function (t) {
					return i[t](e)
				}, function (e) {
					return i[e](t)
				}, e, t)
			}, o = [], s = [], l = [], u = [], c = 0; c < n.length; c++) {
				var h = {
					value: "dendrogram" === r.mode ? n[c] : [n[c]],
					key: c,
					index: c
				};
				o[c] = h, u[c] = h, s[c] = [], l[c] = 0
			}
			for (var d = 0; d < o.length; d++)
				for (var p = 0; p <= d; p++) {
					var f = void 0;
					f = "dendrogram" === r.mode ? d === p ? 1 / 0 : a(o[d].value, o[p].value) : d === p ? 1 / 0 : a(o[d].value[0], o[p].value[0]), s[d][p] = f, s[p][d] = f, f < s[d][l[d]] && (l[d] = p)
				}
			for (var g, v = _n(o, u, s, l, r); v;) v = _n(o, u, s, l, r);
			return "dendrogram" === r.mode ? (g = function e(t, n, r) {
				if (!t) return [];
				var i = [],
					a = [],
					o = [];
				return 0 === n ? (t.left && Nn(t.left, i), t.right && Nn(t.right, a), o = i.concat(a), [r.collection(o)]) : 1 === n ? t.value ? [r.collection(t.value)] : (t.left && Nn(t.left, i), t.right && Nn(t.right, a), [r.collection(i), r.collection(a)]) : t.value ? [r.collection(t.value)] : (t.left && (i = e(t.left, n - 1, r)), t.right && (a = e(t.right, n - 1, r)), i.concat(a))
			}(o[0], r.dendrogramDepth, t), r.addDendrogram && function e(t, n) {
				if (!t) return "";
				if (t.left && t.right) {
					var r = e(t.left, n),
						i = e(t.right, n),
						a = n.add({
							group: "nodes",
							data: {
								id: r + "," + i
							}
						});
					return n.add({
						group: "edges",
						data: {
							source: r,
							target: a.id()
						}
					}), n.add({
						group: "edges",
						data: {
							source: i,
							target: a.id()
						}
					}), a.id()
				}
				return t.value ? t.value.id() : void 0
			}(o[0], t)) : (g = new Array(o.length), o.forEach(function (e, n) {
				e.key = e.index = null, g[n] = t.collection(e.value)
			})), g
		},
		zn = {
			hierarchicalClustering: In,
			hca: In
		},
		Ln = Me({
			distance: "euclidean",
			preference: "median",
			damping: .8,
			maxIterations: 1e3,
			minIterations: 100,
			attributes: []
		}),
		An = function (e, t, n, r) {
			var i = function (e, t) {
				return r[t](e)
			};
			return -fn(e, r.length, function (e) {
				return i(t, e)
			}, function (e) {
				return i(n, e)
			}, t, n)
		},
		On = function (e, t) {
			return "median" === t ? function (e) {
				var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0,
					n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : e.length,
					r = !(arguments.length > 3 && void 0 !== arguments[3]) || arguments[3],
					i = !(arguments.length > 4 && void 0 !== arguments[4]) || arguments[4],
					a = !(arguments.length > 5 && void 0 !== arguments[5]) || arguments[5];
				r ? e = e.slice(t, n) : (n < e.length && e.splice(n, e.length - n), t > 0 && e.splice(0, t));
				for (var o = 0, s = e.length - 1; s >= 0; s--) {
					var l = e[s];
					a ? isFinite(l) || (e[s] = -1 / 0, o++) : e.splice(s, 1)
				}
				i && e.sort(function (e, t) {
					return e - t
				});
				var u = e.length,
					c = Math.floor(u / 2);
				return u % 2 != 0 ? e[c + 1 + o] : (e[c - 1 + o] + e[c + o]) / 2
			}(e) : "mean" === t ? function (e) {
				for (var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0, n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : e.length, r = 0, i = 0, a = t; a < n; a++) {
					var o = e[a];
					isFinite(o) && (r += o, i++)
				}
				return r / i
			}(e) : "min" === t ? function (e) {
				for (var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0, n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : e.length, r = 1 / 0, i = t; i < n; i++) {
					var a = e[i];
					isFinite(a) && (r = Math.min(a, r))
				}
				return r
			}(e) : "max" === t ? function (e) {
				for (var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0, n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : e.length, r = -1 / 0, i = t; i < n; i++) {
					var a = e[i];
					isFinite(a) && (r = Math.max(a, r))
				}
				return r
			}(e) : t
		},
		Rn = function (e, t, n) {
			for (var r = [], i = 0; i < e; i++) {
				for (var a = -1, o = -1 / 0, s = 0; s < n.length; s++) {
					var l = n[s];
					t[i * e + l] > o && (a = l, o = t[i * e + l])
				}
				a > 0 && r.push(a)
			}
			for (var u = 0; u < n.length; u++) r[n[u]] = n[u];
			return r
		},
		Vn = function (e) {
			for (var t, n, r, i, a, o, s = this.cy(), l = this.nodes(), u = function (e) {
				var t = e.damping,
					n = e.preference; .5 <= t && t < 1 || Ee("Damping must range on [0.5, 1).  Got: ".concat(t));
				var r = ["median", "mean", "min", "max"];
				return r.some(function (e) {
					return e === n
				}) || v(n) || Ee("Preference must be one of [".concat(r.map(function (e) {
					return "'".concat(e, "'")
				}).join(", "), "] or a number.  Got: ").concat(n)), Ln(e)
			}(e), c = {}, h = 0; h < l.length; h++) c[l[h].id()] = h;
			n = (t = l.length) * t, r = new Array(n);
			for (var d = 0; d < n; d++) r[d] = -1 / 0;
			for (var p = 0; p < t; p++)
				for (var f = 0; f < t; f++) p !== f && (r[p * t + f] = An(u.distance, l[p], l[f], u.attributes));
			i = On(r, u.preference);
			for (var g = 0; g < t; g++) r[g * t + g] = i;
			a = new Array(n);
			for (var y = 0; y < n; y++) a[y] = 0;
			o = new Array(n);
			for (var m = 0; m < n; m++) o[m] = 0;
			for (var b = new Array(t), x = new Array(t), w = new Array(t), E = 0; E < t; E++) b[E] = 0, x[E] = 0, w[E] = 0;
			for (var k, C = new Array(t * u.minIterations), S = 0; S < C.length; S++) C[S] = 0;
			for (k = 0; k < u.maxIterations; k++) {
				for (var D = 0; D < t; D++) {
					for (var P = -1 / 0, T = -1 / 0, M = -1, B = 0, _ = 0; _ < t; _++) b[_] = a[D * t + _], (B = o[D * t + _] + r[D * t + _]) >= P ? (T = P, P = B, M = _) : B > T && (T = B);
					for (var N = 0; N < t; N++) a[D * t + N] = (1 - u.damping) * (r[D * t + N] - P) + u.damping * b[N];
					a[D * t + M] = (1 - u.damping) * (r[D * t + M] - T) + u.damping * b[M]
				}
				for (var I = 0; I < t; I++) {
					for (var z = 0, L = 0; L < t; L++) b[L] = o[L * t + I], x[L] = Math.max(0, a[L * t + I]), z += x[L];
					z -= x[I], x[I] = a[I * t + I], z += x[I];
					for (var A = 0; A < t; A++) o[A * t + I] = (1 - u.damping) * Math.min(0, z - x[A]) + u.damping * b[A];
					o[I * t + I] = (1 - u.damping) * (z - x[I]) + u.damping * b[I]
				}
				for (var O = 0, R = 0; R < t; R++) {
					var V = o[R * t + R] + a[R * t + R] > 0 ? 1 : 0;
					C[k % u.minIterations * t + R] = V, O += V
				}
				if (O > 0 && (k >= u.minIterations - 1 || k == u.maxIterations - 1)) {
					for (var F = 0, q = 0; q < t; q++) {
						w[q] = 0;
						for (var Y = 0; Y < u.minIterations; Y++) w[q] += C[Y * t + q];
						0 !== w[q] && w[q] !== u.minIterations || F++
					}
					if (F === t) break
				}
			}
			for (var j = function (e, t, n) {
				for (var r = [], i = 0; i < e; i++) t[i * e + i] + n[i * e + i] > 0 && r.push(i);
				return r
			}(t, a, o), X = function (e, t, n) {
				for (var r = Rn(e, t, n), i = 0; i < n.length; i++) {
					for (var a = [], o = 0; o < r.length; o++) r[o] === n[i] && a.push(o);
					for (var s = -1, l = -1 / 0, u = 0; u < a.length; u++) {
						for (var c = 0, h = 0; h < a.length; h++) c += t[a[h] * e + a[u]];
						c > l && (s = u, l = c)
					}
					n[i] = a[s]
				}
				return r = Rn(e, t, n)
			}(t, r, j), W = {}, H = 0; H < j.length; H++) W[j[H]] = [];
			for (var K = 0; K < l.length; K++) {
				var G = X[c[l[K].id()]];
				null != G && W[G].push(l[K])
			}
			for (var Z = new Array(j.length), U = 0; U < j.length; U++) Z[U] = s.collection(W[j[U]]);
			return Z
		},
		Fn = {};
	[Ve, Ye, je, We, Ke, Ze, Je, Yt, Xt, Ht, Gt, an, Tn, zn, {
		affinityPropagation: Vn,
		ap: Vn
	}].forEach(function (e) {
		I(Fn, e)
	});
	var qn = function e(t) {
		if (!(this instanceof e)) return new e(t);
		this.id = "Thenable/1.0.7", this.state = 0, this.fulfillValue = void 0, this.rejectReason = void 0, this.onFulfilled = [], this.onRejected = [], this.proxy = {
			then: this.then.bind(this)
		}, "function" == typeof t && t.call(this, this.fulfill.bind(this), this.reject.bind(this))
	};
	qn.prototype = {
		fulfill: function (e) {
			return Yn(this, 1, "fulfillValue", e)
		},
		reject: function (e) {
			return Yn(this, 2, "rejectReason", e)
		},
		then: function (e, t) {
			var n = new qn;
			return this.onFulfilled.push(Wn(e, n, "fulfill")), this.onRejected.push(Wn(t, n, "reject")), jn(this), n.proxy
		}
	};
	var Yn = function (e, t, n, r) {
		return 0 === e.state && (e.state = t, e[n] = r, jn(e)), e
	},
		jn = function (e) {
			1 === e.state ? Xn(e, "onFulfilled", e.fulfillValue) : 2 === e.state && Xn(e, "onRejected", e.rejectReason)
		},
		Xn = function (e, t, n) {
			if (0 !== e[t].length) {
				var r = e[t];
				e[t] = [];
				var i = function () {
					for (var e = 0; e < r.length; e++) r[e](n)
				};
				"function" == typeof setImmediate ? setImmediate(i) : setTimeout(i, 0)
			}
		},
		Wn = function (e, t, n) {
			return function (r) {
				if ("function" != typeof e) t[n].call(t, r);
				else {
					var i;
					try {
						i = e(r)
					} catch (e) {
						return void t.reject(e)
					}
					Hn(t, i)
				}
			}
		},
		Hn = function t(n, r) {
			if (n !== r && n.proxy !== r) {
				var i;
				if ("object" === e(r) && null !== r || "function" == typeof r) try {
					i = r.then
				} catch (e) {
					return void n.reject(e)
				}
				if ("function" != typeof i) n.fulfill(r);
				else {
					var a = !1;
					try {
						i.call(r, function (e) {
							a || (a = !0, e === r ? n.reject(new TypeError("circular thenable chain")) : t(n, e))
						}, function (e) {
							a || (a = !0, n.reject(e))
						})
					} catch (e) {
						a || n.reject(e)
					}
				}
			} else n.reject(new TypeError("cannot resolve promise with itself"))
		};
	qn.all = function (e) {
		return new qn(function (t, n) {
			for (var r = new Array(e.length), i = 0, a = function (n, a) {
				r[n] = a, ++i === e.length && t(r)
			}, o = 0; o < e.length; o++)! function (t) {
				var r = e[t];
				null != r && null != r.then ? r.then(function (e) {
					a(t, e)
				}, function (e) {
					n(e)
				}) : a(t, r)
			}(o)
		})
	}, qn.resolve = function (e) {
		return new qn(function (t, n) {
			t(e)
		})
	}, qn.reject = function (e) {
		return new qn(function (t, n) {
			n(e)
		})
	};
	var Kn = "undefined" != typeof Promise ? Promise : qn,
		Gn = function (e, t, n) {
			var r = w(e),
				i = !r,
				a = this._private = I({
					duration: 1e3
				}, t, n);
			if (a.target = e, a.style = a.style || a.css, a.started = !1, a.playing = !1, a.hooked = !1, a.applying = !1, a.progress = 0, a.completes = [], a.frames = [], a.complete && p(a.complete) && a.completes.push(a.complete), i) {
				var o = e.position();
				a.startPosition = a.startPosition || {
					x: o.x,
					y: o.y
				}, a.startStyle = a.startStyle || e.cy().style().getAnimationStartStyle(e, a.style)
			}
			if (r) {
				var s = e.pan();
				a.startPan = {
					x: s.x,
					y: s.y
				}, a.startZoom = e.zoom()
			}
			this.length = 1, this[0] = this
		},
		Zn = Gn.prototype;
	I(Zn, {
		instanceString: function () {
			return "animation"
		},
		hook: function () {
			var e = this._private;
			if (!e.hooked) {
				var t = e.target._private.animation;
				(e.queue ? t.queue : t.current).push(this), m(e.target) && e.target.cy().addToAnimationPool(e.target), e.hooked = !0
			}
			return this
		},
		play: function () {
			var e = this._private;
			return 1 === e.progress && (e.progress = 0), e.playing = !0, e.started = !1, e.stopped = !1, this.hook(), this
		},
		playing: function () {
			return this._private.playing
		},
		apply: function () {
			var e = this._private;
			return e.applying = !0, e.started = !1, e.stopped = !1, this.hook(), this
		},
		applying: function () {
			return this._private.applying
		},
		pause: function () {
			var e = this._private;
			return e.playing = !1, e.started = !1, this
		},
		stop: function () {
			var e = this._private;
			return e.playing = !1, e.started = !1, e.stopped = !0, this
		},
		rewind: function () {
			return this.progress(0)
		},
		fastforward: function () {
			return this.progress(1)
		},
		time: function (e) {
			var t = this._private;
			return void 0 === e ? t.progress * t.duration : this.progress(e / t.duration)
		},
		progress: function (e) {
			var t = this._private,
				n = t.playing;
			return void 0 === e ? t.progress : (n && this.pause(), t.progress = e, t.started = !1, n && this.play(), this)
		},
		completed: function () {
			return 1 === this._private.progress
		},
		reverse: function () {
			var e = this._private,
				t = e.playing;
			t && this.pause(), e.progress = 1 - e.progress, e.started = !1;
			var n = function (t, n) {
				var r = e[t];
				null != r && (e[t] = e[n], e[n] = r)
			};
			if (n("zoom", "startZoom"), n("pan", "startPan"), n("position", "startPosition"), e.style)
				for (var r = 0; r < e.style.length; r++) {
					var i = e.style[r],
						a = i.name,
						o = e.startStyle[a];
					e.startStyle[a] = i, e.style[r] = o
				}
			return t && this.play(), this
		},
		promise: function (e) {
			var t, n = this._private;
			switch (e) {
				case "frame":
					t = n.frames;
					break;
				default:
				case "complete":
				case "completed":
					t = n.completes
			}
			return new Kn(function (e, n) {
				t.push(function () {
					e()
				})
			})
		}
	}), Zn.complete = Zn.completed, Zn.run = Zn.play, Zn.running = Zn.playing;
	var Un = {};
	[{
		animated: function () {
			return function () {
				var e = void 0 !== this.length ? this : [this];
				if (!(this._private.cy || this).styleEnabled()) return !1;
				var t = e[0];
				return t ? t._private.animation.current.length > 0 : void 0
			}
		},
		clearQueue: function () {
			return function () {
				var e = void 0 !== this.length ? this : [this];
				if (!(this._private.cy || this).styleEnabled()) return this;
				for (var t = 0; t < e.length; t++) {
					e[t]._private.animation.queue = []
				}
				return this
			}
		},
		delay: function () {
			return function (e, t) {
				return (this._private.cy || this).styleEnabled() ? this.animate({
					delay: e,
					duration: e,
					complete: t
				}) : this
			}
		},
		delayAnimation: function () {
			return function (e, t) {
				return (this._private.cy || this).styleEnabled() ? this.animation({
					delay: e,
					duration: e,
					complete: t
				}) : this
			}
		},
		animation: function () {
			return function (e, t) {
				var n = void 0 !== this.length,
					r = n ? this : [this],
					i = this._private.cy || this,
					a = !n,
					o = !a;
				if (!i.styleEnabled()) return this;
				var s = i.style();
				if (e = I({}, e, t), 0 === Object.keys(e).length) return new Gn(r[0], e);
				switch (void 0 === e.duration && (e.duration = 400), e.duration) {
					case "slow":
						e.duration = 600;
						break;
					case "fast":
						e.duration = 200
				}
				if (o && (e.style = s.getPropsList(e.style || e.css), e.css = void 0), o && null != e.renderedPosition) {
					var l = e.renderedPosition,
						u = i.pan(),
						c = i.zoom();
					e.position = nt(l, c, u)
				}
				if (a && null != e.panBy) {
					var h = e.panBy,
						d = i.pan();
					e.pan = {
						x: d.x + h.x,
						y: d.y + h.y
					}
				}
				var p = e.center || e.centre;
				if (a && null != p) {
					var f = i.getCenterPan(p.eles, e.zoom);
					null != f && (e.pan = f)
				}
				if (a && null != e.fit) {
					var v = e.fit,
						y = i.getFitViewport(v.eles || v.boundingBox, v.padding);
					null != y && (e.pan = y.pan, e.zoom = y.zoom)
				}
				if (a && g(e.zoom)) {
					var m = i.getZoomedViewport(e.zoom);
					null != m && (m.zoomed && (e.zoom = m.zoom), m.panned && (e.pan = m.pan))
				}
				return new Gn(r[0], e)
			}
		},
		animate: function () {
			return function (e, t) {
				var n = void 0 !== this.length ? this : [this];
				if (!(this._private.cy || this).styleEnabled()) return this;
				t && (e = I({}, e, t));
				for (var r = 0; r < n.length; r++) {
					var i = n[r],
						a = i.animated() && (void 0 === e.queue || e.queue);
					i.animation(e, a ? {
						queue: !0
					} : void 0).play()
				}
				return this
			}
		},
		stop: function () {
			return function (e, t) {
				var n = void 0 !== this.length ? this : [this],
					r = this._private.cy || this;
				if (!r.styleEnabled()) return this;
				for (var i = 0; i < n.length; i++) {
					for (var a = n[i]._private, o = a.animation.current, s = 0; s < o.length; s++) {
						var l = o[s]._private;
						t && (l.duration = 0)
					}
					e && (a.animation.queue = []), t || (a.animation.current = [])
				}
				return r.notify("draw"), this
			}
		}
	}, {
		data: function (e) {
			return e = I({}, {
				field: "data",
				bindingEvent: "data",
				allowBinding: !1,
				allowSetting: !1,
				allowGetting: !1,
				settingEvent: "data",
				settingTriggersEvent: !1,
				triggerFnName: "trigger",
				immutableKeys: {},
				updateStyle: !1,
				beforeGet: function (e) { },
				beforeSet: function (e, t) { },
				onSet: function (e) { },
				canSet: function (e) {
					return !0
				}
			}, e),
				function (t, n) {
					var r = e,
						i = void 0 !== this.length,
						a = i ? this : [this],
						o = i ? this[0] : this;
					if (d(t)) {
						var s;
						if (r.allowGetting && void 0 === n) return o && (r.beforeGet(o), s = o._private[r.field][t]), s;
						if (r.allowSetting && void 0 !== n && !r.immutableKeys[t]) {
							var l = function (e, t, n) {
								return t in e ? Object.defineProperty(e, t, {
									value: n,
									enumerable: !0,
									configurable: !0,
									writable: !0
								}) : e[t] = n, e
							}({}, t, n);
							r.beforeSet(this, l);
							for (var u = 0, c = a.length; u < c; u++) {
								var h = a[u];
								r.canSet(h) && (h._private[r.field][t] = n)
							}
							r.updateStyle && this.updateStyle(), r.onSet(this), r.settingTriggersEvent && this[r.triggerFnName](r.settingEvent)
						}
					} else if (r.allowSetting && g(t)) {
						var f, v, y = t,
							m = Object.keys(y);
						r.beforeSet(this, y);
						for (var b = 0; b < m.length; b++) {
							if (v = y[f = m[b]], !r.immutableKeys[f])
								for (var x = 0; x < a.length; x++) {
									var w = a[x];
									r.canSet(w) && (w._private[r.field][f] = v)
								}
						}
						r.updateStyle && this.updateStyle(), r.onSet(this), r.settingTriggersEvent && this[r.triggerFnName](r.settingEvent)
					} else if (r.allowBinding && p(t)) {
						var E = t;
						this.on(r.bindingEvent, E)
					} else if (r.allowGetting && void 0 === t) {
						var k;
						return o && (r.beforeGet(o), k = o._private[r.field]), k
					}
					return this
				}
		},
		removeData: function (e) {
			return e = I({}, {
				field: "data",
				event: "data",
				triggerFnName: "trigger",
				triggerEvent: !1,
				immutableKeys: {}
			}, e),
				function (t) {
					var n = e,
						r = void 0 !== this.length ? this : [this];
					if (d(t)) {
						for (var i = t.split(/\s+/), a = i.length, o = 0; o < a; o++) {
							var s = i[o];
							if (!k(s))
								if (!n.immutableKeys[s])
									for (var l = 0, u = r.length; l < u; l++) r[l]._private[n.field][s] = void 0
						}
						n.triggerEvent && this[n.triggerFnName](n.event)
					} else if (void 0 === t) {
						for (var c = 0, h = r.length; c < h; c++)
							for (var p = r[c]._private[n.field], f = Object.keys(p), g = 0; g < f.length; g++) {
								var v = f[g];
								!n.immutableKeys[v] && (p[v] = void 0)
							}
						n.triggerEvent && this[n.triggerFnName](n.event)
					}
					return this
				}
		}
	}, {
		eventAliasesOn: function (e) {
			var t = e;
			t.addListener = t.listen = t.bind = t.on, t.unlisten = t.unbind = t.off = t.removeListener, t.trigger = t.emit, t.pon = t.promiseOn = function (e, t) {
				var n = this,
					r = Array.prototype.slice.call(arguments, 0);
				return new Kn(function (e, t) {
					var i = r.concat([
						function (t) {
							n.off.apply(n, a), e(t)
						}
					]),
						a = i.concat([]);
					n.on.apply(n, i)
				})
			}
		}
	}].forEach(function (e) {
		I(Un, e)
	});
	var $n = {
		animate: Un.animate(),
		animation: Un.animation(),
		animated: Un.animated(),
		clearQueue: Un.clearQueue(),
		delay: Un.delay(),
		delayAnimation: Un.delayAnimation(),
		stop: Un.stop()
	},
		Qn = {
			classes: function (e) {
				if (void 0 === e) {
					var t = [];
					return this[0]._private.classes.forEach(function (e) {
						return t.push(e)
					}), t
				}
				f(e) || (e = (e || "").match(/\S+/g) || []);
				for (var n = [], r = new Ae(e), i = 0; i < this.length; i++) {
					for (var a = this[i], o = a._private, s = o.classes, l = !1, u = 0; u < e.length; u++) {
						var c = e[u];
						if (!s.has(c)) {
							l = !0;
							break
						}
					}
					l || (l = s.size !== e.length), l && (o.classes = r, n.push(a))
				}
				return n.length > 0 && this.spawn(n).updateStyle().emit("class"), this
			},
			addClass: function (e) {
				return this.toggleClass(e, !0)
			},
			hasClass: function (e) {
				var t = this[0];
				return null != t && t._private.classes.has(e)
			},
			toggleClass: function (e, t) {
				f(e) || (e = e.match(/\S+/g) || []);
				for (var n = void 0 === t, r = [], i = 0, a = this.length; i < a; i++)
					for (var o = this[i], s = o._private.classes, l = !1, u = 0; u < e.length; u++) {
						var c = e[u],
							h = s.has(c),
							d = !1;
						t || n && !h ? (s.add(c), d = !0) : (!t || n && h) && (s.delete(c), d = !0), !l && d && (r.push(o), l = !0)
					}
				return r.length > 0 && this.spawn(r).updateStyle().emit("class"), this
			},
			removeClass: function (e) {
				return this.toggleClass(e, !1)
			},
			flashClass: function (e, t) {
				var n = this;
				if (null == t) t = 250;
				else if (0 === t) return n;
				return n.addClass(e), setTimeout(function () {
					n.removeClass(e)
				}, t), n
			}
		};
	Qn.className = Qn.classNames = Qn.classes;
	var Jn = {
		metaChar: "[\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\]\\^\\`\\{\\|\\}\\~]",
		comparatorOp: "=|\\!=|>|>=|<|<=|\\$=|\\^=|\\*=",
		boolOp: "\\?|\\!|\\^",
		string: '"(?:\\\\"|[^"])*"|' + "'(?:\\\\'|[^'])*'",
		number: _,
		meta: "degree|indegree|outdegree",
		separator: "\\s*,\\s*",
		descendant: "\\s+",
		child: "\\s+>\\s+",
		subject: "\\$",
		group: "node|edge|\\*",
		directedEdge: "\\s+->\\s+",
		undirectedEdge: "\\s+<->\\s+"
	};
	Jn.variable = "(?:[\\w-]|(?:\\\\" + Jn.metaChar + "))+", Jn.value = Jn.string + "|" + Jn.number, Jn.className = Jn.variable, Jn.id = Jn.variable,
		function () {
			var e, t, n;
			for (e = Jn.comparatorOp.split("|"), n = 0; n < e.length; n++) t = e[n], Jn.comparatorOp += "|@" + t;
			for (e = Jn.comparatorOp.split("|"), n = 0; n < e.length; n++)(t = e[n]).indexOf("!") >= 0 || "=" !== t && (Jn.comparatorOp += "|\\!" + t)
		}();
	var er = 0,
		tr = 1,
		nr = 2,
		rr = 3,
		ir = 4,
		ar = 5,
		or = 6,
		sr = 7,
		lr = 8,
		ur = 9,
		cr = 10,
		hr = 11,
		dr = 12,
		pr = 13,
		fr = 14,
		gr = 15,
		vr = 16,
		yr = 17,
		mr = 18,
		br = 19,
		xr = 20,
		wr = [{
			selector: ":selected",
			matches: function (e) {
				return e.selected()
			}
		}, {
			selector: ":unselected",
			matches: function (e) {
				return !e.selected()
			}
		}, {
			selector: ":selectable",
			matches: function (e) {
				return e.selectable()
			}
		}, {
			selector: ":unselectable",
			matches: function (e) {
				return !e.selectable()
			}
		}, {
			selector: ":locked",
			matches: function (e) {
				return e.locked()
			}
		}, {
			selector: ":unlocked",
			matches: function (e) {
				return !e.locked()
			}
		}, {
			selector: ":visible",
			matches: function (e) {
				return e.visible()
			}
		}, {
			selector: ":hidden",
			matches: function (e) {
				return !e.visible()
			}
		}, {
			selector: ":transparent",
			matches: function (e) {
				return e.transparent()
			}
		}, {
			selector: ":grabbed",
			matches: function (e) {
				return e.grabbed()
			}
		}, {
			selector: ":free",
			matches: function (e) {
				return !e.grabbed()
			}
		}, {
			selector: ":removed",
			matches: function (e) {
				return e.removed()
			}
		}, {
			selector: ":inside",
			matches: function (e) {
				return !e.removed()
			}
		}, {
			selector: ":grabbable",
			matches: function (e) {
				return e.grabbable()
			}
		}, {
			selector: ":ungrabbable",
			matches: function (e) {
				return !e.grabbable()
			}
		}, {
			selector: ":animated",
			matches: function (e) {
				return e.animated()
			}
		}, {
			selector: ":unanimated",
			matches: function (e) {
				return !e.animated()
			}
		}, {
			selector: ":parent",
			matches: function (e) {
				return e.isParent()
			}
		}, {
			selector: ":childless",
			matches: function (e) {
				return e.isChildless()
			}
		}, {
			selector: ":child",
			matches: function (e) {
				return e.isChild()
			}
		}, {
			selector: ":orphan",
			matches: function (e) {
				return e.isOrphan()
			}
		}, {
			selector: ":nonorphan",
			matches: function (e) {
				return e.isChild()
			}
		}, {
			selector: ":compound",
			matches: function (e) {
				return e.isNode() ? e.isParent() : e.source().isParent() || e.target().isParent()
			}
		}, {
			selector: ":loop",
			matches: function (e) {
				return e.isLoop()
			}
		}, {
			selector: ":simple",
			matches: function (e) {
				return e.isSimple()
			}
		}, {
			selector: ":active",
			matches: function (e) {
				return e.active()
			}
		}, {
			selector: ":inactive",
			matches: function (e) {
				return !e.active()
			}
		}, {
			selector: ":backgrounding",
			matches: function (e) {
				return e.backgrounding()
			}
		}, {
			selector: ":nonbackgrounding",
			matches: function (e) {
				return !e.backgrounding()
			}
		}].sort(function (e, t) {
			return function (e, t) {
				return -1 * N(e, t)
			}(e.selector, t.selector)
		}),
		Er = function () {
			for (var e, t = {}, n = 0; n < wr.length; n++) t[(e = wr[n]).selector] = e.matches;
			return t
		}(),
		kr = "(" + wr.map(function (e) {
			return e.selector
		}).join("|") + ")",
		Cr = function (e) {
			return e.replace(new RegExp("\\\\(" + Jn.metaChar + ")", "g"), function (e, t) {
				return t
			})
		},
		Sr = function (e, t, n) {
			e[e.length - 1] = n
		},
		Dr = [{
			name: "group",
			query: !0,
			regex: "(" + Jn.group + ")",
			populate: function (e, t, n) {
				var r = i(n, 1)[0];
				t.checks.push({
					type: er,
					value: "*" === r ? r : r + "s"
				})
			}
		}, {
			name: "state",
			query: !0,
			regex: kr,
			populate: function (e, t, n) {
				var r = i(n, 1)[0];
				t.checks.push({
					type: sr,
					value: r
				})
			}
		}, {
			name: "id",
			query: !0,
			regex: "\\#(" + Jn.id + ")",
			populate: function (e, t, n) {
				var r = i(n, 1)[0];
				t.checks.push({
					type: lr,
					value: Cr(r)
				})
			}
		}, {
			name: "className",
			query: !0,
			regex: "\\.(" + Jn.className + ")",
			populate: function (e, t, n) {
				var r = i(n, 1)[0];
				t.checks.push({
					type: ur,
					value: Cr(r)
				})
			}
		}, {
			name: "dataExists",
			query: !0,
			regex: "\\[\\s*(" + Jn.variable + ")\\s*\\]",
			populate: function (e, t, n) {
				var r = i(n, 1)[0];
				t.checks.push({
					type: ir,
					field: Cr(r)
				})
			}
		}, {
			name: "dataCompare",
			query: !0,
			regex: "\\[\\s*(" + Jn.variable + ")\\s*(" + Jn.comparatorOp + ")\\s*(" + Jn.value + ")\\s*\\]",
			populate: function (e, t, n) {
				var r = i(n, 3),
					a = r[0],
					o = r[1],
					s = r[2];
				s = null != new RegExp("^" + Jn.string + "$").exec(s) ? s.substring(1, s.length - 1) : parseFloat(s), t.checks.push({
					type: rr,
					field: Cr(a),
					operator: o,
					value: s
				})
			}
		}, {
			name: "dataBool",
			query: !0,
			regex: "\\[\\s*(" + Jn.boolOp + ")\\s*(" + Jn.variable + ")\\s*\\]",
			populate: function (e, t, n) {
				var r = i(n, 2),
					a = r[0],
					o = r[1];
				t.checks.push({
					type: ar,
					field: Cr(o),
					operator: a
				})
			}
		}, {
			name: "metaCompare",
			query: !0,
			regex: "\\[\\[\\s*(" + Jn.meta + ")\\s*(" + Jn.comparatorOp + ")\\s*(" + Jn.number + ")\\s*\\]\\]",
			populate: function (e, t, n) {
				var r = i(n, 3),
					a = r[0],
					o = r[1],
					s = r[2];
				t.checks.push({
					type: or,
					field: Cr(a),
					operator: o,
					value: parseFloat(s)
				})
			}
		}, {
			name: "nextQuery",
			separator: !0,
			regex: Jn.separator,
			populate: function (e, t) {
				var n = e.currentSubject,
					r = e.edgeCount,
					i = e.compoundCount,
					a = e[e.length - 1];
				return null != n && (a.subject = n, e.currentSubject = null), a.edgeCount = r, a.compoundCount = i, e.edgeCount = 0, e.compoundCount = 0, e[e.length++] = {
					checks: []
				}
			}
		}, {
			name: "directedEdge",
			separator: !0,
			regex: Jn.directedEdge,
			populate: function (e, t) {
				if (null == e.currentSubject) {
					var n = {
						checks: []
					},
						r = t,
						i = {
							checks: []
						};
					return n.checks.push({
						type: hr,
						source: r,
						target: i
					}), Sr(e, 0, n), e.edgeCount++ , i
				}
				var a = {
					checks: []
				},
					o = t,
					s = {
						checks: []
					};
				return a.checks.push({
					type: dr,
					source: o,
					target: s
				}), Sr(e, 0, a), e.edgeCount++ , s
			}
		}, {
			name: "undirectedEdge",
			separator: !0,
			regex: Jn.undirectedEdge,
			populate: function (e, t) {
				if (null == e.currentSubject) {
					var n = {
						checks: []
					},
						r = t,
						i = {
							checks: []
						};
					return n.checks.push({
						type: cr,
						nodes: [r, i]
					}), Sr(e, 0, n), e.edgeCount++ , i
				}
				var a = {
					checks: []
				},
					o = t,
					s = {
						checks: []
					};
				return a.checks.push({
					type: fr,
					node: o,
					neighbor: s
				}), Sr(e, 0, a), s
			}
		}, {
			name: "child",
			separator: !0,
			regex: Jn.child,
			populate: function (e, t) {
				if (null == e.currentSubject) {
					var n = {
						checks: []
					},
						r = {
							checks: []
						},
						i = e[e.length - 1];
					return n.checks.push({
						type: gr,
						parent: i,
						child: r
					}), Sr(e, 0, n), e.compoundCount++ , r
				}
				if (e.currentSubject === t) {
					var a = {
						checks: []
					},
						o = e[e.length - 1],
						s = {
							checks: []
						},
						l = {
							checks: []
						},
						u = {
							checks: []
						},
						c = {
							checks: []
						};
					return a.checks.push({
						type: br,
						left: o,
						right: s,
						subject: l
					}), l.checks = t.checks, t.checks = [{
						type: xr
					}], c.checks.push({
						type: xr
					}), s.checks.push({
						type: yr,
						parent: c,
						child: u
					}), Sr(e, 0, a), e.currentSubject = l, e.compoundCount++ , u
				}
				var h = {
					checks: []
				},
					d = {
						checks: []
					},
					p = [{
						type: yr,
						parent: h,
						child: d
					}];
				return h.checks = t.checks, t.checks = p, e.compoundCount++ , d
			}
		}, {
			name: "descendant",
			separator: !0,
			regex: Jn.descendant,
			populate: function (e, t) {
				if (null == e.currentSubject) {
					var n = {
						checks: []
					},
						r = {
							checks: []
						},
						i = e[e.length - 1];
					return n.checks.push({
						type: vr,
						ancestor: i,
						descendant: r
					}), Sr(e, 0, n), e.compoundCount++ , r
				}
				if (e.currentSubject === t) {
					var a = {
						checks: []
					},
						o = e[e.length - 1],
						s = {
							checks: []
						},
						l = {
							checks: []
						},
						u = {
							checks: []
						},
						c = {
							checks: []
						};
					return a.checks.push({
						type: br,
						left: o,
						right: s,
						subject: l
					}), l.checks = t.checks, t.checks = [{
						type: xr
					}], c.checks.push({
						type: xr
					}), s.checks.push({
						type: mr,
						ancestor: c,
						descendant: u
					}), Sr(e, 0, a), e.currentSubject = l, e.compoundCount++ , u
				}
				var h = {
					checks: []
				},
					d = {
						checks: []
					},
					p = [{
						type: mr,
						ancestor: h,
						descendant: d
					}];
				return h.checks = t.checks, t.checks = p, e.compoundCount++ , d
			}
		}, {
			name: "subject",
			modifier: !0,
			regex: Jn.subject,
			populate: function (e, t) {
				if (null != e.currentSubject && e.currentSubject !== t) return Ce("Redefinition of subject in selector `" + e.toString() + "`"), !1;
				e.currentSubject = t;
				var n = e[e.length - 1].checks[0],
					r = null == n ? null : n.type;
				r === hr ? n.type = pr : r === cr && (n.type = fr, n.node = n.nodes[1], n.neighbor = n.nodes[0], n.nodes = null)
			}
		}];
	Dr.forEach(function (e) {
		return e.regexObj = new RegExp("^" + e.regex)
	});
	var Pr = function (e) {
		for (var t, n, r, i = 0; i < Dr.length; i++) {
			var a = Dr[i],
				o = a.name,
				s = e.match(a.regexObj);
			if (null != s) {
				n = s, t = a, r = o;
				var l = s[0];
				e = e.substring(l.length);
				break
			}
		}
		return {
			expr: t,
			match: n,
			name: r,
			remaining: e
		}
	},
		Tr = {
			parse: function (e) {
				var t = this.inputText = e,
					n = this[0] = {
						checks: []
					};
				for (this.length = 1, t = function (e) {
					var t = e.match(/^\s+/);
					if (t) {
						var n = t[0];
						e = e.substring(n.length)
					}
					return e
				}(t); ;) {
					var r = Pr(t);
					if (null == r.expr) return Ce("The selector `" + e + "`is invalid"), !1;
					var i = r.match.slice(1),
						a = r.expr.populate(this, n, i);
					if (!1 === a) return !1;
					if (null != a && (n = a), (t = r.remaining).match(/^\s*$/)) break
				}
				var o = this[this.length - 1];
				null != this.currentSubject && (o.subject = this.currentSubject), o.edgeCount = this.edgeCount, o.compoundCount = this.compoundCount;
				for (var s = 0; s < this.length; s++) {
					var l = this[s];
					if (l.compoundCount > 0 && l.edgeCount > 0) return Ce("The selector `" + e + "` is invalid because it uses both a compound selector and an edge selector"), !1;
					if (l.edgeCount > 1) return Ce("The selector `" + e + "` is invalid because it uses multiple edge selectors"), !1;
					1 === l.edgeCount && Ce("The selector `" + e + "` is deprecated.  Edge selectors do not take effect on changes to source and target nodes after an edge is added, for performance reasons.  Use a class or data selector on edges instead, updating the class or data of an edge when your app detects a change in source or target nodes.")
				}
				return !0
			},
			toString: function () {
				if (null != this.toStringCache) return this.toStringCache;
				for (var e = function (e) {
					return null == e ? "" : e
				}, t = function (t) {
					return d(t) ? '"' + t + '"' : e(t)
				}, n = function (e) {
					return " " + e + " "
				}, r = function (r, a) {
					var o = r.type,
						s = r.value;
					switch (o) {
						case er:
							var l = e(s);
							return l.substring(0, l.length - 1);
						case rr:
							var u = r.field,
								c = r.operator;
							return "[" + u + n(e(c)) + t(s) + "]";
						case ar:
							var h = r.operator,
								d = r.field;
							return "[" + e(h) + d + "]";
						case ir:
							return "[" + r.field + "]";
						case or:
							var p = r.operator;
							return "[[" + r.field + n(e(p)) + t(s) + "]]";
						case sr:
							return s;
						case lr:
							return "#" + s;
						case ur:
							return "." + s;
						case yr:
						case gr:
							return i(r.parent, a) + n(">") + i(r.child, a);
						case mr:
						case vr:
							return i(r.ancestor, a) + " " + i(r.descendant, a);
						case br:
							var f = i(r.left, a),
								g = i(r.subject, a),
								v = i(r.right, a);
							return f + (f.length > 0 ? " " : "") + g + v;
						case xr:
							return ""
					}
				}, i = function (e, t) {
					return e.checks.reduce(function (n, i, a) {
						return n + (t === e && 0 === a ? "$" : "") + r(i, t)
					}, "")
				}, a = "", o = 0; o < this.length; o++) {
					var s = this[o];
					a += i(s, s.subject), this.length > 1 && o < this.length - 1 && (a += ", ")
				}
				return this.toStringCache = a, a
			}
		},
		Mr = function (e, t, n) {
			var r, i, a, o = d(e),
				s = v(e),
				l = d(n),
				u = !1,
				c = !1,
				h = !1;
			switch (t.indexOf("!") >= 0 && (t = t.replace("!", ""), c = !0), t.indexOf("@") >= 0 && (t = t.replace("@", ""), u = !0), (o || l || u) && (i = o || s ? "" + e : "", a = "" + n), u && (e = i = i.toLowerCase(), n = a = a.toLowerCase()), t) {
				case "*=":
					r = i.indexOf(a) >= 0;
					break;
				case "$=":
					r = i.indexOf(a, i.length - a.length) >= 0;
					break;
				case "^=":
					r = 0 === i.indexOf(a);
					break;
				case "=":
					r = e === n;
					break;
				case ">":
					h = !0, r = e > n;
					break;
				case ">=":
					h = !0, r = e >= n;
					break;
				case "<":
					h = !0, r = e < n;
					break;
				case "<=":
					h = !0, r = e <= n;
					break;
				default:
					r = !1
			}
			return !c || null == e && h || (r = !r), r
		},
		Br = function (e, t) {
			return e.data(t)
		},
		_r = [],
		Nr = function (e, t) {
			return e.checks.every(function (e) {
				return _r[e.type](e, t)
			})
		};
	_r[er] = function (e, t) {
		var n = e.value;
		return "*" === n || n === t.group()
	}, _r[sr] = function (e, t) {
		return function (e, t) {
			return Er[e](t)
		}(e.value, t)
	}, _r[lr] = function (e, t) {
		var n = e.value;
		return t.id() === n
	}, _r[ur] = function (e, t) {
		var n = e.value;
		return t.hasClass(n)
	}, _r[or] = function (e, t) {
		var n = e.field,
			r = e.operator,
			i = e.value;
		return Mr(function (e, t) {
			return e[t]()
		}(t, n), r, i)
	}, _r[rr] = function (e, t) {
		var n = e.field,
			r = e.operator,
			i = e.value;
		return Mr(Br(t, n), r, i)
	}, _r[ar] = function (e, t) {
		var n = e.field,
			r = e.operator;
		return function (e, t) {
			switch (t) {
				case "?":
					return !!e;
				case "!":
					return !e;
				case "^":
					return void 0 === e
			}
		}(Br(t, n), r)
	}, _r[ir] = function (e, t) {
		var n = e.field;
		e.operator;
		return void 0 !== Br(t, n)
	}, _r[cr] = function (e, t) {
		var n = e.nodes[0],
			r = e.nodes[1],
			i = t.source(),
			a = t.target();
		return Nr(n, i) && Nr(r, a) || Nr(r, i) && Nr(n, a)
	}, _r[fr] = function (e, t) {
		return Nr(e.node, t) && t.neighborhood().some(function (t) {
			return t.isNode() && Nr(e.neighbor, t)
		})
	}, _r[hr] = function (e, t) {
		return Nr(e.source, t.source()) && Nr(e.target, t.target())
	}, _r[dr] = function (e, t) {
		return Nr(e.source, t) && t.outgoers().some(function (t) {
			return t.isNode() && Nr(e.target, t)
		})
	}, _r[pr] = function (e, t) {
		return Nr(e.target, t) && t.incomers().some(function (t) {
			return t.isNode() && Nr(e.source, t)
		})
	}, _r[gr] = function (e, t) {
		return Nr(e.child, t) && Nr(e.parent, t.parent())
	}, _r[yr] = function (e, t) {
		return Nr(e.parent, t) && t.children().some(function (t) {
			return Nr(e.child, t)
		})
	}, _r[vr] = function (e, t) {
		return Nr(e.descendant, t) && t.ancestors().some(function (t) {
			return Nr(e.ancestor, t)
		})
	}, _r[mr] = function (e, t) {
		return Nr(e.ancestor, t) && t.descendants().some(function (t) {
			return Nr(e.descendant, t)
		})
	}, _r[br] = function (e, t) {
		return Nr(e.subject, t) && Nr(e.left, t) && Nr(e.right, t)
	}, _r[xr] = function () {
		return !0
	}, _r[tr] = function (e, t) {
		return e.value.has(t)
	}, _r[nr] = function (e, t) {
		return (0, e.value)(t)
	};
	var Ir = function (e) {
		this.inputText = e, this.currentSubject = null, this.compoundCount = 0, this.edgeCount = 0, this.length = 0, null == e || d(e) && e.match(/^\s*$/) || (m(e) ? this.addQuery({
			checks: [{
				type: tr,
				value: e.collection()
			}]
		}) : p(e) ? this.addQuery({
			checks: [{
				type: nr,
				value: e
			}]
		}) : d(e) ? this.parse(e) || (this.invalid = !0) : Ee("A selector must be created from a string; found "))
	},
		zr = Ir.prototype;
	[Tr, {
		matches: function (e) {
			for (var t = 0; t < this.length; t++) {
				var n = this[t];
				if (Nr(n, e)) return !0
			}
			return !1
		},
		filter: function (e) {
			var t = this;
			if (1 === t.length && 1 === t[0].checks.length && t[0].checks[0].type === lr) return e.getElementById(t[0].checks[0].value).collection();
			var n = function (e) {
				for (var n = 0; n < t.length; n++) {
					var r = t[n];
					if (Nr(r, e)) return !0
				}
				return !1
			};
			return null == t.text() && (n = function () {
				return !0
			}), e.filter(n)
		}
	}].forEach(function (e) {
		return I(zr, e)
	}), zr.text = function () {
		return this.inputText
	}, zr.size = function () {
		return this.length
	}, zr.eq = function (e) {
		return this[e]
	}, zr.sameText = function (e) {
		return !this.invalid && !e.invalid && this.text() === e.text()
	}, zr.addQuery = function (e) {
		this[this.length++] = e
	}, zr.selector = zr.toString;
	var Lr = {
		allAre: function (e) {
			var t = new Ir(e);
			return this.every(function (e) {
				return t.matches(e)
			})
		},
		is: function (e) {
			var t = new Ir(e);
			return this.some(function (e) {
				return t.matches(e)
			})
		},
		some: function (e, t) {
			for (var n = 0; n < this.length; n++) {
				if (t ? e.apply(t, [this[n], n, this]) : e(this[n], n, this)) return !0
			}
			return !1
		},
		every: function (e, t) {
			for (var n = 0; n < this.length; n++) {
				if (!(t ? e.apply(t, [this[n], n, this]) : e(this[n], n, this))) return !1
			}
			return !0
		},
		same: function (e) {
			if (this === e) return !0;
			e = this.cy().collection(e);
			var t = this.length;
			return t === e.length && (1 === t ? this[0] === e[0] : this.every(function (t) {
				return e.hasElementWithId(t.id())
			}))
		},
		anySame: function (e) {
			return e = this.cy().collection(e), this.some(function (t) {
				return e.hasElementWithId(t.id())
			})
		},
		allAreNeighbors: function (e) {
			e = this.cy().collection(e);
			var t = this.neighborhood();
			return e.every(function (e) {
				return t.hasElementWithId(e.id())
			})
		},
		contains: function (e) {
			e = this.cy().collection(e);
			var t = this;
			return e.every(function (e) {
				return t.hasElementWithId(e.id())
			})
		}
	};
	Lr.allAreNeighbours = Lr.allAreNeighbors, Lr.has = Lr.contains, Lr.equal = Lr.equals = Lr.same;
	var Ar, Or, Rr = function (e, t) {
		return function (n, r, i, a) {
			var o, s = n;
			if (null == s ? o = "" : m(s) && 1 === s.length && (o = s.id()), 1 === this.length && o) {
				var l = this[0]._private,
					u = l.traversalCache = l.traversalCache || {},
					c = u[t] = u[t] || [],
					h = he(o),
					d = c[h];
				return d || (c[h] = e.call(this, n, r, i, a))
			}
			return e.call(this, n, r, i, a)
		}
	},
		Vr = {
			parent: function (e) {
				var t = [];
				if (1 === this.length) {
					var n = this[0]._private.parent;
					if (n) return n
				}
				for (var r = 0; r < this.length; r++) {
					var i = this[r]._private.parent;
					i && t.push(i)
				}
				return this.spawn(t, {
					unique: !0
				}).filter(e)
			},
			parents: function (e) {
				for (var t = [], n = this.parent(); n.nonempty();) {
					for (var r = 0; r < n.length; r++) {
						var i = n[r];
						t.push(i)
					}
					n = n.parent()
				}
				return this.spawn(t, {
					unique: !0
				}).filter(e)
			},
			commonAncestors: function (e) {
				for (var t, n = 0; n < this.length; n++) {
					var r = this[n].parents();
					t = (t = t || r).intersect(r)
				}
				return t.filter(e)
			},
			orphans: function (e) {
				return this.stdFilter(function (e) {
					return e.isOrphan()
				}).filter(e)
			},
			nonorphans: function (e) {
				return this.stdFilter(function (e) {
					return e.isChild()
				}).filter(e)
			},
			children: Rr(function (e) {
				for (var t = [], n = 0; n < this.length; n++)
					for (var r = this[n]._private.children, i = 0; i < r.length; i++) t.push(r[i]);
				return this.spawn(t, {
					unique: !0
				}).filter(e)
			}, "children"),
			siblings: function (e) {
				return this.parent().children().not(this).filter(e)
			},
			isParent: function () {
				var e = this[0];
				if (e) return e.isNode() && 0 !== e._private.children.length
			},
			isChildless: function () {
				var e = this[0];
				if (e) return e.isNode() && 0 === e._private.children.length
			},
			isChild: function () {
				var e = this[0];
				if (e) return e.isNode() && null != e._private.parent
			},
			isOrphan: function () {
				var e = this[0];
				if (e) return e.isNode() && null == e._private.parent
			},
			descendants: function (e) {
				var t = [];
				return function e(n) {
					for (var r = 0; r < n.length; r++) {
						var i = n[r];
						t.push(i), i.children().nonempty() && e(i.children())
					}
				}(this.children()), this.spawn(t, {
					unique: !0
				}).filter(e)
			}
		};

	function Fr(e, t, n, r) {
		for (var i = [], a = new Ae, o = e.cy().hasCompoundNodes(), s = 0; s < e.length; s++) {
			var l = e[s];
			n ? i.push(l) : o && r(i, a, l)
		}
		for (; i.length > 0;) {
			var u = i.shift();
			t(u), a.add(u.id()), o && r(i, a, u)
		}
		return e
	}

	function qr(e, t, n) {
		if (n.isParent())
			for (var r = n._private.children, i = 0; i < r.length; i++) {
				var a = r[i];
				t.has(a.id()) || e.push(a)
			}
	}

	function Yr(e, t, n) {
		if (n.isChild()) {
			var r = n._private.parent;
			t.has(r.id()) || e.push(r)
		}
	}

	function jr(e, t, n) {
		Yr(e, t, n), qr(e, t, n)
	}
	Vr.forEachDown = function (e) {
		return Fr(this, e, !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1], qr)
	}, Vr.forEachUp = function (e) {
		return Fr(this, e, !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1], Yr)
	}, Vr.forEachUpAndDown = function (e) {
		return Fr(this, e, !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1], jr)
	}, Vr.ancestors = Vr.parents, (Ar = Or = {
		data: Un.data({
			field: "data",
			bindingEvent: "data",
			allowBinding: !0,
			allowSetting: !0,
			settingEvent: "data",
			settingTriggersEvent: !0,
			triggerFnName: "trigger",
			allowGetting: !0,
			immutableKeys: {
				id: !0,
				source: !0,
				target: !0,
				parent: !0
			},
			updateStyle: !0
		}),
		removeData: Un.removeData({
			field: "data",
			event: "data",
			triggerFnName: "trigger",
			triggerEvent: !0,
			immutableKeys: {
				id: !0,
				source: !0,
				target: !0,
				parent: !0
			},
			updateStyle: !0
		}),
		scratch: Un.data({
			field: "scratch",
			bindingEvent: "scratch",
			allowBinding: !0,
			allowSetting: !0,
			settingEvent: "scratch",
			settingTriggersEvent: !0,
			triggerFnName: "trigger",
			allowGetting: !0,
			updateStyle: !0
		}),
		removeScratch: Un.removeData({
			field: "scratch",
			event: "scratch",
			triggerFnName: "trigger",
			triggerEvent: !0,
			updateStyle: !0
		}),
		rscratch: Un.data({
			field: "rscratch",
			allowBinding: !1,
			allowSetting: !0,
			settingTriggersEvent: !1,
			allowGetting: !0
		}),
		removeRscratch: Un.removeData({
			field: "rscratch",
			triggerEvent: !1
		}),
		id: function () {
			var e = this[0];
			if (e) return e._private.data.id
		}
	}).attr = Ar.data, Ar.removeAttr = Ar.removeData;
	var Xr, Wr, Hr = Or,
		Kr = {};

	function Gr(e) {
		return function (t) {
			if (void 0 === t && (t = !0), 0 !== this.length && this.isNode() && !this.removed()) {
				for (var n = 0, r = this[0], i = r._private.edges, a = 0; a < i.length; a++) {
					var o = i[a];
					!t && o.isLoop() || (n += e(r, o))
				}
				return n
			}
		}
	}

	function Zr(e, t) {
		return function (n) {
			for (var r, i = this.nodes(), a = 0; a < i.length; a++) {
				var o = i[a][e](n);
				void 0 === o || void 0 !== r && !t(o, r) || (r = o)
			}
			return r
		}
	}
	I(Kr, {
		degree: Gr(function (e, t) {
			return t.source().same(t.target()) ? 2 : 1
		}),
		indegree: Gr(function (e, t) {
			return t.target().same(e) ? 1 : 0
		}),
		outdegree: Gr(function (e, t) {
			return t.source().same(e) ? 1 : 0
		})
	}), I(Kr, {
		minDegree: Zr("degree", function (e, t) {
			return e < t
		}),
		maxDegree: Zr("degree", function (e, t) {
			return e > t
		}),
		minIndegree: Zr("indegree", function (e, t) {
			return e < t
		}),
		maxIndegree: Zr("indegree", function (e, t) {
			return e > t
		}),
		minOutdegree: Zr("outdegree", function (e, t) {
			return e < t
		}),
		maxOutdegree: Zr("outdegree", function (e, t) {
			return e > t
		})
	}), I(Kr, {
		totalDegree: function (e) {
			for (var t = 0, n = this.nodes(), r = 0; r < n.length; r++) t += n[r].degree(e);
			return t
		}
	});
	var Ur = function (e, t, n) {
		for (var r = 0; r < e.length; r++) {
			var i = e[r];
			if (!i.locked()) {
				var a = i._private.position,
					o = {
						x: null != t.x ? t.x - a.x : 0,
						y: null != t.y ? t.y - a.y : 0
					};
				!i.isParent() || 0 === o.x && 0 === o.y || i.children().shift(o, n), i.shiftCachedBoundingBox(o)
			}
		}
	},
		$r = {
			field: "position",
			bindingEvent: "position",
			allowBinding: !0,
			allowSetting: !0,
			settingEvent: "position",
			settingTriggersEvent: !0,
			triggerFnName: "emitAndNotify",
			allowGetting: !0,
			validKeys: ["x", "y"],
			beforeGet: function (e) {
				e.updateCompoundBounds()
			},
			beforeSet: function (e, t) {
				Ur(e, t, !1)
			},
			onSet: function (e) {
				e.dirtyCompoundBoundsCache()
			},
			canSet: function (e) {
				return !e.locked()
			}
		};
	(Xr = Wr = {
		position: Un.data($r),
		silentPosition: Un.data(I({}, $r, {
			allowBinding: !1,
			allowSetting: !0,
			settingTriggersEvent: !1,
			allowGetting: !1,
			beforeSet: function (e, t) {
				Ur(e, t, !0)
			}
		})),
		positions: function (e, t) {
			if (g(e)) t ? this.silentPosition(e) : this.position(e);
			else if (p(e)) {
				var n = e,
					r = this.cy();
				r.startBatch();
				for (var i = 0; i < this.length; i++) {
					var a, o = this[i];
					(a = n(o, i)) && (t ? o.silentPosition(a) : o.position(a))
				}
				r.endBatch()
			}
			return this
		},
		silentPositions: function (e) {
			return this.positions(e, !0)
		},
		shift: function (e, t, n) {
			var r;
			if (g(e) ? (r = {
				x: v(e.x) ? e.x : 0,
				y: v(e.y) ? e.y : 0
			}, n = t) : d(e) && v(t) && ((r = {
				x: 0,
				y: 0
			})[e] = t), null != r) {
				var i = this.cy();
				i.startBatch();
				for (var a = 0; a < this.length; a++) {
					var o = this[a],
						s = o.position(),
						l = {
							x: s.x + r.x,
							y: s.y + r.y
						};
					n ? o.silentPosition(l) : o.position(l)
				}
				i.endBatch()
			}
			return this
		},
		silentShift: function (e, t) {
			return g(e) ? this.shift(e, !0) : d(e) && v(t) && this.shift(e, t, !0), this
		},
		renderedPosition: function (e, t) {
			var n = this[0],
				r = this.cy(),
				i = r.zoom(),
				a = r.pan(),
				o = g(e) ? e : void 0,
				s = void 0 !== o || void 0 !== t && d(e);
			if (n && n.isNode()) {
				if (!s) {
					var l = n.position();
					return o = tt(l, i, a), void 0 === e ? o : o[e]
				}
				for (var u = 0; u < this.length; u++) {
					var c = this[u];
					void 0 !== t ? c.position(e, (t - a[e]) / i) : void 0 !== o && c.position(nt(o, i, a))
				}
			} else if (!s) return;
			return this
		},
		relativePosition: function (e, t) {
			var n = this[0],
				r = this.cy(),
				i = g(e) ? e : void 0,
				a = void 0 !== i || void 0 !== t && d(e),
				o = r.hasCompoundNodes();
			if (n && n.isNode()) {
				if (!a) {
					var s = n.position(),
						l = o ? n.parent() : null,
						u = l && l.length > 0,
						c = u;
					u && (l = l[0]);
					var h = c ? l.position() : {
						x: 0,
						y: 0
					};
					return i = {
						x: s.x - h.x,
						y: s.y - h.y
					}, void 0 === e ? i : i[e]
				}
				for (var p = 0; p < this.length; p++) {
					var f = this[p],
						v = o ? f.parent() : null,
						y = v && v.length > 0,
						m = y;
					y && (v = v[0]);
					var b = m ? v.position() : {
						x: 0,
						y: 0
					};
					void 0 !== t ? f.position(e, t + b[e]) : void 0 !== i && f.position({
						x: i.x + b.x,
						y: i.y + b.y
					})
				}
			} else if (!a) return;
			return this
		}
	}).modelPosition = Xr.point = Xr.position, Xr.modelPositions = Xr.points = Xr.positions, Xr.renderedPoint = Xr.renderedPosition, Xr.relativePoint = Xr.relativePosition;
	var Qr, Jr, ei = Wr;
	Qr = Jr = {}, Jr.renderedBoundingBox = function (e) {
		var t = this.boundingBox(e),
			n = this.cy(),
			r = n.zoom(),
			i = n.pan(),
			a = t.x1 * r + i.x,
			o = t.x2 * r + i.x,
			s = t.y1 * r + i.y,
			l = t.y2 * r + i.y;
		return {
			x1: a,
			x2: o,
			y1: s,
			y2: l,
			w: o - a,
			h: l - s
		}
	}, Jr.dirtyCompoundBoundsCache = function () {
		var e = this.cy();
		return e.styleEnabled() && e.hasCompoundNodes() ? (this.forEachUp(function (e) {
			if (e.isParent()) {
				var t = e._private;
				t.compoundBoundsClean = !1, t.bbCache = null, e.emitAndNotify("bounds")
			}
		}), this) : this
	}, Jr.updateCompoundBounds = function () {
		var e = arguments.length > 0 && void 0 !== arguments[0] && arguments[0],
			t = this.cy();
		if (!t.styleEnabled() || !t.hasCompoundNodes()) return this;
		if (!e && t.batching()) return this;

		function n(e) {
			if (e.isParent()) {
				var t = e._private,
					n = e.children(),
					r = "include" === e.pstyle("compound-sizing-wrt-labels").value,
					i = {
						width: {
							val: e.pstyle("min-width").pfValue,
							left: e.pstyle("min-width-bias-left"),
							right: e.pstyle("min-width-bias-right")
						},
						height: {
							val: e.pstyle("min-height").pfValue,
							top: e.pstyle("min-height-bias-top"),
							bottom: e.pstyle("min-height-bias-bottom")
						}
					},
					a = n.filter(function (e) {
						return "element" === e.pstyle("display").value
					}).boundingBox({
						includeLabels: r,
						includeOverlays: !1,
						useCache: !1
					}),
					o = t.position;
				0 !== a.w && 0 !== a.h || ((a = {
					w: e.pstyle("width").pfValue,
					h: e.pstyle("height").pfValue
				}).x1 = o.x - a.w / 2, a.x2 = o.x + a.w / 2, a.y1 = o.y - a.h / 2, a.y2 = o.y + a.h / 2);
				var s = i.width.left.value;
				"px" === i.width.left.units && i.width.val > 0 && (s = 100 * s / i.width.val);
				var l = i.width.right.value;
				"px" === i.width.right.units && i.width.val > 0 && (l = 100 * l / i.width.val);
				var u = i.height.top.value;
				"px" === i.height.top.units && i.height.val > 0 && (u = 100 * u / i.height.val);
				var c = i.height.bottom.value;
				"px" === i.height.bottom.units && i.height.val > 0 && (c = 100 * c / i.height.val);
				var h = y(i.width.val - a.w, s, l),
					d = h.biasDiff,
					p = h.biasComplementDiff,
					f = y(i.height.val - a.h, u, c),
					g = f.biasDiff,
					v = f.biasComplementDiff;
				t.autoPadding = function (e, t, n, r) {
					if ("%" !== n.units) return "px" === n.units ? n.pfValue : 0;
					switch (r) {
						case "width":
							return e > 0 ? n.pfValue * e : 0;
						case "height":
							return t > 0 ? n.pfValue * t : 0;
						case "average":
							return e > 0 && t > 0 ? n.pfValue * (e + t) / 2 : 0;
						case "min":
							return e > 0 && t > 0 ? e > t ? n.pfValue * t : n.pfValue * e : 0;
						case "max":
							return e > 0 && t > 0 ? e > t ? n.pfValue * e : n.pfValue * t : 0;
						default:
							return 0
					}
				}(a.w, a.h, e.pstyle("padding"), e.pstyle("padding-relative-to").value), t.autoWidth = Math.max(a.w, i.width.val), o.x = (-d + a.x1 + a.x2 + p) / 2, t.autoHeight = Math.max(a.h, i.height.val), o.y = (-g + a.y1 + a.y2 + v) / 2
			}

			function y(e, t, n) {
				var r = 0,
					i = 0,
					a = t + n;
				return e > 0 && a > 0 && (r = t / a * e, i = n / a * e), {
					biasDiff: r,
					biasComplementDiff: i
				}
			}
		}
		for (var r = 0; r < this.length; r++) {
			var i = this[r],
				a = i._private;
			a.compoundBoundsClean || (n(i), t.batching() || (a.compoundBoundsClean = !0))
		}
		return this
	};
	var ti = function (e) {
		return e === 1 / 0 || e === -1 / 0 ? 0 : e
	},
		ni = function (e, t, n, r, i) {
			r - t != 0 && i - n != 0 && null != t && null != n && null != r && null != i && (e.x1 = t < e.x1 ? t : e.x1, e.x2 = r > e.x2 ? r : e.x2, e.y1 = n < e.y1 ? n : e.y1, e.y2 = i > e.y2 ? i : e.y2, e.w = e.x2 - e.x1, e.h = e.y2 - e.y1)
		},
		ri = function (e, t) {
			return null == t ? e : ni(e, t.x1, t.y1, t.x2, t.y2)
		},
		ii = function (e, t, n) {
			return Ne(e, t, n)
		},
		ai = function (e, t, n) {
			if (!t.cy().headless()) {
				var r, i, a = t._private,
					o = a.rstyle,
					s = o.arrowWidth / 2;
				if ("none" !== t.pstyle(n + "-arrow-shape").value) {
					"source" === n ? (r = o.srcX, i = o.srcY) : "target" === n ? (r = o.tgtX, i = o.tgtY) : (r = o.midX, i = o.midY);
					var l = a.arrowBounds = a.arrowBounds || {},
						u = l[n] = l[n] || {};
					u.x1 = r - s, u.y1 = i - s, u.x2 = r + s, u.y2 = i + s, u.w = u.x2 - u.x1, u.h = u.y2 - u.y1, gt(u, 1), ni(e, u.x1, u.y1, u.x2, u.y2)
				}
			}
		},
		oi = function (e, t, n) {
			if (!t.cy().headless()) {
				var r;
				r = n ? n + "-" : "";
				var i = t._private,
					a = i.rstyle;
				if (t.pstyle(r + "label").strValue) {
					var o, s, l, u, c = t.pstyle("text-halign"),
						h = t.pstyle("text-valign"),
						d = ii(a, "labelWidth", n),
						p = ii(a, "labelHeight", n),
						f = ii(a, "labelX", n),
						g = ii(a, "labelY", n),
						v = t.pstyle(r + "text-margin-x").pfValue,
						y = t.pstyle(r + "text-margin-y").pfValue,
						m = t.isEdge(),
						b = t.pstyle(r + "text-rotation"),
						x = t.pstyle("text-outline-width").pfValue,
						w = t.pstyle("text-border-width").pfValue / 2,
						E = t.pstyle("text-background-padding").pfValue,
						k = p,
						C = d,
						S = C / 2,
						D = k / 2;
					if (m) o = f - S, s = f + S, l = g - D, u = g + D;
					else {
						switch (c.value) {
							case "left":
								o = f - C, s = f;
								break;
							case "center":
								o = f - S, s = f + S;
								break;
							case "right":
								o = f, s = f + C
						}
						switch (h.value) {
							case "top":
								l = g - k, u = g;
								break;
							case "center":
								l = g - D, u = g + D;
								break;
							case "bottom":
								l = g, u = g + k
						}
					}
					o += v - Math.max(x, w) - E, s += v + Math.max(x, w) + E, l += y - Math.max(x, w) - E, u += y + Math.max(x, w) + E;
					var P = n || "main",
						T = i.labelBounds,
						M = T[P] = T[P] || {};
					M.x1 = o, M.y1 = l, M.x2 = s, M.y2 = u, M.w = s - o, M.h = u - l, gt(M, 1);
					var B = m && "autorotate" === b.strValue,
						_ = null != b.pfValue && 0 !== b.pfValue;
					if (B || _) {
						var N = B ? ii(i.rstyle, "labelAngle", n) : b.pfValue,
							I = Math.cos(N),
							z = Math.sin(N),
							L = (o + s) / 2,
							A = (l + u) / 2;
						if (!m) {
							switch (c.value) {
								case "left":
									L = s;
									break;
								case "right":
									L = o
							}
							switch (h.value) {
								case "top":
									A = u;
									break;
								case "bottom":
									A = l
							}
						}
						var O = function (e, t) {
							return {
								x: (e -= L) * I - (t -= A) * z + L,
								y: e * z + t * I + A
							}
						},
							R = O(o, l),
							V = O(o, u),
							F = O(s, l),
							q = O(s, u);
						o = Math.min(R.x, V.x, F.x, q.x), s = Math.max(R.x, V.x, F.x, q.x), l = Math.min(R.y, V.y, F.y, q.y), u = Math.max(R.y, V.y, F.y, q.y)
					}
					ni(e, o, l, s, u), ni(i.labelBounds.all, o, l, s, u)
				}
				return e
			}
		},
		si = function (e) {
			var t = 0,
				n = function (e) {
					return (e ? 1 : 0) << t++
				},
				r = 0;
			return r += n(e.incudeNodes), r += n(e.includeEdges), r += n(e.includeLabels), r += n(e.includeOverlays)
		},
		li = function (e) {
			if (e.isEdge()) {
				var t = e.source().position(),
					n = e.target().position(),
					r = function (e) {
						return Math.round(e)
					};
				return ce([r(t.x), r(t.y), r(n.x), r(n.y)])
			}
			return 0
		},
		ui = function (e, t) {
			var n, r = e._private,
				i = (null == t ? hi : si(t)) === hi,
				a = li(e),
				o = r.bbCachePosKey === a,
				s = !(t.useCache && o) || null == r.bbCache;
			if (s ? (o || e.recalculateRenderedStyle(), n = function (e, t) {
				var n, r, i, a, o, s, l, u = e._private.cy,
					c = u.styleEnabled(),
					h = u.headless(),
					d = pt(),
					p = e._private,
					f = c ? e.pstyle("display").value : "element",
					g = e.isNode(),
					v = e.isEdge(),
					y = "none" !== f,
					m = p.rstyle,
					b = g && c ? e.pstyle("bounds-expansion").pfValue : 0;
				if (y) {
					var x = 0;
					c && t.includeOverlays && 0 !== e.pstyle("overlay-opacity").value && (x = e.pstyle("overlay-padding").value);
					var w = 0;
					if (c && (w = e.pstyle("width").pfValue / 2), g && t.includeNodes) {
						var E = e.position();
						o = E.x, s = E.y;
						var k = e.outerWidth() / 2,
							C = e.outerHeight() / 2;
						ni(d, n = o - k, i = s - C, r = o + k, a = s + C)
					} else if (v && t.includeEdges)
						if (c && !h && (n = Math.min(m.srcX, m.midX, m.tgtX), r = Math.max(m.srcX, m.midX, m.tgtX), i = Math.min(m.srcY, m.midY, m.tgtY), a = Math.max(m.srcY, m.midY, m.tgtY), ni(d, n -= w, i -= w, r += w, a += w)), c && !h && "haystack" === e.pstyle("curve-style").strValue) {
							var S = m.haystackPts || [];
							if (n = S[0].x, i = S[0].y, n > (r = S[1].x)) {
								var D = n;
								n = r, r = D
							}
							if (i > (a = S[1].y)) {
								var P = i;
								i = a, a = P
							}
							ni(d, n - w, i - w, r + w, a + w)
						} else {
							for (var T = m.bezierPts || m.linePts || [], M = 0; M < T.length; M++) {
								var B = T[M];
								n = B.x - w, r = B.x + w, i = B.y - w, a = B.y + w, ni(d, n, i, r, a)
							}
							if (0 === T.length) {
								var _ = e.source().position(),
									N = e.target().position();
								if ((n = _.x) > (r = N.x)) {
									var I = n;
									n = r, r = I
								}
								if ((i = _.y) > (a = N.y)) {
									var z = i;
									i = a, a = z
								}
								ni(d, n -= w, i -= w, r += w, a += w)
							}
						}
					if (c && t.includeEdges && v && (ai(d, e, "mid-source"), ai(d, e, "mid-target"), ai(d, e, "source"), ai(d, e, "target")), c && "yes" === e.pstyle("ghost").value) {
						var L = e.pstyle("ghost-offset-x").pfValue,
							A = e.pstyle("ghost-offset-y").pfValue;
						ni(d, d.x1 + L, d.y1 + A, d.x2 + L, d.y2 + A)
					}
					var O = p.bodyBounds = p.bodyBounds || {};
					vt(O, d), gt(O, b), gt(O, 1), c && (n = d.x1, r = d.x2, i = d.y1, a = d.y2, ni(d, n - x, i - x, r + x, a + x));
					var R = p.overlayBounds = p.overlayBounds || {};
					vt(R, d), gt(R, b), gt(R, 1);
					var V = p.labelBounds = p.labelBounds || {};
					null != V.all ? ((l = V.all).x1 = 1 / 0, l.y1 = 1 / 0, l.x2 = -1 / 0, l.y2 = -1 / 0, l.w = 0, l.h = 0) : V.all = pt(), c && t.includeLabels && (oi(d, e, null), v && (oi(d, e, "source"), oi(d, e, "target")))
				}
				return d.x1 = ti(d.x1), d.y1 = ti(d.y1), d.x2 = ti(d.x2), d.y2 = ti(d.y2), d.w = ti(d.x2 - d.x1), d.h = ti(d.y2 - d.y1), d.w > 0 && d.h > 0 && y && (gt(d, b), gt(d, 1)), d
			}(e, ci), r.bbCache = n, r.bbCacheShift.x = r.bbCacheShift.y = 0, r.bbCachePosKey = a) : n = r.bbCache, !s && (0 !== r.bbCacheShift.x || 0 !== r.bbCacheShift.y)) {
				var l = yt,
					u = r.bbCacheShift,
					c = function (e, t) {
						null != e && l(e, t)
					};
				l(n, u);
				var h = r.bodyBounds,
					d = r.overlayBounds,
					p = r.labelBounds,
					f = r.arrowBounds;
				c(h, u), c(d, u), null != f && (c(f.source, u), c(f.target, u), c(f["mid-source"], u), c(f["mid-target"], u)), null != p && (c(p.main, u), c(p.all, u), c(p.source, u), c(p.target, u))
			}
			if (r.bbCacheShift.x = r.bbCacheShift.y = 0, !i) {
				var g = e.isNode();
				n = pt(), (t.includeNodes && g || t.includeEdges && !g) && (t.includeOverlays ? ri(n, r.overlayBounds) : ri(n, r.bodyBounds)), t.includeLabels && ri(n, r.labelBounds.all), n.w = n.x2 - n.x1, n.h = n.y2 - n.y1
			}
			return n
		},
		ci = {
			includeNodes: !0,
			includeEdges: !0,
			includeLabels: !0,
			includeOverlays: !0,
			useCache: !0
		},
		hi = si(ci),
		di = Me(ci);
	Jr.boundingBox = function (e) {
		if (1 === this.length && null != this[0]._private.bbCache && (void 0 === e || void 0 === e.useCache || !0 === e.useCache)) return e = void 0 === e ? ci : di(e), ui(this[0], e);
		var t = pt(),
			n = di(e = e || ci);
		if (this.cy().styleEnabled())
			for (var r = 0; r < this.length; r++) {
				var i = this[r],
					a = i._private,
					o = li(i),
					s = a.bbCachePosKey === o,
					l = n.useCache && s;
				i.recalculateRenderedStyle(l)
			}
		this.updateCompoundBounds();
		for (var u = 0; u < this.length; u++) {
			var c = this[u];
			ri(t, ui(c, n))
		}
		return t.x1 = ti(t.x1), t.y1 = ti(t.y1), t.x2 = ti(t.x2), t.y2 = ti(t.y2), t.w = ti(t.x2 - t.x1), t.h = ti(t.y2 - t.y1), t
	}, Jr.dirtyBoundingBoxCache = function () {
		for (var e = 0; e < this.length; e++) {
			var t = this[e]._private;
			t.bbCache = null, t.bbCacheShift.x = t.bbCacheShift.y = 0, t.bbCachePosKey = null
		}
		return this.emitAndNotify("bounds"), this
	}, Jr.shiftCachedBoundingBox = function (e) {
		for (var t = 0; t < this.length; t++) {
			var n = this[t]._private;
			null != n.bbCache && (n.bbCacheShift.x += e.x, n.bbCacheShift.y += e.y)
		}
		return this.emitAndNotify("bounds"), this
	}, Jr.boundingBoxAt = function (e) {
		var t = this.nodes(),
			n = this.cy(),
			r = n.hasCompoundNodes();
		if (r && (t = t.filter(function (e) {
			return !e.isParent()
		})), g(e)) {
			var i = e;
			e = function () {
				return i
			}
		}
		n.startBatch(), t.forEach(function (t, n) {
			return t._private.bbAtOldPos = e(t, n)
		}).silentPositions(e), r && this.updateCompoundBounds(!0);
		var a = function (e) {
			return {
				x1: e.x1,
				x2: e.x2,
				w: e.w,
				y1: e.y1,
				y2: e.y2,
				h: e.h
			}
		}(this.boundingBox({
			useCache: !1
		}));
		return t.silentPositions(function (e) {
			return e._private.bbAtOldPos
		}), n.endBatch(), a
	}, Qr.boundingbox = Qr.bb = Qr.boundingBox, Qr.renderedBoundingbox = Qr.renderedBoundingBox;
	var pi, fi, gi = Jr;
	pi = fi = {};
	var vi = function (e) {
		e.uppercaseName = B(e.name), e.autoName = "auto" + e.uppercaseName, e.labelName = "label" + e.uppercaseName, e.outerName = "outer" + e.uppercaseName, e.uppercaseOuterName = B(e.outerName), pi[e.name] = function () {
			var t = this[0],
				n = t._private,
				r = n.cy._private.styleEnabled;
			if (t) {
				if (!r) return 1;
				if (t.isParent()) return t.updateCompoundBounds(), n[e.autoName] || 0;
				var i = t.pstyle(e.name);
				switch (i.strValue) {
					case "label":
						return t.recalculateRenderedStyle(), n.rstyle[e.labelName] || 0;
					default:
						return i.pfValue
				}
			}
		}, pi["outer" + e.uppercaseName] = function () {
			var t = this[0],
				n = t._private.cy._private.styleEnabled;
			if (t) return n ? t[e.name]() + t.pstyle("border-width").pfValue + 2 * t.padding() : 1
		}, pi["rendered" + e.uppercaseName] = function () {
			var t = this[0];
			if (t) return t[e.name]() * this.cy().zoom()
		}, pi["rendered" + e.uppercaseOuterName] = function () {
			var t = this[0];
			if (t) return t[e.outerName]() * this.cy().zoom()
		}
	};
	vi({
		name: "width"
	}), vi({
		name: "height"
	}), fi.padding = function () {
		var e = this[0],
			t = e._private;
		return e.isParent() ? (e.updateCompoundBounds(), void 0 !== t.autoPadding ? t.autoPadding : e.pstyle("padding").pfValue) : e.pstyle("padding").pfValue
	};
	var yi = fi,
		mi = {
			controlPoints: {
				get: function (e) {
					return e.renderer().getControlPoints(e)
				},
				mult: !0
			},
			segmentPoints: {
				get: function (e) {
					return e.renderer().getSegmentPoints(e)
				},
				mult: !0
			},
			sourceEndpoint: {
				get: function (e) {
					return e.renderer().getSourceEndpoint(e)
				}
			},
			targetEndpoint: {
				get: function (e) {
					return e.renderer().getTargetEndpoint(e)
				}
			},
			midpoint: {
				get: function (e) {
					return e.renderer().getEdgeMidpoint(e)
				}
			}
		},
		bi = Object.keys(mi).reduce(function (e, t) {
			var n = mi[t],
				r = function (e) {
					return "rendered" + e[0].toUpperCase() + e.substr(1)
				}(t);
			return e[t] = function () {
				return function (e, t) {
					if (e.isEdge()) return t(e)
				}(this, n.get)
			}, n.mult ? e[r] = function () {
				return function (e, t) {
					if (e.isEdge()) {
						var n = e.cy(),
							r = n.pan(),
							i = n.zoom();
						return t(e).map(function (e) {
							return tt(e, i, r)
						})
					}
				}(this, n.get)
			} : e[r] = function () {
				return function (e, t) {
					if (e.isEdge()) {
						var n = e.cy();
						return tt(t(e), n.zoom(), n.pan())
					}
				}(this, n.get)
			}, e
		}, {}),
		xi = I({}, ei, gi, yi, bi),
		wi = function (e, t) {
			this.recycle(e, t)
		};

	function Ei() {
		return !1
	}

	function ki() {
		return !0
	}
	wi.prototype = {
		instanceString: function () {
			return "event"
		},
		recycle: function (e, t) {
			if (this.isImmediatePropagationStopped = this.isPropagationStopped = this.isDefaultPrevented = Ei, null != e && e.preventDefault ? (this.type = e.type, this.isDefaultPrevented = e.defaultPrevented ? ki : Ei) : null != e && e.type ? t = e : this.type = e, null != t && (this.originalEvent = t.originalEvent, this.type = null != t.type ? t.type : this.type, this.cy = t.cy, this.target = t.target, this.position = t.position, this.renderedPosition = t.renderedPosition, this.namespace = t.namespace, this.layout = t.layout), null != this.cy && null != this.position && null == this.renderedPosition) {
				var n = this.position,
					r = this.cy.zoom(),
					i = this.cy.pan();
				this.renderedPosition = {
					x: n.x * r + i.x,
					y: n.y * r + i.y
				}
			}
			this.timeStamp = e && e.timeStamp || Date.now()
		},
		preventDefault: function () {
			this.isDefaultPrevented = ki;
			var e = this.originalEvent;
			e && e.preventDefault && e.preventDefault()
		},
		stopPropagation: function () {
			this.isPropagationStopped = ki;
			var e = this.originalEvent;
			e && e.stopPropagation && e.stopPropagation()
		},
		stopImmediatePropagation: function () {
			this.isImmediatePropagationStopped = ki, this.stopPropagation()
		},
		isDefaultPrevented: Ei,
		isPropagationStopped: Ei,
		isImmediatePropagationStopped: Ei
	};
	var Ci = /^([^.]+)(\.(?:[^.]+))?$/,
		Si = {
			qualifierCompare: function (e, t) {
				return e === t
			},
			eventMatches: function () {
				return !0
			},
			addEventFields: function () { },
			callbackContext: function (e) {
				return e
			},
			beforeEmit: function () { },
			afterEmit: function () { },
			bubble: function () {
				return !1
			},
			parent: function () {
				return null
			},
			context: null
		},
		Di = Object.keys(Si),
		Pi = {};

	function Ti() {
		for (var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : Pi, t = arguments.length > 1 ? arguments[1] : void 0, n = 0; n < Di.length; n++) {
			var r = Di[n];
			this[r] = e[r] || Si[r]
		}
		this.context = t || this.context, this.listeners = [], this.emitting = 0
	}
	var Mi = Ti.prototype,
		Bi = function (e, t, n, r, i, a, o) {
			p(r) && (i = r, r = null), o && (a = null == a ? o : I({}, a, o));
			for (var s = f(n) ? n : n.split(/\s+/), l = 0; l < s.length; l++) {
				var u = s[l];
				if (!k(u)) {
					var c = u.match(Ci);
					if (c)
						if (!1 === t(e, u, c[1], c[2] ? c[2] : null, r, i, a)) break
				}
			}
		},
		_i = function (e, t) {
			return e.addEventFields(e.context, t), new wi(t.type, t)
		},
		Ni = function (e, t, n) {
			if ("event" !== h(n))
				if (g(n)) t(e, _i(e, n));
				else
					for (var r = f(n) ? n : n.split(/\s+/), i = 0; i < r.length; i++) {
						var a = r[i];
						if (!k(a)) {
							var o = a.match(Ci);
							if (o) {
								var s = o[1],
									l = o[2] ? o[2] : null;
								t(e, _i(e, {
									type: s,
									namespace: l,
									target: e.context
								}))
							}
						}
					} else t(e, n)
		};
	Mi.on = Mi.addListener = function (e, t, n, r, i) {
		return Bi(this, function (e, t, n, r, i, a, o) {
			p(a) && e.listeners.push({
				event: t,
				callback: a,
				type: n,
				namespace: r,
				qualifier: i,
				conf: o
			})
		}, e, t, n, r, i), this
	}, Mi.one = function (e, t, n, r) {
		return this.on(e, t, n, r, {
			one: !0
		})
	}, Mi.removeListener = Mi.off = function (e, t, n, r) {
		var i = this;
		0 !== this.emitting && (this.listeners = this.listeners.slice());
		for (var a = this.listeners, o = function (o) {
			var s = a[o];
			Bi(i, function (t, n, r, i, l, u) {
				if ((s.type === r || "*" === e) && (!i && ".*" !== s.namespace || s.namespace === i) && (!l || t.qualifierCompare(s.qualifier, l)) && (!u || s.callback === u)) return a.splice(o, 1), !1
			}, e, t, n, r)
		}, s = a.length - 1; s >= 0; s--) o(s);
		return this
	}, Mi.removeAllListeners = function () {
		return this.removeListener("*")
	}, Mi.emit = Mi.trigger = function (e, t, n) {
		var r = this.listeners,
			i = r.length;
		return this.emitting++ , f(t) || (t = [t]), Ni(this, function (e, a) {
			null != n && (r = [{
				event: a.event,
				type: a.type,
				namespace: a.namespace,
				callback: n
			}], i = r.length);
			for (var o = function (n) {
				var i = r[n];
				if (i.type === a.type && (!i.namespace || i.namespace === a.namespace || ".*" === i.namespace) && e.eventMatches(e.context, i, a)) {
					var o = [a];
					null != t && function (e, t) {
						for (var n = 0; n < t.length; n++) {
							var r = t[n];
							e.push(r)
						}
					}(o, t), e.beforeEmit(e.context, i, a), i.conf && i.conf.one && (e.listeners = e.listeners.filter(function (e) {
						return e !== i
					}));
					var s = e.callbackContext(e.context, i, a),
						l = i.callback.apply(s, o);
					e.afterEmit(e.context, i, a), !1 === l && (a.stopPropagation(), a.preventDefault())
				}
			}, s = 0; s < i; s++) o(s);
			e.bubble(e.context) && !a.isPropagationStopped() && e.parent(e.context).emit(a, t)
		}, e), this.emitting-- , this
	};
	var Ii = {
		qualifierCompare: function (e, t) {
			return null == e || null == t ? null == e && null == t : e.sameText(t)
		},
		eventMatches: function (e, t, n) {
			var r = t.qualifier;
			return null == r || e !== n.target && b(n.target) && r.matches(n.target)
		},
		addEventFields: function (e, t) {
			t.cy = e.cy(), t.target = e
		},
		callbackContext: function (e, t, n) {
			return null != t.qualifier ? n.target : e
		},
		beforeEmit: function (e, t) {
			t.conf && t.conf.once && t.conf.onceCollection.removeListener(t.event, t.qualifier, t.callback)
		},
		bubble: function () {
			return !0
		},
		parent: function (e) {
			return e.isChild() ? e.parent() : e.cy()
		}
	},
		zi = function (e) {
			return d(e) ? new Ir(e) : e
		},
		Li = {
			createEmitter: function () {
				for (var e = 0; e < this.length; e++) {
					var t = this[e],
						n = t._private;
					n.emitter || (n.emitter = new Ti(Ii, t))
				}
				return this
			},
			emitter: function () {
				return this._private.emitter
			},
			on: function (e, t, n) {
				for (var r = zi(t), i = 0; i < this.length; i++) {
					this[i].emitter().on(e, r, n)
				}
				return this
			},
			removeListener: function (e, t, n) {
				for (var r = zi(t), i = 0; i < this.length; i++) {
					this[i].emitter().removeListener(e, r, n)
				}
				return this
			},
			removeAllListeners: function () {
				for (var e = 0; e < this.length; e++) {
					this[e].emitter().removeAllListeners()
				}
				return this
			},
			one: function (e, t, n) {
				for (var r = zi(t), i = 0; i < this.length; i++) {
					this[i].emitter().one(e, r, n)
				}
				return this
			},
			once: function (e, t, n) {
				for (var r = zi(t), i = 0; i < this.length; i++) {
					this[i].emitter().on(e, r, n, {
						once: !0,
						onceCollection: this
					})
				}
			},
			emit: function (e, t) {
				for (var n = 0; n < this.length; n++) {
					this[n].emitter().emit(e, t)
				}
				return this
			},
			emitAndNotify: function (e, t) {
				if (0 !== this.length) return this.cy().notify(e, this), this.emit(e, t), this
			}
		};
	Un.eventAliasesOn(Li);
	var Ai = {
		nodes: function (e) {
			return this.filter(function (e) {
				return e.isNode()
			}).filter(e)
		},
		edges: function (e) {
			return this.filter(function (e) {
				return e.isEdge()
			}).filter(e)
		},
		byGroup: function () {
			for (var e = this.spawn(), t = this.spawn(), n = 0; n < this.length; n++) {
				var r = this[n];
				r.isNode() ? e.merge(r) : t.merge(r)
			}
			return {
				nodes: e,
				edges: t
			}
		},
		filter: function (e, t) {
			if (void 0 === e) return this;
			if (d(e) || m(e)) return new Ir(e).filter(this);
			if (p(e)) {
				for (var n = this.spawn(), r = 0; r < this.length; r++) {
					var i = this[r];
					(t ? e.apply(t, [i, r, this]) : e(i, r, this)) && n.merge(i)
				}
				return n
			}
			return this.spawn()
		},
		not: function (e) {
			if (e) {
				d(e) && (e = this.filter(e));
				for (var t = [], n = e._private.map, r = 0; r < this.length; r++) {
					var i = this[r];
					n.has(i.id()) || t.push(i)
				}
				return this.spawn(t)
			}
			return this
		},
		absoluteComplement: function () {
			return this.cy().mutableElements().not(this)
		},
		intersect: function (e) {
			if (d(e)) {
				var t = e;
				return this.filter(t)
			}
			for (var n = [], r = e, i = this.length < e.length, a = i ? r._private.map : this._private.map, o = i ? this : r, s = 0; s < o.length; s++) {
				var l = o[s]._private.data.id,
					u = a.get(l);
				u && n.push(u.ele)
			}
			return this.spawn(n)
		},
		xor: function (e) {
			var t = this._private.cy;
			d(e) && (e = t.$(e));
			var n = [],
				r = e,
				i = function (e, t) {
					for (var r = 0; r < e.length; r++) {
						var i = e[r],
							a = i._private.data.id;
						t.hasElementWithId(a) || n.push(i)
					}
				};
			return i(this, r), i(r, this), this.spawn(n)
		},
		diff: function (e) {
			var t = this._private.cy;
			d(e) && (e = t.$(e));
			var n = [],
				r = [],
				i = [],
				a = e,
				o = function (e, t, n) {
					for (var r = 0; r < e.length; r++) {
						var a = e[r],
							o = a._private.data.id;
						t.hasElementWithId(o) ? i.push(a) : n.push(a)
					}
				};
			return o(this, a, n), o(a, this, r), {
				left: this.spawn(n, {
					unique: !0
				}),
				right: this.spawn(r, {
					unique: !0
				}),
				both: this.spawn(i, {
					unique: !0
				})
			}
		},
		add: function (e) {
			var t = this._private.cy;
			if (!e) return this;
			if (d(e)) {
				var n = e;
				e = t.mutableElements().filter(n)
			}
			for (var r = [], i = 0; i < this.length; i++) r.push(this[i]);
			for (var a = this._private.map, o = 0; o < e.length; o++) {
				var s = !a.has(e[o].id());
				s && r.push(e[o])
			}
			return this.spawn(r)
		},
		merge: function (e) {
			var t = this._private,
				n = t.cy;
			if (!e) return this;
			if (e && d(e)) {
				var r = e;
				e = n.mutableElements().filter(r)
			}
			for (var i = t.map, a = 0; a < e.length; a++) {
				var o = e[a],
					s = o._private.data.id;
				if (!i.has(s)) {
					var l = this.length++;
					this[l] = o, i.set(s, {
						ele: o,
						index: l
					})
				} else {
					var u = i.get(s).index;
					this[u] = o, i.set(s, {
						ele: o,
						index: u
					})
				}
			}
			return this
		},
		unmergeAt: function (e) {
			var t = this[e].id(),
				n = this._private.map;
			this[e] = void 0, n.delete(t);
			var r = e === this.length - 1;
			if (this.length > 1 && !r) {
				var i = this.length - 1,
					a = this[i],
					o = a._private.data.id;
				this[i] = void 0, this[e] = a, n.set(o, {
					ele: a,
					index: e
				})
			}
			return this.length-- , this
		},
		unmergeOne: function (e) {
			e = e[0];
			var t = this._private,
				n = e._private.data.id,
				r = t.map.get(n);
			if (!r) return this;
			var i = r.index;
			return this.unmergeAt(i), this
		},
		unmerge: function (e) {
			var t = this._private.cy;
			if (!e) return this;
			if (e && d(e)) {
				var n = e;
				e = t.mutableElements().filter(n)
			}
			for (var r = 0; r < e.length; r++) this.unmergeOne(e[r]);
			return this
		},
		unmergeBy: function (e) {
			for (var t = this.length - 1; t >= 0; t--) {
				e(this[t]) && this.unmergeAt(t)
			}
			return this
		},
		map: function (e, t) {
			for (var n = [], r = 0; r < this.length; r++) {
				var i = this[r],
					a = t ? e.apply(t, [i, r, this]) : e(i, r, this);
				n.push(a)
			}
			return n
		},
		reduce: function (e, t) {
			for (var n = t, r = 0; r < this.length; r++) n = e(n, this[r], r, this);
			return n
		},
		max: function (e, t) {
			for (var n, r = -1 / 0, i = 0; i < this.length; i++) {
				var a = this[i],
					o = t ? e.apply(t, [a, i, this]) : e(a, i, this);
				o > r && (r = o, n = a)
			}
			return {
				value: r,
				ele: n
			}
		},
		min: function (e, t) {
			for (var n, r = 1 / 0, i = 0; i < this.length; i++) {
				var a = this[i],
					o = t ? e.apply(t, [a, i, this]) : e(a, i, this);
				o < r && (r = o, n = a)
			}
			return {
				value: r,
				ele: n
			}
		}
	},
		Oi = Ai;
	Oi.u = Oi["|"] = Oi["+"] = Oi.union = Oi.or = Oi.add, Oi["\\"] = Oi["!"] = Oi["-"] = Oi.difference = Oi.relativeComplement = Oi.subtract = Oi.not, Oi.n = Oi["&"] = Oi["."] = Oi.and = Oi.intersection = Oi.intersect, Oi["^"] = Oi["(+)"] = Oi["(-)"] = Oi.symmetricDifference = Oi.symdiff = Oi.xor, Oi.fnFilter = Oi.filterFn = Oi.stdFilter = Oi.filter, Oi.complement = Oi.abscomp = Oi.absoluteComplement;
	var Ri = function (e, t) {
		var n = e.cy().hasCompoundNodes();

		function r(e) {
			var t = e.pstyle("z-compound-depth");
			return "auto" === t.value ? n ? e.zDepth() : 0 : "bottom" === t.value ? -1 : "top" === t.value ? ye : 0
		}
		var i = r(e) - r(t);
		if (0 !== i) return i;

		function a(e) {
			return "auto" === e.pstyle("z-index-compare").value && e.isNode() ? 1 : 0
		}
		var o = a(e) - a(t);
		if (0 !== o) return o;
		var s = e.pstyle("z-index").value - t.pstyle("z-index").value;
		return 0 !== s ? s : e.poolIndex() - t.poolIndex()
	},
		Vi = {
			forEach: function (e, t) {
				if (p(e))
					for (var n = this.length, r = 0; r < n; r++) {
						var i = this[r];
						if (!1 === (t ? e.apply(t, [i, r, this]) : e(i, r, this))) break
					}
				return this
			},
			toArray: function () {
				for (var e = [], t = 0; t < this.length; t++) e.push(this[t]);
				return e
			},
			slice: function (e, t) {
				var n = [],
					r = this.length;
				null == t && (t = r), null == e && (e = 0), e < 0 && (e = r + e), t < 0 && (t = r + t);
				for (var i = e; i >= 0 && i < t && i < r; i++) n.push(this[i]);
				return this.spawn(n)
			},
			size: function () {
				return this.length
			},
			eq: function (e) {
				return this[e] || this.spawn()
			},
			first: function () {
				return this[0] || this.spawn()
			},
			last: function () {
				return this[this.length - 1] || this.spawn()
			},
			empty: function () {
				return 0 === this.length
			},
			nonempty: function () {
				return !this.empty()
			},
			sort: function (e) {
				if (!p(e)) return this;
				var t = this.toArray().sort(e);
				return this.spawn(t)
			},
			sortByZIndex: function () {
				return this.sort(Ri)
			},
			zDepth: function () {
				var e = this[0];
				if (e) {
					var t = e._private;
					if ("nodes" === t.group) {
						var n = t.data.parent ? e.parents().size() : 0;
						return e.isParent() ? n : ye - 1
					}
					var r = t.source,
						i = t.target,
						a = r.zDepth(),
						o = i.zDepth();
					return Math.max(a, o, 0)
				}
			}
		};
	Vi.each = Vi.forEach;
	var Fi = Me({
		nodeDimensionsIncludeLabels: !1
	}),
		qi = {
			layoutDimensions: function (e) {
				if ((e = Fi(e)).nodeDimensionsIncludeLabels) {
					var t = this.boundingBox();
					return {
						w: t.w,
						h: t.h
					}
				}
				return {
					w: this.outerWidth(),
					h: this.outerHeight()
				}
			},
			layoutPositions: function (e, t, n) {
				var r = this.nodes(),
					i = this.cy(),
					a = t.eles,
					o = function (e) {
						return e.id()
					},
					s = D(n, o);
				e.emit({
					type: "layoutstart",
					layout: e
				}), e.animations = [];
				var l = t.spacingFactor && 1 !== t.spacingFactor,
					u = function () {
						if (!l) return null;
						for (var e = pt(), t = 0; t < r.length; t++) {
							var n = r[t],
								i = s(n, t);
							ft(e, i.x, i.y)
						}
						return e
					}(),
					c = D(function (e, n) {
						var r = s(e, n);
						l && (r = function (e, t, n) {
							var r = t.x1 + t.w / 2,
								i = t.y1 + t.h / 2;
							return {
								x: r + (n.x - r) * e,
								y: i + (n.y - i) * e
							}
						}(Math.abs(t.spacingFactor), u, r));
						return null != t.transform && (r = t.transform(e, r)), r
					}, o);
				if (t.animate) {
					for (var h = 0; h < r.length; h++) {
						var d = r[h],
							p = c(d, h);
						if (null == t.animateFilter || t.animateFilter(d, h)) {
							var f = d.animation({
								position: p,
								duration: t.animationDuration,
								easing: t.animationEasing
							});
							e.animations.push(f)
						} else d.position(p)
					}
					if (t.fit) {
						var g = i.animation({
							fit: {
								boundingBox: a.boundingBoxAt(c),
								padding: t.padding
							},
							duration: t.animationDuration,
							easing: t.animationEasing
						});
						e.animations.push(g)
					} else if (void 0 !== t.zoom && void 0 !== t.pan) {
						var v = i.animation({
							zoom: t.zoom,
							pan: t.pan,
							duration: t.animationDuration,
							easing: t.animationEasing
						});
						e.animations.push(v)
					}
					e.animations.forEach(function (e) {
						return e.play()
					}), e.one("layoutready", t.ready), e.emit({
						type: "layoutready",
						layout: e
					}), Kn.all(e.animations.map(function (e) {
						return e.promise()
					})).then(function () {
						e.one("layoutstop", t.stop), e.emit({
							type: "layoutstop",
							layout: e
						})
					})
				} else r.positions(c), t.fit && i.fit(t.eles, t.padding), null != t.zoom && i.zoom(t.zoom), t.pan && i.pan(t.pan), e.one("layoutready", t.ready), e.emit({
					type: "layoutready",
					layout: e
				}), e.one("layoutstop", t.stop), e.emit({
					type: "layoutstop",
					layout: e
				});
				return this
			},
			layout: function (e) {
				return this.cy().makeLayout(I({}, e, {
					eles: this
				}))
			}
		};

	function Yi(e, t, n) {
		var r, i = n._private,
			a = i.styleCache = i.styleCache || [];
		return null != (r = a[e]) ? r : r = a[e] = t(n)
	}

	function ji(e, t) {
		return e = he(e),
			function (n) {
				return Yi(e, t, n)
			}
	}

	function Xi(e, t) {
		e = he(e);
		var n = function (e) {
			return t.call(e)
		};
		return function () {
			var t = this[0];
			if (t) return Yi(e, n, t)
		}
	}
	qi.createLayout = qi.makeLayout = qi.layout;
	var Wi = {
		recalculateRenderedStyle: function (e) {
			var t = this.cy(),
				n = t.renderer(),
				r = t.styleEnabled();
			return n && r && n.recalculateRenderedStyle(this, e), this
		},
		dirtyStyleCache: function () {
			var e, t = this.cy(),
				n = function (e) {
					return e._private.styleCache = null
				};
			t.hasCompoundNodes() ? ((e = this.spawnSelf().merge(this.descendants()).merge(this.parents())).merge(e.connectedEdges()), e.forEach(n)) : this.forEach(function (e) {
				n(e), e.connectedEdges().forEach(n)
			});
			return this
		},
		updateStyle: function (e) {
			var t = this._private.cy;
			if (!t.styleEnabled()) return this;
			if (t.batching()) return t._private.batchStyleEles.merge(this), this;
			var n = t.hasCompoundNodes(),
				r = t.style(),
				i = this;
			e = !(!e && void 0 !== e), n && (i = this.spawnSelf().merge(this.descendants()).merge(this.parents()));
			var a = r.apply(i);
			return e ? a.emitAndNotify("style") : a.emit("style"), this
		},
		parsedStyle: function (e) {
			var t = !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1],
				n = this[0],
				r = n.cy();
			if (r.styleEnabled() && n) {
				var i = n._private.style[e];
				return null != i ? i : t ? r.style().getDefaultProperty(e) : null
			}
		},
		numericStyle: function (e) {
			var t = this[0];
			if (t.cy().styleEnabled() && t) {
				var n = t.pstyle(e);
				return void 0 !== n.pfValue ? n.pfValue : n.value
			}
		},
		numericStyleUnits: function (e) {
			var t = this[0];
			if (t.cy().styleEnabled()) return t ? t.pstyle(e).units : void 0
		},
		renderedStyle: function (e) {
			var t = this.cy();
			if (!t.styleEnabled()) return this;
			var n = this[0];
			return n ? t.style().getRenderedStyle(n, e) : void 0
		},
		style: function (e, t) {
			var n = this.cy();
			if (!n.styleEnabled()) return this;
			var r = n.style();
			if (g(e)) {
				var i = e;
				r.applyBypass(this, i, !1), this.emitAndNotify("style")
			} else if (d(e)) {
				if (void 0 === t) {
					var a = this[0];
					return a ? r.getStylePropertyValue(a, e) : void 0
				}
				r.applyBypass(this, e, t, !1), this.emitAndNotify("style")
			} else if (void 0 === e) {
				var o = this[0];
				return o ? r.getRawStyle(o) : void 0
			}
			return this
		},
		removeStyle: function (e) {
			var t = this.cy();
			if (!t.styleEnabled()) return this;
			var n = t.style();
			if (void 0 === e)
				for (var r = 0; r < this.length; r++) {
					var i = this[r];
					n.removeAllBypasses(i, !1)
				} else {
				e = e.split(/\s+/);
				for (var a = 0; a < this.length; a++) {
					var o = this[a];
					n.removeBypasses(o, e, !1)
				}
			}
			return this.emitAndNotify("style"), this
		},
		show: function () {
			return this.css("display", "element"), this
		},
		hide: function () {
			return this.css("display", "none"), this
		},
		effectiveOpacity: function () {
			var e = this.cy();
			if (!e.styleEnabled()) return 1;
			var t = e.hasCompoundNodes(),
				n = this[0];
			if (n) {
				var r = n._private,
					i = n.pstyle("opacity").value;
				if (!t) return i;
				var a = r.data.parent ? n.parents() : null;
				if (a)
					for (var o = 0; o < a.length; o++) {
						i *= a[o].pstyle("opacity").value
					}
				return i
			}
		},
		transparent: function () {
			if (!this.cy().styleEnabled()) return !1;
			var e = this[0],
				t = e.cy().hasCompoundNodes();
			return e ? t ? 0 === e.effectiveOpacity() : 0 === e.pstyle("opacity").value : void 0
		},
		backgrounding: function () {
			return !!this.cy().styleEnabled() && !!this[0]._private.backgrounding
		}
	};

	function Hi(e, t) {
		var n = e._private.data.parent ? e.parents() : null;
		if (n)
			for (var r = 0; r < n.length; r++) {
				if (!t(n[r])) return !1
			}
		return !0
	}

	function Ki(e) {
		var t = e.ok,
			n = e.edgeOkViaNode || e.ok,
			r = e.parentOk || e.ok;
		return function () {
			var e = this.cy();
			if (!e.styleEnabled()) return !0;
			var i = this[0],
				a = e.hasCompoundNodes();
			if (i) {
				var o = i._private;
				if (!t(i)) return !1;
				if (i.isNode()) return !a || Hi(i, r);
				var s = o.source,
					l = o.target;
				return n(s) && (!a || Hi(s, n)) && (s === l || n(l) && (!a || Hi(l, n)))
			}
		}
	}
	var Gi = ji("eleTakesUpSpace", function (e) {
		return "element" === e.pstyle("display").value && 0 !== e.width() && (!e.isNode() || 0 !== e.height())
	});
	Wi.takesUpSpace = Xi("takesUpSpace", Ki({
		ok: Gi
	}));
	var Zi = ji("eleInteractive", function (e) {
		return "yes" === e.pstyle("events").value && "visible" === e.pstyle("visibility").value && Gi(e)
	}),
		Ui = ji("parentInteractive", function (e) {
			return "visible" === e.pstyle("visibility").value && Gi(e)
		});
	Wi.interactive = Xi("interactive", Ki({
		ok: Zi,
		parentOk: Ui,
		edgeOkViaNode: Gi
	})), Wi.noninteractive = function () {
		var e = this[0];
		if (e) return !e.interactive()
	};
	var $i = ji("eleVisible", function (e) {
		return "visible" === e.pstyle("visibility").value && 0 !== e.pstyle("opacity").pfValue && Gi(e)
	}),
		Qi = Gi;
	Wi.visible = Xi("visible", Ki({
		ok: $i,
		edgeOkViaNode: Qi
	})), Wi.hidden = function () {
		var e = this[0];
		if (e) return !e.visible()
	}, Wi.isBundledBezier = Xi("isBundledBezier", function () {
		return !!this.cy().styleEnabled() && (!this.removed() && "bezier" === this.pstyle("curve-style").value && this.takesUpSpace())
	}), Wi.bypass = Wi.css = Wi.style, Wi.renderedCss = Wi.renderedStyle, Wi.removeBypass = Wi.removeCss = Wi.removeStyle, Wi.pstyle = Wi.parsedStyle;
	var Ji = {};

	function ea(e) {
		return function () {
			var t = arguments,
				n = [];
			if (2 === t.length) {
				var r = t[0],
					i = t[1];
				this.on(e.event, r, i)
			} else if (1 === t.length && p(t[0])) {
				var a = t[0];
				this.on(e.event, a)
			} else if (0 === t.length || 1 === t.length && f(t[0])) {
				for (var o = 1 === t.length ? t[0] : null, s = 0; s < this.length; s++) {
					var l = this[s],
						u = !e.ableField || l._private[e.ableField],
						c = l._private[e.field] != e.value;
					if (e.overrideAble) {
						var h = e.overrideAble(l);
						if (void 0 !== h && (u = h, !h)) return this
					}
					u && (l._private[e.field] = e.value, c && n.push(l))
				}
				var d = this.spawn(n);
				d.updateStyle(), d.emit(e.event), o && d.emit(o)
			}
			return this
		}
	}

	function ta(e) {
		Ji[e.field] = function () {
			var t = this[0];
			if (t) {
				if (e.overrideField) {
					var n = e.overrideField(t);
					if (void 0 !== n) return n
				}
				return t._private[e.field]
			}
		}, Ji[e.on] = ea({
			event: e.on,
			field: e.field,
			ableField: e.ableField,
			overrideAble: e.overrideAble,
			value: !0
		}), Ji[e.off] = ea({
			event: e.off,
			field: e.field,
			ableField: e.ableField,
			overrideAble: e.overrideAble,
			value: !1
		})
	}
	ta({
		field: "locked",
		overrideField: function (e) {
			return !!e.cy().autolock() || void 0
		},
		on: "lock",
		off: "unlock"
	}), ta({
		field: "grabbable",
		overrideField: function (e) {
			return !e.cy().autoungrabify() && !e.pannable() && void 0
		},
		on: "grabify",
		off: "ungrabify"
	}), ta({
		field: "selected",
		ableField: "selectable",
		overrideAble: function (e) {
			return !e.cy().autounselectify() && void 0
		},
		on: "select",
		off: "unselect"
	}), ta({
		field: "selectable",
		overrideField: function (e) {
			return !e.cy().autounselectify() && void 0
		},
		on: "selectify",
		off: "unselectify"
	}), Ji.deselect = Ji.unselect, Ji.grabbed = function () {
		var e = this[0];
		if (e) return e._private.grabbed
	}, ta({
		field: "active",
		on: "activate",
		off: "unactivate"
	}), ta({
		field: "pannable",
		on: "panify",
		off: "unpanify"
	}), Ji.inactive = function () {
		var e = this[0];
		if (e) return !e._private.active
	};
	var na = {},
		ra = function (e) {
			return function (t) {
				for (var n = [], r = 0; r < this.length; r++) {
					var i = this[r];
					if (i.isNode()) {
						for (var a = !1, o = i.connectedEdges(), s = 0; s < o.length; s++) {
							var l = o[s],
								u = l.source(),
								c = l.target();
							if (e.noIncomingEdges && c === i && u !== i || e.noOutgoingEdges && u === i && c !== i) {
								a = !0;
								break
							}
						}
						a || n.push(i)
					}
				}
				return this.spawn(n, {
					unique: !0
				}).filter(t)
			}
		},
		ia = function (e) {
			return function (t) {
				for (var n = [], r = 0; r < this.length; r++) {
					var i = this[r];
					if (i.isNode())
						for (var a = i.connectedEdges(), o = 0; o < a.length; o++) {
							var s = a[o],
								l = s.source(),
								u = s.target();
							e.outgoing && l === i ? (n.push(s), n.push(u)) : e.incoming && u === i && (n.push(s), n.push(l))
						}
				}
				return this.spawn(n, {
					unique: !0
				}).filter(t)
			}
		},
		aa = function (e) {
			return function (t) {
				for (var n = this, r = [], i = {}; ;) {
					var a = e.outgoing ? n.outgoers() : n.incomers();
					if (0 === a.length) break;
					for (var o = !1, s = 0; s < a.length; s++) {
						var l = a[s],
							u = l.id();
						i[u] || (i[u] = !0, r.push(l), o = !0)
					}
					if (!o) break;
					n = a
				}
				return this.spawn(r, {
					unique: !0
				}).filter(t)
			}
		};

	function oa(e) {
		return function (t) {
			for (var n = [], r = 0; r < this.length; r++) {
				var i = this[r]._private[e.attr];
				i && n.push(i)
			}
			return this.spawn(n, {
				unique: !0
			}).filter(t)
		}
	}

	function sa(e) {
		return function (t) {
			var n = [],
				r = this._private.cy,
				i = e || {};
			d(t) && (t = r.$(t));
			for (var a = 0; a < t.length; a++)
				for (var o = t[a]._private.edges, s = 0; s < o.length; s++) {
					var l = o[s],
						u = l._private.data,
						c = this.hasElementWithId(u.source) && t.hasElementWithId(u.target),
						h = t.hasElementWithId(u.source) && this.hasElementWithId(u.target);
					if (c || h) {
						if (i.thisIsSrc || i.thisIsTgt) {
							if (i.thisIsSrc && !c) continue;
							if (i.thisIsTgt && !h) continue
						}
						n.push(l)
					}
				}
			return this.spawn(n, {
				unique: !0
			})
		}
	}

	function la(e) {
		return e = I({}, {
			codirected: !1
		}, e),
			function (t) {
				for (var n = [], r = this.edges(), i = e, a = 0; a < r.length; a++)
					for (var o = r[a]._private, s = o.source, l = s._private.data.id, u = o.data.target, c = s._private.edges, h = 0; h < c.length; h++) {
						var d = c[h],
							p = d._private.data,
							f = p.target,
							g = p.source,
							v = f === u && g === l,
							y = l === f && u === g;
						(i.codirected && v || !i.codirected && (v || y)) && n.push(d)
					}
				return this.spawn(n, {
					unique: !0
				}).filter(t)
			}
	}
	na.clearTraversalCache = function () {
		for (var e = 0; e < this.length; e++) this[e]._private.traversalCache = null
	}, I(na, {
		roots: ra({
			noIncomingEdges: !0
		}),
		leaves: ra({
			noOutgoingEdges: !0
		}),
		outgoers: Rr(ia({
			outgoing: !0
		}), "outgoers"),
		successors: aa({
			outgoing: !0
		}),
		incomers: Rr(ia({
			incoming: !0
		}), "incomers"),
		predecessors: aa({
			incoming: !0
		})
	}), I(na, {
		neighborhood: Rr(function (e) {
			for (var t = [], n = this.nodes(), r = 0; r < n.length; r++)
				for (var i = n[r], a = i.connectedEdges(), o = 0; o < a.length; o++) {
					var s = a[o],
						l = s.source(),
						u = s.target(),
						c = i === l ? u : l;
					c.length > 0 && t.push(c[0]), t.push(s[0])
				}
			return this.spawn(t, {
				unique: !0
			}).filter(e)
		}, "neighborhood"),
		closedNeighborhood: function (e) {
			return this.neighborhood().add(this).filter(e)
		},
		openNeighborhood: function (e) {
			return this.neighborhood(e)
		}
	}), na.neighbourhood = na.neighborhood, na.closedNeighbourhood = na.closedNeighborhood, na.openNeighbourhood = na.openNeighborhood, I(na, {
		source: Rr(function (e) {
			var t, n = this[0];
			return n && (t = n._private.source || n.cy().collection()), t && e ? t.filter(e) : t
		}, "source"),
		target: Rr(function (e) {
			var t, n = this[0];
			return n && (t = n._private.target || n.cy().collection()), t && e ? t.filter(e) : t
		}, "target"),
		sources: oa({
			attr: "source"
		}),
		targets: oa({
			attr: "target"
		})
	}), I(na, {
		edgesWith: Rr(sa(), "edgesWith"),
		edgesTo: Rr(sa({
			thisIsSrc: !0
		}), "edgesTo")
	}), I(na, {
		connectedEdges: Rr(function (e) {
			for (var t = [], n = 0; n < this.length; n++) {
				var r = this[n];
				if (r.isNode())
					for (var i = r._private.edges, a = 0; a < i.length; a++) {
						var o = i[a];
						t.push(o)
					}
			}
			return this.spawn(t, {
				unique: !0
			}).filter(e)
		}, "connectedEdges"),
		connectedNodes: Rr(function (e) {
			for (var t = [], n = 0; n < this.length; n++) {
				var r = this[n];
				r.isEdge() && (t.push(r.source()[0]), t.push(r.target()[0]))
			}
			return this.spawn(t, {
				unique: !0
			}).filter(e)
		}, "connectedNodes"),
		parallelEdges: Rr(la(), "parallelEdges"),
		codirectedEdges: Rr(la({
			codirected: !0
		}), "codirectedEdges")
	}), I(na, {
		components: function (e) {
			var t = this,
				n = t.cy(),
				r = n.collection(),
				i = null == e ? t.nodes() : e.nodes(),
				a = [];
			null != e && i.empty() && (i = e.sources());
			var o = function (e, t) {
				r.merge(e), i.unmerge(e), t.merge(e)
			};
			if (i.empty()) return t.spawn();
			var s = function () {
				var e = n.collection();
				a.push(e);
				var r = i[0];
				o(r, e), t.bfs({
					directed: !1,
					roots: r,
					visit: function (t) {
						return o(t, e)
					}
				}), e.forEach(function (t) {
					t.connectedEdges().forEach(function (t) {
						e.has(t.source()) && e.has(t.target()) && e.merge(t)
					})
				})
			};
			do {
				s()
			} while (i.length > 0);
			return a
		},
		component: function () {
			var e = this[0];
			return e.cy().mutableElements().components(e)[0]
		}
	}), na.componentsOf = na.components;
	var ua = function (e, t, n) {
		for (var r = null != n ? n : De(); e.hasElementWithId(r);) r = De();
		return r
	},
		ca = function (e, t, n) {
			if (void 0 !== e && w(e)) {
				var r = new ze,
					i = !1;
				if (t) {
					if (t.length > 0 && g(t[0]) && !b(t[0])) {
						i = !0;
						for (var a = [], o = new Ae, s = 0, l = t.length; s < l; s++) {
							var u = t[s];
							null == u.data && (u.data = {});
							var c = u.data;
							if (null == c.id) c.id = ua(e, u);
							else if (e.hasElementWithId(c.id) || o.has(c.id)) continue;
							var h = new Oe(e, u, !1);
							a.push(h), o.add(c.id)
						}
						t = a
					}
				} else t = [];
				this.length = 0;
				for (var d = 0, p = t.length; d < p; d++) {
					var f = t[d][0];
					if (null != f) {
						var v = f._private.data.id;
						(null == n || n.unique && !r.has(v)) && (r.set(v, {
							index: this.length,
							ele: f
						}), this[this.length] = f, this.length++)
					}
				}
				this._private = {
					cy: e,
					map: r
				}, i && this.restore()
			} else Ee("A collection must have a reference to the core")
		},
		ha = Oe.prototype = ca.prototype;
	ha.instanceString = function () {
		return "collection"
	}, ha.spawn = function (e, t, n) {
		return w(e) || (n = t, t = e, e = this.cy()), new ca(e, t, n)
	}, ha.spawnSelf = function () {
		return this.spawn(this)
	}, ha.cy = function () {
		return this._private.cy
	}, ha.renderer = function () {
		return this._private.cy.renderer()
	}, ha.element = function () {
		return this[0]
	}, ha.collection = function () {
		return x(this) ? this : new ca(this._private.cy, [this])
	}, ha.unique = function () {
		return new ca(this._private.cy, this, {
			unique: !0
		})
	}, ha.hasElementWithId = function (e) {
		return e = "" + e, this._private.map.has(e)
	}, ha.getElementById = function (e) {
		e = "" + e;
		var t = this._private.cy,
			n = this._private.map.get(e);
		return n ? n.ele : new ca(t)
	}, ha.$id = ha.getElementById, ha.poolIndex = function () {
		var e = this._private.cy._private.elements,
			t = this[0]._private.data.id;
		return e._private.map.get(t).index
	}, ha.indexOf = function (e) {
		var t = e[0]._private.data.id;
		return this._private.map.get(t).index
	}, ha.indexOfId = function (e) {
		return e = "" + e, this._private.map.get(e).index
	}, ha.json = function (e) {
		var t = this.element(),
			n = this.cy();
		if (null == t && e) return this;
		if (null != t) {
			var r = t._private;
			if (g(e)) {
				if (n.startBatch(), e.data) {
					t.data(e.data);
					var i = r.data;
					if (t.isEdge()) {
						var a = !1,
							o = {},
							s = e.data.source,
							l = e.data.target;
						null != s && s != i.source && (o.source = "" + s, a = !0), null != l && l != i.target && (o.target = "" + l, a = !0), a && (t = t.move(o))
					} else {
						var u = e.data.parent;
						null == u && null == i.parent || u == i.parent || (void 0 === u && (u = null), null != u && (u = "" + u), t = t.move({
							parent: u
						}))
					}
				}
				e.position && t.position(e.position);
				var c = function (n, i, a) {
					var o = e[n];
					null != o && o !== r[n] && (o ? t[i]() : t[a]())
				};
				return c("removed", "remove", "restore"), c("selected", "select", "unselect"), c("selectable", "selectify", "unselectify"), c("locked", "lock", "unlock"), c("grabbable", "grabify", "ungrabify"), c("pannable", "panify", "unpanify"), null != e.classes && t.classes(e.classes), n.endBatch(), this
			}
			if (void 0 === e) {
				var h = {
					data: Se(r.data),
					position: Se(r.position),
					group: r.group,
					removed: r.removed,
					selected: r.selected,
					selectable: r.selectable,
					locked: r.locked,
					grabbable: r.grabbable,
					pannable: r.pannable,
					classes: null
				};
				h.classes = "";
				var d = 0;
				return r.classes.forEach(function (e) {
					return h.classes += 0 == d++ ? e : " " + e
				}), h
			}
		}
	}, ha.jsons = function () {
		for (var e = [], t = 0; t < this.length; t++) {
			var n = this[t].json();
			e.push(n)
		}
		return e
	}, ha.clone = function () {
		for (var e = this.cy(), t = [], n = 0; n < this.length; n++) {
			var r = this[n].json(),
				i = new Oe(e, r, !1);
			t.push(i)
		}
		return new ca(e, t)
	}, ha.copy = ha.clone, ha.restore = function () {
		for (var e, t, n = !(arguments.length > 0 && void 0 !== arguments[0]) || arguments[0], r = !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1], i = this.cy(), a = i._private, o = [], s = [], l = 0, u = this.length; l < u; l++) {
			var c = this[l];
			r && !c.removed() || (c.isNode() ? o.push(c) : s.push(c))
		}
		e = o.concat(s);
		var h = function () {
			e.splice(t, 1), t--
		};
		for (t = 0; t < e.length; t++) {
			var p = e[t],
				f = p._private,
				g = f.data;
			if (p.clearTraversalCache(), r || f.removed)
				if (void 0 === g.id) g.id = ua(i, p);
				else if (v(g.id)) g.id = "" + g.id;
				else {
					if (k(g.id) || !d(g.id)) {
						Ee("Can not create element with invalid string ID `" + g.id + "`"), h();
						continue
					}
					if (i.hasElementWithId(g.id)) {
						Ee("Can not create second element with ID `" + g.id + "`"), h();
						continue
					}
				} else;
			var y = g.id;
			if (p.isNode()) {
				var m = f.position;
				null == m.x && (m.x = 0), null == m.y && (m.y = 0)
			}
			if (p.isEdge()) {
				for (var b = p, x = ["source", "target"], w = x.length, E = !1, C = 0; C < w; C++) {
					var S = x[C],
						D = g[S];
					v(D) && (D = g[S] = "" + g[S]), null == D || "" === D ? (Ee("Can not create edge `" + y + "` with unspecified " + S), E = !0) : i.hasElementWithId(D) || (Ee("Can not create edge `" + y + "` with nonexistant " + S + " `" + D + "`"), E = !0)
				}
				if (E) {
					h();
					continue
				}
				var P = i.getElementById(g.source),
					T = i.getElementById(g.target);
				P.same(T) ? P._private.edges.push(b) : (P._private.edges.push(b), T._private.edges.push(b)), b._private.source = P, b._private.target = T
			}
			f.map = new ze, f.map.set(y, {
				ele: p,
				index: 0
			}), f.removed = !1, r && i.addToPool(p)
		}
		for (var M = 0; M < o.length; M++) {
			var B = o[M],
				_ = B._private.data;
			v(_.parent) && (_.parent = "" + _.parent);
			var N = _.parent;
			if (null != N) {
				var I = i.getElementById(N);
				if (I.empty()) _.parent = void 0;
				else {
					for (var z = !1, L = I; !L.empty();) {
						if (B.same(L)) {
							z = !0, _.parent = void 0;
							break
						}
						L = L.parent()
					}
					z || (I[0]._private.children.push(B), B._private.parent = I[0], a.hasCompoundNodes = !0)
				}
			}
		}
		if (e.length > 0) {
			for (var A = new ca(i, e), O = 0; O < A.length; O++) {
				var R = A[O];
				R.isNode() || (R.parallelEdges().clearTraversalCache(), R.source().clearTraversalCache(), R.target().clearTraversalCache())
			} (a.hasCompoundNodes ? i.collection().merge(A).merge(A.connectedNodes()).merge(A.parent()) : A).dirtyCompoundBoundsCache().dirtyBoundingBoxCache().updateStyle(n), n ? A.emitAndNotify("add") : r && A.emit("add")
		}
		return this
	}, ha.removed = function () {
		var e = this[0];
		return e && e._private.removed
	}, ha.inside = function () {
		var e = this[0];
		return e && !e._private.removed
	}, ha.remove = function () {
		var e = !(arguments.length > 0 && void 0 !== arguments[0]) || arguments[0],
			t = !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1],
			n = [],
			r = {},
			i = this._private.cy;

		function a(e) {
			var i = r[e.id()];
			t && e.removed() || i || (r[e.id()] = !0, e.isNode() ? (n.push(e), function (e) {
				for (var t = e._private.edges, n = 0; n < t.length; n++) a(t[n])
			}(e), function (e) {
				for (var t = e._private.children, n = 0; n < t.length; n++) a(t[n])
			}(e)) : n.unshift(e))
		}
		for (var o = 0, s = this.length; o < s; o++) {
			a(this[o])
		}

		function l(e, t) {
			var n = e._private.edges;
			Be(n, t), e.clearTraversalCache()
		}

		function u(e) {
			e.clearTraversalCache()
		}
		var c = [];

		function h(e, t) {
			t = t[0];
			var n = (e = e[0])._private.children,
				r = e.id();
			Be(n, t), t._private.parent = null, c.ids[r] || (c.ids[r] = !0, c.push(e))
		}
		c.ids = {}, this.dirtyCompoundBoundsCache(), t && i.removeFromPool(n);
		for (var d = 0; d < n.length; d++) {
			var p = n[d];
			if (p.isEdge()) {
				var f = p.source()[0],
					g = p.target()[0];
				l(f, p), l(g, p);
				for (var v = p.parallelEdges(), y = 0; y < v.length; y++) {
					var m = v[y];
					u(m), m.isBundledBezier() && m.dirtyBoundingBoxCache()
				}
			} else {
				var b = p.parent();
				0 !== b.length && h(b, p)
			}
			t && (p._private.removed = !0)
		}
		var x = i._private.elements;
		i._private.hasCompoundNodes = !1;
		for (var w = 0; w < x.length; w++) {
			if (x[w].isParent()) {
				i._private.hasCompoundNodes = !0;
				break
			}
		}
		var E = new ca(this.cy(), n);
		E.size() > 0 && (e ? E.emitAndNotify("remove") : t && E.emit("remove"));
		for (var k = 0; k < c.length; k++) {
			var C = c[k];
			t && C.removed() || C.updateStyle()
		}
		return E
	}, ha.move = function (e) {
		var t = this._private.cy,
			n = this,
			r = function (e) {
				return null == e ? e : "" + e
			};
		if (void 0 !== e.source || void 0 !== e.target) {
			var i = r(e.source),
				a = r(e.target),
				o = null != i && t.hasElementWithId(i),
				s = null != a && t.hasElementWithId(a);
			(o || s) && (t.batch(function () {
				n.remove(!1, !1), n.emitAndNotify("moveout");
				for (var e = 0; e < n.length; e++) {
					var t = n[e],
						r = t._private.data;
					t.isEdge() && (o && (r.source = i), s && (r.target = a))
				}
				n.restore(!1, !1)
			}), n.emitAndNotify("move"))
		} else if (void 0 !== e.parent) {
			var l = r(e.parent);
			if (null === l || t.hasElementWithId(l)) {
				var u = null === l ? void 0 : l;
				t.batch(function () {
					var e = n.remove(!1, !1);
					e.emitAndNotify("moveout");
					for (var t = 0; t < n.length; t++) {
						var r = n[t],
							i = r._private.data;
						r.isNode() && (i.parent = u)
					}
					e.restore(!1, !1)
				}), n.emitAndNotify("move")
			}
		}
		return this
	}, [Fn, $n, Qn, Lr, Vr, Hr, Kr, xi, Li, Ai, {
		isNode: function () {
			return "nodes" === this.group()
		},
		isEdge: function () {
			return "edges" === this.group()
		},
		isLoop: function () {
			return this.isEdge() && this.source()[0] === this.target()[0]
		},
		isSimple: function () {
			return this.isEdge() && this.source()[0] !== this.target()[0]
		},
		group: function () {
			var e = this[0];
			if (e) return e._private.group
		}
	},
		Vi, qi, Wi, Ji, na
	].forEach(function (e) {
		I(ha, e)
	});
	var da = {
		add: function (e) {
			var t, n = this;
			if (m(e)) {
				var r = e;
				if (r._private.cy === n) t = r.restore();
				else {
					for (var i = [], a = 0; a < r.length; a++) {
						var o = r[a];
						i.push(o.json())
					}
					t = new ca(n, i)
				}
			} else if (f(e)) {
				t = new ca(n, e)
			} else if (g(e) && (f(e.nodes) || f(e.edges))) {
				for (var s = e, l = [], u = ["nodes", "edges"], c = 0, h = u.length; c < h; c++) {
					var d = u[c],
						p = s[d];
					if (f(p))
						for (var v = 0, y = p.length; v < y; v++) {
							var b = I({
								group: d
							}, p[v]);
							l.push(b)
						}
				}
				t = new ca(n, l)
			} else {
				t = new Oe(n, e).collection()
			}
			return t
		},
		remove: function (e) {
			if (m(e));
			else if (d(e)) {
				var t = e;
				e = this.$(t)
			}
			return e.remove()
		}
	};

	function pa(e, t, n, r) {
		var i = 4,
			a = .001,
			o = 1e-7,
			s = 10,
			l = 11,
			u = 1 / (l - 1),
			c = "undefined" != typeof Float32Array;
		if (4 !== arguments.length) return !1;
		for (var h = 0; h < 4; ++h)
			if ("number" != typeof arguments[h] || isNaN(arguments[h]) || !isFinite(arguments[h])) return !1;
		e = Math.min(e, 1), n = Math.min(n, 1), e = Math.max(e, 0), n = Math.max(n, 0);
		var d = c ? new Float32Array(l) : new Array(l);

		function p(e, t) {
			return 1 - 3 * t + 3 * e
		}

		function f(e, t) {
			return 3 * t - 6 * e
		}

		function g(e) {
			return 3 * e
		}

		function v(e, t, n) {
			return ((p(t, n) * e + f(t, n)) * e + g(t)) * e
		}

		function y(e, t, n) {
			return 3 * p(t, n) * e * e + 2 * f(t, n) * e + g(t)
		}

		function m(t) {
			for (var r = 0, c = 1, h = l - 1; c !== h && d[c] <= t; ++c) r += u;
			var p = r + (t - d[--c]) / (d[c + 1] - d[c]) * u,
				f = y(p, e, n);
			return f >= a ? function (t, r) {
				for (var a = 0; a < i; ++a) {
					var o = y(r, e, n);
					if (0 === o) return r;
					r -= (v(r, e, n) - t) / o
				}
				return r
			}(t, p) : 0 === f ? p : function (t, r, i) {
				var a, l, u = 0;
				do {
					(a = v(l = r + (i - r) / 2, e, n) - t) > 0 ? i = l : r = l
				} while (Math.abs(a) > o && ++u < s);
				return l
			}(t, r, r + u)
		}
		var b = !1;

		function x() {
			b = !0, e === t && n === r || function () {
				for (var t = 0; t < l; ++t) d[t] = v(t * u, e, n)
			}()
		}
		var w = function (i) {
			return b || x(), e === t && n === r ? i : 0 === i ? 0 : 1 === i ? 1 : v(m(i), t, r)
		};
		w.getControlPoints = function () {
			return [{
				x: e,
				y: t
			}, {
				x: n,
				y: r
			}]
		};
		var E = "generateBezier(" + [e, t, n, r] + ")";
		return w.toString = function () {
			return E
		}, w
	}
	var fa = function () {
		function e(e) {
			return -e.tension * e.x - e.friction * e.v
		}

		function t(t, n, r) {
			var i = {
				x: t.x + r.dx * n,
				v: t.v + r.dv * n,
				tension: t.tension,
				friction: t.friction
			};
			return {
				dx: i.v,
				dv: e(i)
			}
		}

		function n(n, r) {
			var i = {
				dx: n.v,
				dv: e(n)
			},
				a = t(n, .5 * r, i),
				o = t(n, .5 * r, a),
				s = t(n, r, o),
				l = 1 / 6 * (i.dx + 2 * (a.dx + o.dx) + s.dx),
				u = 1 / 6 * (i.dv + 2 * (a.dv + o.dv) + s.dv);
			return n.x = n.x + l * r, n.v = n.v + u * r, n
		}
		return function e(t, r, i) {
			var a, o, s, l = {
				x: -1,
				v: 0,
				tension: null,
				friction: null
			},
				u = [0],
				c = 0;
			for (t = parseFloat(t) || 500, r = parseFloat(r) || 20, i = i || null, l.tension = t, l.friction = r, o = (a = null !== i) ? (c = e(t, r)) / i * .016 : .016; s = n(s || l, o), u.push(1 + s.x), c += 16, Math.abs(s.x) > 1e-4 && Math.abs(s.v) > 1e-4;);
			return a ? function (e) {
				return u[e * (u.length - 1) | 0]
			} : c
		}
	}(),
		ga = function (e, t, n, r) {
			var i = pa(e, t, n, r);
			return function (e, t, n) {
				return e + (t - e) * i(n)
			}
		},
		va = {
			linear: function (e, t, n) {
				return e + (t - e) * n
			},
			ease: ga(.25, .1, .25, 1),
			"ease-in": ga(.42, 0, 1, 1),
			"ease-out": ga(0, 0, .58, 1),
			"ease-in-out": ga(.42, 0, .58, 1),
			"ease-in-sine": ga(.47, 0, .745, .715),
			"ease-out-sine": ga(.39, .575, .565, 1),
			"ease-in-out-sine": ga(.445, .05, .55, .95),
			"ease-in-quad": ga(.55, .085, .68, .53),
			"ease-out-quad": ga(.25, .46, .45, .94),
			"ease-in-out-quad": ga(.455, .03, .515, .955),
			"ease-in-cubic": ga(.55, .055, .675, .19),
			"ease-out-cubic": ga(.215, .61, .355, 1),
			"ease-in-out-cubic": ga(.645, .045, .355, 1),
			"ease-in-quart": ga(.895, .03, .685, .22),
			"ease-out-quart": ga(.165, .84, .44, 1),
			"ease-in-out-quart": ga(.77, 0, .175, 1),
			"ease-in-quint": ga(.755, .05, .855, .06),
			"ease-out-quint": ga(.23, 1, .32, 1),
			"ease-in-out-quint": ga(.86, 0, .07, 1),
			"ease-in-expo": ga(.95, .05, .795, .035),
			"ease-out-expo": ga(.19, 1, .22, 1),
			"ease-in-out-expo": ga(1, 0, 0, 1),
			"ease-in-circ": ga(.6, .04, .98, .335),
			"ease-out-circ": ga(.075, .82, .165, 1),
			"ease-in-out-circ": ga(.785, .135, .15, .86),
			spring: function (e, t, n) {
				if (0 === n) return va.linear;
				var r = fa(e, t, n);
				return function (e, t, n) {
					return e + (t - e) * r(n)
				}
			},
			"cubic-bezier": ga
		};

	function ya(e, t, n, r, i) {
		if (1 === r) return n;
		var a = i(t, n, r);
		return null == e ? a : ((e.roundValue || e.color) && (a = Math.round(a)), void 0 !== e.min && (a = Math.max(a, e.min)), void 0 !== e.max && (a = Math.min(a, e.max)), a)
	}

	function ma(e, t) {
		return null != e.pfValue || null != e.value ? null == e.pfValue || null != t && "%" === t.type.units ? e.value : e.pfValue : e
	}

	function ba(e, t, n, r, i) {
		var a = null != i ? i.type : null;
		n < 0 ? n = 0 : n > 1 && (n = 1);
		var o = ma(e, i),
			s = ma(t, i);
		if (v(o) && v(s)) return ya(a, o, s, n, r);
		if (f(o) && f(s)) {
			for (var l = [], u = 0; u < s.length; u++) {
				var c = o[u],
					h = s[u];
				if (null != c && null != h) {
					var d = ya(a, c, h, n, r);
					l.push(d)
				} else l.push(h)
			}
			return l
		}
	}

	function xa(e, t, n, r) {
		var i = !r,
			a = e._private,
			o = t._private,
			s = o.easing,
			l = o.startTime,
			u = (r ? e : e.cy()).style();
		if (!o.easingImpl)
			if (null == s) o.easingImpl = va.linear;
			else {
				var c, h, p;
				if (d(s)) c = u.parse("transition-timing-function", s).value;
				else c = s;
				d(c) ? (h = c, p = []) : (h = c[1], p = c.slice(2).map(function (e) {
					return +e
				})), p.length > 0 ? ("spring" === h && p.push(o.duration), o.easingImpl = va[h].apply(null, p)) : o.easingImpl = va[h]
			}
		var f, g = o.easingImpl;
		if (f = 0 === o.duration ? 1 : (n - l) / o.duration, o.applying && (f = o.progress), f < 0 ? f = 0 : f > 1 && (f = 1), null == o.delay) {
			var v = o.startPosition,
				y = o.position;
			if (y && i && !e.locked()) {
				var m = {};
				wa(v.x, y.x) && (m.x = ba(v.x, y.x, f, g)), wa(v.y, y.y) && (m.y = ba(v.y, y.y, f, g)), e.position(m)
			}
			var b = o.startPan,
				x = o.pan,
				w = a.pan,
				E = null != x && r;
			E && (wa(b.x, x.x) && (w.x = ba(b.x, x.x, f, g)), wa(b.y, x.y) && (w.y = ba(b.y, x.y, f, g)), e.emit("pan"));
			var k = o.startZoom,
				C = o.zoom,
				S = null != C && r;
			S && (wa(k, C) && (a.zoom = dt(a.minZoom, ba(k, C, f, g), a.maxZoom)), e.emit("zoom")), (E || S) && e.emit("viewport");
			var D = o.style;
			if (D && D.length > 0 && i) {
				for (var P = 0; P < D.length; P++) {
					var T = D[P],
						M = T.name,
						B = T,
						_ = o.startStyle[M],
						N = ba(_, B, f, g, u.properties[_.name]);
					u.overrideBypass(e, M, N)
				}
				e.emit("style")
			}
		}
		return o.progress = f, f
	}

	function wa(e, t) {
		return null != e && null != t && (!(!v(e) || !v(t)) || !(!e || !t))
	}

	function Ea(e, t, n, r) {
		var i = t._private;
		i.started = !0, i.startTime = n - i.progress * i.duration
	}

	function ka(e, t) {
		var n = t._private.aniEles,
			r = [];

		function i(t, n) {
			var i = t._private,
				a = i.animation.current,
				o = i.animation.queue,
				s = !1;
			if (!n && "none" === t.pstyle("display").value) {
				a = a.splice(0, a.length).concat(o.splice(0, o.length));
				for (var l = 0; l < a.length; l++) a[l].stop()
			}
			if (0 === a.length) {
				var u = o.shift();
				u && a.push(u)
			}
			for (var c = function (e) {
				for (var t = e.length - 1; t >= 0; t--) {
					(0, e[t])()
				}
				e.splice(0, e.length)
			}, h = a.length - 1; h >= 0; h--) {
				var d = a[h],
					p = d._private;
				p.stopped ? (a.splice(h, 1), p.hooked = !1, p.playing = !1, p.started = !1, c(p.frames)) : (p.playing || p.applying) && (p.playing && p.applying && (p.applying = !1), p.started || Ea(0, d, e), xa(t, d, e, n), p.applying && (p.applying = !1), c(p.frames), null != p.step && p.step(e), d.completed() && (a.splice(h, 1), p.hooked = !1, p.playing = !1, p.started = !1, c(p.completes)), s = !0)
			}
			return n || 0 !== a.length || 0 !== o.length || r.push(t), s
		}
		for (var a = !1, o = 0; o < n.length; o++) {
			var s = i(n[o]);
			a = a || s
		}
		var l = i(t, !0);
		(a || l) && (n.length > 0 ? t.notify("draw", n) : t.notify("draw")), n.unmerge(r), t.emit("step")
	}
	var Ca = {
		animate: Un.animate(),
		animation: Un.animation(),
		animated: Un.animated(),
		clearQueue: Un.clearQueue(),
		delay: Un.delay(),
		delayAnimation: Un.delayAnimation(),
		stop: Un.stop(),
		addToAnimationPool: function (e) {
			this.styleEnabled() && this._private.aniEles.merge(e)
		},
		stopAnimationLoop: function () {
			this._private.animationsRunning = !1
		},
		startAnimationLoop: function () {
			var e = this;
			if (e._private.animationsRunning = !0, e.styleEnabled()) {
				var t = e.renderer();
				t && t.beforeRender ? t.beforeRender(function (t, n) {
					ka(n, e)
				}, t.beforeRenderPriorities.animations) : function t() {
					e._private.animationsRunning && oe(function (n) {
						ka(n, e), t()
					})
				}()
			}
		}
	},
		Sa = {
			qualifierCompare: function (e, t) {
				return null == e || null == t ? null == e && null == t : e.sameText(t)
			},
			eventMatches: function (e, t, n) {
				var r = t.qualifier;
				return null == r || e !== n.target && b(n.target) && r.matches(n.target)
			},
			addEventFields: function (e, t) {
				t.cy = e, t.target = e
			},
			callbackContext: function (e, t, n) {
				return null != t.qualifier ? n.target : e
			}
		},
		Da = function (e) {
			return d(e) ? new Ir(e) : e
		},
		Pa = {
			createEmitter: function () {
				var e = this._private;
				return e.emitter || (e.emitter = new Ti(Sa, this)), this
			},
			emitter: function () {
				return this._private.emitter
			},
			on: function (e, t, n) {
				return this.emitter().on(e, Da(t), n), this
			},
			removeListener: function (e, t, n) {
				return this.emitter().removeListener(e, Da(t), n), this
			},
			removeAllListeners: function () {
				return this.emitter().removeAllListeners(), this
			},
			one: function (e, t, n) {
				return this.emitter().one(e, Da(t), n), this
			},
			once: function (e, t, n) {
				return this.emitter().one(e, Da(t), n), this
			},
			emit: function (e, t) {
				return this.emitter().emit(e, t), this
			},
			emitAndNotify: function (e, t) {
				return this.emit(e), this.notify(e, t), this
			}
		};
	Un.eventAliasesOn(Pa);
	var Ta = {
		png: function (e) {
			return e = e || {}, this._private.renderer.png(e)
		},
		jpg: function (e) {
			var t = this._private.renderer;
			return (e = e || {}).bg = e.bg || "#fff", t.jpg(e)
		}
	};
	Ta.jpeg = Ta.jpg;
	var Ma = {
		layout: function (e) {
			if (null != e)
				if (null != e.name) {
					var t = e.name,
						n = this.extension("layout", t);
					if (null != n) {
						var r;
						r = d(e.eles) ? this.$(e.eles) : null != e.eles ? e.eles : this.$();
						var i = new n(I({}, e, {
							cy: this,
							eles: r
						}));
						return i
					}
					Ee("No such layout `" + t + "` found.  Did you forget to import it and `cytoscape.use()` it?")
				} else Ee("A `name` must be specified to make a layout");
			else Ee("Layout options must be specified to make a layout")
		}
	};
	Ma.createLayout = Ma.makeLayout = Ma.layout;
	var Ba = {
		notify: function (e, t) {
			var n = this._private;
			if (this.batching()) {
				n.batchNotifications = n.batchNotifications || {};
				var r = n.batchNotifications[e] = n.batchNotifications[e] || this.collection();
				null != t && r.merge(t)
			} else if (n.notificationsEnabled) {
				var i = this.renderer();
				!this.destroyed() && i && i.notify(e, t)
			}
		},
		notifications: function (e) {
			var t = this._private;
			return void 0 === e ? t.notificationsEnabled : (t.notificationsEnabled = !!e, this)
		},
		noNotifications: function (e) {
			this.notifications(!1), e(), this.notifications(!0)
		},
		batching: function () {
			return this._private.batchCount > 0
		},
		startBatch: function () {
			var e = this._private;
			return null == e.batchCount && (e.batchCount = 0), 0 === e.batchCount && (e.batchStyleEles = this.collection(), e.batchNotifications = {}), e.batchCount++ , this
		},
		endBatch: function () {
			var e = this._private;
			if (0 === e.batchCount) return this;
			if (e.batchCount-- , 0 === e.batchCount) {
				e.batchStyleEles.updateStyle();
				var t = this.renderer();
				Object.keys(e.batchNotifications).forEach(function (n) {
					var r = e.batchNotifications[n];
					r.empty() ? t.notify(n) : t.notify(n, r)
				})
			}
			return this
		},
		batch: function (e) {
			return this.startBatch(), e(), this.endBatch(), this
		},
		batchData: function (e) {
			var t = this;
			return this.batch(function () {
				for (var n = Object.keys(e), r = 0; r < n.length; r++) {
					var i = n[r],
						a = e[i];
					t.getElementById(i).data(a)
				}
			})
		}
	},
		_a = Me({
			hideEdgesOnViewport: !1,
			textureOnViewport: !1,
			motionBlur: !1,
			motionBlurOpacity: .05,
			pixelRatio: void 0,
			desktopTapThreshold: 4,
			touchTapThreshold: 8,
			wheelSensitivity: 1,
			debug: !1,
			showFps: !1
		}),
		Na = {
			renderTo: function (e, t, n, r) {
				return this._private.renderer.renderTo(e, t, n, r), this
			},
			renderer: function () {
				return this._private.renderer
			},
			forceRender: function () {
				return this.notify("draw"), this
			},
			resize: function () {
				return this.invalidateSize(), this.emitAndNotify("resize"), this
			},
			initRenderer: function (e) {
				var t = this.extension("renderer", e.name);
				if (null != t) {
					void 0 !== e.wheelSensitivity && Ce("You have set a custom wheel sensitivity.  This will make your app zoom unnaturally when using mainstream mice.  You should change this value from the default only if you can guarantee that all your users will use the same hardware and OS configuration as your current machine.");
					var n = _a(e);
					n.cy = this, this._private.renderer = new t(n), this.notify("init")
				} else Ee("Can not initialise: No such renderer `".concat(e.name, "` found. Did you forget to import it and `cytoscape.use()` it?"))
			},
			destroyRenderer: function () {
				this.notify("destroy");
				var e = this.container();
				if (e)
					for (e._cyreg = null; e.childNodes.length > 0;) e.removeChild(e.childNodes[0]);
				this._private.renderer = null, this.mutableElements().forEach(function (e) {
					var t = e._private;
					t.rscratch = {}, t.rstyle = {}, t.animation.current = [], t.animation.queue = []
				})
			},
			onRender: function (e) {
				return this.on("render", e)
			},
			offRender: function (e) {
				return this.off("render", e)
			}
		};
	Na.invalidateDimensions = Na.resize;
	var Ia = {
		collection: function (e, t) {
			return d(e) ? this.$(e) : m(e) ? e.collection() : f(e) ? new ca(this, e, t) : new ca(this)
		},
		nodes: function (e) {
			var t = this.$(function (e) {
				return e.isNode()
			});
			return e ? t.filter(e) : t
		},
		edges: function (e) {
			var t = this.$(function (e) {
				return e.isEdge()
			});
			return e ? t.filter(e) : t
		},
		$: function (e) {
			var t = this._private.elements;
			return e ? t.filter(e) : t.spawnSelf()
		},
		mutableElements: function () {
			return this._private.elements
		}
	};
	Ia.elements = Ia.filter = Ia.$;
	var za = {};
	za.apply = function (e) {
		var t = this._private,
			n = t.cy.collection();
		t.newStyle && (t.contextStyles = {}, t.propDiffs = {}, this.cleanElements(e, !0));
		for (var r = 0; r < e.length; r++) {
			var i = e[r],
				a = this.getContextMeta(i);
			if (!a.empty) {
				var o = this.getContextStyle(a),
					s = this.applyContextStyle(a, o, i);
				t.newStyle || this.updateTransitions(i, s.diffProps), this.updateStyleHints(i) && n.merge(i)
			}
		}
		return t.newStyle = !1, n
	}, za.getPropertiesDiff = function (e, t) {
		var n = this._private.propDiffs = this._private.propDiffs || {},
			r = e + "-" + t,
			i = n[r];
		if (i) return i;
		for (var a = [], o = {}, s = 0; s < this.length; s++) {
			var l = this[s],
				u = "t" === e[s],
				c = "t" === t[s],
				h = u !== c,
				d = l.mappedProperties.length > 0;
			if (h || c && d) {
				var p = void 0;
				h && d ? p = l.properties : h ? p = l.properties : d && (p = l.mappedProperties);
				for (var f = 0; f < p.length; f++) {
					for (var g = p[f], v = g.name, y = !1, m = s + 1; m < this.length; m++) {
						var b = this[m];
						if ("t" === t[m] && (y = null != b.properties[g.name])) break
					}
					o[v] || y || (o[v] = !0, a.push(v))
				}
			}
		}
		return n[r] = a, a
	}, za.getContextMeta = function (e) {
		var t, n = "",
			r = e._private.styleCxtKey || "";
		this._private.newStyle && (r = "");
		for (var i = 0; i < this.length; i++) {
			var a = this[i];
			n += a.selector && a.selector.matches(e) ? "t" : "f"
		}
		return t = this.getPropertiesDiff(r, n), e._private.styleCxtKey = n, {
			key: n,
			diffPropNames: t,
			empty: 0 === t.length
		}
	}, za.getContextStyle = function (e) {
		var t = e.key,
			n = this._private.contextStyles = this._private.contextStyles || {};
		if (n[t]) return n[t];
		for (var r = {
			_private: {
				key: t
			}
		}, i = 0; i < this.length; i++) {
			var a = this[i];
			if ("t" === t[i])
				for (var o = 0; o < a.properties.length; o++) {
					var s = a.properties[o];
					r[s.name] = s
				}
		}
		return n[t] = r, r
	}, za.applyContextStyle = function (e, t, n) {
		for (var r = e.diffPropNames, i = {}, a = this.types, o = 0; o < r.length; o++) {
			var s = r[o],
				l = t[s],
				u = n.pstyle(s);
			if (!l) {
				if (!u) continue;
				l = u.bypass ? {
					name: s,
					deleteBypassed: !0
				} : {
						name: s,
						delete: !0
					}
			}
			if (u !== l) {
				if (l.mapped === a.fn && null != u && null != u.mapping && u.mapping.value === l.value) {
					var c = u.mapping;
					if ((c.fnValue = l.value(n)) === c.prevFnValue) continue
				}
				var h = i[s] = {
					prev: u
				};
				this.applyParsedProperty(n, l), h.next = n.pstyle(s), h.next && h.next.bypass && (h.next = h.next.bypassed)
			}
		}
		return {
			diffProps: i
		}
	}, za.updateStyleHints = function (e) {
		var t = e._private,
			n = this,
			r = n.propertyGroupNames,
			i = n.propertyGroupKeys,
			a = function (e, t, r) {
				return n.getPropertiesHash(e, t, r)
			},
			o = t.styleKey;
		if (e.removed()) return !1;
		var s = "nodes" === t.group,
			l = e._private.style;
		r = Object.keys(l);
		for (var u = 0; u < i.length; u++) {
			var c = i[u];
			t.styleKeys[c] = 0
		}
		for (var h = function (e, n) {
			return t.styleKeys[n] = ue(e, t.styleKeys[n])
		}, d = function (e, t) {
			for (var n = 0; n < e.length; n++) h(e.charCodeAt(n), t)
		}, p = function (e) {
			return -128 < e && e < 128 && Math.floor(e) !== e ? -(1024 * e | 0) : e
		}, f = 0; f < r.length; f++) {
			var g = r[f],
				v = l[g];
			if (null != v) {
				var y = this.properties[g],
					m = y.type,
					b = y.groupKey,
					x = void 0;
				null != y.hashOverride ? x = y.hashOverride(e, v) : null != v.pfValue && (x = v.pfValue);
				var w = null == y.enums ? v.value : null,
					E = null != x,
					k = E || null != w,
					C = v.units;
				if (m.number && k) {
					var S = E ? x : w;
					if (m.multiple)
						for (var D = 0; D < S.length; D++) h(p(S[D]), b);
					else h(p(S), b);
					E || null == C || d(C, b)
				} else d(v.strValue, b)
			}
		}
		for (var P = 0, T = 0; T < i.length; T++) {
			var M = i[T],
				B = t.styleKeys[M];
			P = ue(B, P)
		}
		t.styleKey = P;
		var _ = t.labelDimsKey = t.styleKeys.labelDimensions;
		if (t.labelKey = a(e, ["label"], _), t.labelStyleKey = ue(t.styleKeys.commonLabel, t.labelKey), s || (t.sourceLabelKey = a(e, ["source-label"], _), t.sourceLabelStyleKey = ue(t.styleKeys.commonLabel, t.sourceLabelKey), t.targetLabelKey = a(e, ["target-label"], _), t.targetLabelStyleKey = ue(t.styleKeys.commonLabel, t.targetLabelKey)), s) {
			var N = t.styleKeys,
				I = N.nodeBody,
				z = N.nodeBorder,
				L = N.backgroundImage,
				A = N.compound,
				O = N.pie;
			t.nodeKey = ce([z, L, A, O], I), t.hasPie = 0 != O
		}
		return o !== t.styleKey
	}, za.clearStyleHints = function (e) {
		var t = e._private;
		t.styleKeys = {}, t.styleKey = null, t.labelKey = null, t.labelStyleKey = null, t.sourceLabelKey = null, t.sourceLabelStyleKey = null, t.targetLabelKey = null, t.targetLabelStyleKey = null, t.nodeKey = null, t.hasPie = null
	}, za.applyParsedProperty = function (e, t) {
		var n, r = this,
			i = t,
			a = e._private.style,
			o = r.types,
			s = r.properties[i.name].type,
			l = i.bypass,
			u = a[i.name],
			c = u && u.bypass,
			h = e._private,
			d = function (e) {
				return null == e ? null : null != e.pfValue ? e.pfValue : e.value
			},
			p = function () {
				var t = d(u),
					n = d(i);
				r.checkTriggers(e, i.name, t, n)
			};
		if ("curve-style" === t.name && e.isEdge() && ("bezier" !== t.value && e.isLoop() || "haystack" === t.value && (e.source().isParent() || e.target().isParent())) && (i = t = this.parse(t.name, "bezier", l)), i.delete) return a[i.name] = void 0, p(), !0;
		if (i.deleteBypassed) return u ? !!u.bypass && (u.bypassed = void 0, p(), !0) : (p(), !0);
		if (i.deleteBypass) return u ? !!u.bypass && (a[i.name] = u.bypassed, p(), !0) : (p(), !0);
		var f = function () {
			Ce("Do not assign mappings to elements without corresponding data (i.e. ele `" + e.id() + "` has no mapping for property `" + i.name + "` with data field `" + i.field + "`); try a `[" + i.field + "]` selector to limit scope to elements with `" + i.field + "` defined")
		};
		switch (i.mapped) {
			case o.mapData:
				for (var g, y = i.field.split("."), m = h.data, b = 0; b < y.length && m; b++) {
					m = m[y[b]]
				}
				if (null == m) return f(), !1;
				if (!v(m)) return Ce("Do not use continuous mappers without specifying numeric data (i.e. `" + i.field + ": " + m + "` for `" + e.id() + "` is non-numeric)"), !1;
				var x = i.fieldMax - i.fieldMin;
				if ((g = 0 === x ? 0 : (m - i.fieldMin) / x) < 0 ? g = 0 : g > 1 && (g = 1), s.color) {
					var w = i.valueMin[0],
						E = i.valueMax[0],
						k = i.valueMin[1],
						C = i.valueMax[1],
						S = i.valueMin[2],
						D = i.valueMax[2],
						P = null == i.valueMin[3] ? 1 : i.valueMin[3],
						T = null == i.valueMax[3] ? 1 : i.valueMax[3],
						M = [Math.round(w + (E - w) * g), Math.round(k + (C - k) * g), Math.round(S + (D - S) * g), Math.round(P + (T - P) * g)];
					n = {
						bypass: i.bypass,
						name: i.name,
						value: M,
						strValue: "rgb(" + M[0] + ", " + M[1] + ", " + M[2] + ")"
					}
				} else {
					if (!s.number) return !1;
					var B = i.valueMin + (i.valueMax - i.valueMin) * g;
					n = this.parse(i.name, B, i.bypass, "mapping")
				} if (!n) return f(), !1;
				n.mapping = i, i = n;
				break;
			case o.data:
				for (var _ = i.field.split("."), N = h.data, I = 0; I < _.length && N; I++) {
					N = N[_[I]]
				}
				if (null != N && (n = this.parse(i.name, N, i.bypass, "mapping")), !n) return f(), !1;
				n.mapping = i, i = n;
				break;
			case o.fn:
				var z = i.value,
					L = null != i.fnValue ? i.fnValue : z(e);
				if (i.prevFnValue = L, null == L) return Ce("Custom function mappers may not return null (i.e. `" + i.name + "` for ele `" + e.id() + "` is null)"), !1;
				if (!(n = this.parse(i.name, L, i.bypass, "mapping"))) return Ce("Custom function mappers may not return invalid values for the property type (i.e. `" + i.name + "` for ele `" + e.id() + "` is invalid)"), !1;
				n.mapping = Se(i), i = n;
				break;
			case void 0:
				break;
			default:
				return !1
		}
		return l ? (i.bypassed = c ? u.bypassed : u, a[i.name] = i) : c ? u.bypassed = i : a[i.name] = i, p(), !0
	}, za.cleanElements = function (e, t) {
		for (var n = 0; n < e.length; n++) {
			var r = e[n];
			if (this.clearStyleHints(r), r.dirtyCompoundBoundsCache(), r.dirtyBoundingBoxCache(), t)
				for (var i = r._private.style, a = Object.keys(i), o = 0; o < a.length; o++) {
					var s = a[o],
						l = i[s];
					null != l && (l.bypass ? l.bypassed = null : i[s] = null)
				} else r._private.style = {}
		}
	}, za.update = function () {
		this._private.cy.mutableElements().updateStyle()
	}, za.updateTransitions = function (e, t) {
		var n = this,
			r = e._private,
			i = e.pstyle("transition-property").value,
			a = e.pstyle("transition-duration").pfValue,
			o = e.pstyle("transition-delay").pfValue;
		if (i.length > 0 && a > 0) {
			for (var s = {}, l = !1, u = 0; u < i.length; u++) {
				var c = i[u],
					h = e.pstyle(c),
					d = t[c];
				if (d) {
					var p = d.prev,
						g = null != d.next ? d.next : h,
						y = !1,
						m = void 0;
					p && (v(p.pfValue) && v(g.pfValue) ? (y = g.pfValue - p.pfValue, m = p.pfValue + 1e-6 * y) : v(p.value) && v(g.value) ? (y = g.value - p.value, m = p.value + 1e-6 * y) : f(p.value) && f(g.value) && (y = p.value[0] !== g.value[0] || p.value[1] !== g.value[1] || p.value[2] !== g.value[2], m = p.strValue), y && (s[c] = g.strValue, this.applyBypass(e, c, m), l = !0))
				}
			}
			if (!l) return;
			r.transitioning = !0, new Kn(function (t) {
				o > 0 ? e.delayAnimation(o).play().promise().then(t) : t()
			}).then(function () {
				return e.animation({
					style: s,
					duration: a,
					easing: e.pstyle("transition-timing-function").value,
					queue: !1
				}).play().promise()
			}).then(function () {
				n.removeBypasses(e, i), e.emitAndNotify("style"), r.transitioning = !1
			})
		} else r.transitioning && (this.removeBypasses(e, i), e.emitAndNotify("style"), r.transitioning = !1)
	}, za.checkTrigger = function (e, t, n, r, i, a) {
		var o = this.properties[t],
			s = i(o);
		null != s && s(n, r) && a(o)
	}, za.checkZOrderTrigger = function (e, t, n, r) {
		var i = this;
		this.checkTrigger(e, t, n, r, function (e) {
			return e.triggersZOrder
		}, function () {
			i._private.cy.notify("zorder", e)
		})
	}, za.checkBoundsTrigger = function (e, t, n, r) {
		this.checkTrigger(e, t, n, r, function (e) {
			return e.triggersBounds
		}, function (i) {
			e.dirtyCompoundBoundsCache(), e.dirtyBoundingBoxCache(), "bezier" !== e.pstyle("curve-style").value && ("curve-style" !== t || "bezier" !== n && "bezier" !== r) || !i.triggersBoundsOfParallelBeziers || e.parallelEdges().forEach(function (e) {
				e.isBundledBezier() && e.dirtyBoundingBoxCache()
			})
		})
	}, za.checkTriggers = function (e, t, n, r) {
		e.dirtyStyleCache(), this.checkZOrderTrigger(e, t, n, r), this.checkBoundsTrigger(e, t, n, r)
	};
	var La = {
		applyBypass: function (e, t, n, r) {
			var i = [];
			if ("*" === t || "**" === t) {
				if (void 0 !== n)
					for (var a = 0; a < this.properties.length; a++) {
						var o = this.properties[a].name,
							s = this.parse(o, n, !0);
						s && i.push(s)
					}
			} else if (d(t)) {
				var l = this.parse(t, n, !0);
				l && i.push(l)
			} else {
				if (!g(t)) return !1;
				var u = t;
				r = n;
				for (var c = Object.keys(u), h = 0; h < c.length; h++) {
					var p = c[h],
						f = u[p];
					if (void 0 === f && (f = u[T(p)]), void 0 !== f) {
						var v = this.parse(p, f, !0);
						v && i.push(v)
					}
				}
			} if (0 === i.length) return !1;
			for (var y = !1, m = 0; m < e.length; m++) {
				for (var b = e[m], x = {}, w = void 0, E = 0; E < i.length; E++) {
					var k = i[E];
					if (r) {
						var C = b.pstyle(k.name);
						w = x[k.name] = {
							prev: C
						}
					}
					y = this.applyParsedProperty(b, k) || y, r && (w.next = b.pstyle(k.name))
				}
				y && this.updateStyleHints(b), r && this.updateTransitions(b, x, !0)
			}
			return y
		},
		overrideBypass: function (e, t, n) {
			t = P(t);
			for (var r = 0; r < e.length; r++) {
				var i = e[r],
					a = i._private.style[t],
					o = this.properties[t].type,
					s = o.color,
					l = o.mutiple,
					u = a ? null != a.pfValue ? a.pfValue : a.value : null;
				a && a.bypass ? (a.value = n, null != a.pfValue && (a.pfValue = n), a.strValue = s ? "rgb(" + n.join(",") + ")" : l ? n.join(" ") : "" + n, this.updateStyleHints(i)) : this.applyBypass(i, t, n), this.checkTriggers(i, t, u, n)
			}
		},
		removeAllBypasses: function (e, t) {
			return this.removeBypasses(e, this.propertyNames, t)
		},
		removeBypasses: function (e, t, n) {
			for (var r = 0; r < e.length; r++) {
				for (var i = e[r], a = {}, o = 0; o < t.length; o++) {
					var s = t[o],
						l = this.properties[s],
						u = i.pstyle(l.name);
					if (u && u.bypass) {
						var c = this.parse(s, "", !0),
							h = a[l.name] = {
								prev: u
							};
						this.applyParsedProperty(i, c), h.next = i.pstyle(l.name)
					}
				}
				this.updateStyleHints(i), n && this.updateTransitions(i, a, !0)
			}
		}
	},
		Aa = {
			getEmSizeInPixels: function () {
				var e = this.containerCss("font-size");
				return null != e ? parseFloat(e) : 1
			},
			containerCss: function (e) {
				var t = this._private.cy.container();
				if (a && t && a.getComputedStyle) return a.getComputedStyle(t).getPropertyValue(e)
			}
		},
		Oa = {
			getRenderedStyle: function (e, t) {
				return t ? this.getStylePropertyValue(e, t, !0) : this.getRawStyle(e, !0)
			},
			getRawStyle: function (e, t) {
				if (e = e[0]) {
					for (var n = {}, r = 0; r < this.properties.length; r++) {
						var i = this.properties[r],
							a = this.getStylePropertyValue(e, i.name, t);
						null != a && (n[i.name] = a, n[T(i.name)] = a)
					}
					return n
				}
			},
			getIndexedStyle: function (e, t, n, r) {
				var i = e.pstyle(t)[n][r];
				return null != i ? i : e.cy().style().getDefaultProperty(t)[n][0]
			},
			getStylePropertyValue: function (e, t, n) {
				if (e = e[0]) {
					var r = this.properties[t];
					r.alias && (r = r.pointsTo);
					var i = r.type,
						a = e.pstyle(r.name);
					if (a) {
						var o = a.value,
							s = a.units,
							l = a.strValue;
						if (n && i.number && null != o && v(o)) {
							var u = e.cy().zoom(),
								c = function (e) {
									return e * u
								},
								h = function (e, t) {
									return c(e) + t
								},
								p = f(o);
							return (p ? s.every(function (e) {
								return null != e
							}) : null != s) ? p ? o.map(function (e, t) {
								return h(e, s[t])
							}).join(" ") : h(o, s) : p ? o.map(function (e) {
								return d(e) ? e : "" + c(e)
							}).join(" ") : "" + c(o)
						}
						if (null != l) return l
					}
					return null
				}
			},
			getAnimationStartStyle: function (e, t) {
				for (var n = {}, r = 0; r < t.length; r++) {
					var i = t[r].name,
						a = e.pstyle(i);
					void 0 !== a && (a = g(a) ? this.parse(i, a.strValue) : this.parse(i, a)), a && (n[i] = a)
				}
				return n
			},
			getPropsList: function (e) {
				var t = [],
					n = e,
					r = this.properties;
				if (n)
					for (var i = Object.keys(n), a = 0; a < i.length; a++) {
						var o = i[a],
							s = n[o],
							l = r[o] || r[P(o)],
							u = this.parse(l.name, s);
						u && t.push(u)
					}
				return t
			},
			getNonDefaultPropertiesHash: function (e, t, n) {
				var r, i, a, o, s, l, u = n;
				for (s = 0; s < t.length; s++)
					if (r = t[s], null != (i = e.pstyle(r, !1)))
						if (null != i.pfValue) u = ue(o, u);
						else
							for (a = i.strValue, l = 0; l < a.length; l++) o = a.charCodeAt(l), u = ue(o, u);
				return u
			}
		};
	Oa.getPropertiesHash = Oa.getNonDefaultPropertiesHash;
	var Ra = {
		appendFromJson: function (e) {
			for (var t = 0; t < e.length; t++) {
				var n = e[t],
					r = n.selector,
					i = n.style || n.css,
					a = Object.keys(i);
				this.selector(r);
				for (var o = 0; o < a.length; o++) {
					var s = a[o],
						l = i[s];
					this.css(s, l)
				}
			}
			return this
		},
		fromJson: function (e) {
			return this.resetToDefault(), this.appendFromJson(e), this
		},
		json: function () {
			for (var e = [], t = this.defaultLength; t < this.length; t++) {
				for (var n = this[t], r = n.selector, i = n.properties, a = {}, o = 0; o < i.length; o++) {
					var s = i[o];
					a[s.name] = s.strValue
				}
				e.push({
					selector: r ? r.toString() : "core",
					style: a
				})
			}
			return e
		}
	},
		Va = {
			appendFromString: function (e) {
				var t, n, r, i = "" + e;

				function a() {
					i = i.length > t.length ? i.substr(t.length) : ""
				}

				function o() {
					n = n.length > r.length ? n.substr(r.length) : ""
				}
				for (i = i.replace(/[\/][*](\s|.)+?[*][\/]/g, ""); ;) {
					if (i.match(/^\s*$/)) break;
					var s = i.match(/^\s*((?:.|\s)+?)\s*\{((?:.|\s)+?)\}/);
					if (!s) {
						Ce("Halting stylesheet parsing: String stylesheet contains more to parse but no selector and block found in: " + i);
						break
					}
					t = s[0];
					var l = s[1];
					if ("core" !== l)
						if (new Ir(l).invalid) {
							Ce("Skipping parsing of block: Invalid selector found in string stylesheet: " + l), a();
							continue
						}
					var u = s[2],
						c = !1;
					n = u;
					for (var h = []; ;) {
						if (n.match(/^\s*$/)) break;
						var d = n.match(/^\s*(.+?)\s*:\s*(.+?)\s*;/);
						if (!d) {
							Ce("Skipping parsing of block: Invalid formatting of style property and value definitions found in:" + u), c = !0;
							break
						}
						r = d[0];
						var p = d[1],
							f = d[2];
						if (this.properties[p]) this.parse(p, f) ? (h.push({
							name: p,
							val: f
						}), o()) : (Ce("Skipping property: Invalid property definition in: " + r), o());
						else Ce("Skipping property: Invalid property name in: " + r), o()
					}
					if (c) {
						a();
						break
					}
					this.selector(l);
					for (var g = 0; g < h.length; g++) {
						var v = h[g];
						this.css(v.name, v.val)
					}
					a()
				}
				return this
			},
			fromString: function (e) {
				return this.resetToDefault(), this.appendFromString(e), this
			}
		},
		Fa = {};
	! function () {
		var e = _,
			t = function (e) {
				return "^" + e + "\\s*\\(\\s*([\\w\\.]+)\\s*\\)$"
			},
			n = function (t) {
				var n = e + "|\\w+|rgb[a]?\\((?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%]?)\\s*,\\s*(?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%]?)\\s*,\\s*(?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%]?)(?:\\s*,\\s*(?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))))?\\)|hsl[a]?\\((?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?)))\\s*,\\s*(?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%])\\s*,\\s*(?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))[%])(?:\\s*,\\s*(?:(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))))?\\)|\\#[0-9a-fA-F]{3}|\\#[0-9a-fA-F]{6}";
				return "^" + t + "\\s*\\(([\\w\\.]+)\\s*\\,\\s*(" + e + ")\\s*\\,\\s*(" + e + ")\\s*,\\s*(" + n + ")\\s*\\,\\s*(" + n + ")\\)$"
			},
			r = ["^url\\s*\\(\\s*['\"]?(.+?)['\"]?\\s*\\)$", "^(none)$", "^(.+)$"];
		Fa.types = {
			time: {
				number: !0,
				min: 0,
				units: "s|ms",
				implicitUnits: "ms"
			},
			percent: {
				number: !0,
				min: 0,
				max: 100,
				units: "%",
				implicitUnits: "%"
			},
			percentages: {
				number: !0,
				min: 0,
				max: 100,
				units: "%",
				implicitUnits: "%",
				multiple: !0
			},
			zeroOneNumber: {
				number: !0,
				min: 0,
				max: 1,
				unitless: !0
			},
			zeroOneNumbers: {
				number: !0,
				min: 0,
				max: 1,
				unitless: !0,
				multiple: !0
			},
			nOneOneNumber: {
				number: !0,
				min: -1,
				max: 1,
				unitless: !0
			},
			nonNegativeInt: {
				number: !0,
				min: 0,
				integer: !0,
				unitless: !0
			},
			position: {
				enums: ["parent", "origin"]
			},
			nodeSize: {
				number: !0,
				min: 0,
				enums: ["label"]
			},
			number: {
				number: !0,
				unitless: !0
			},
			numbers: {
				number: !0,
				unitless: !0,
				multiple: !0
			},
			positiveNumber: {
				number: !0,
				unitless: !0,
				min: 0,
				strictMin: !0
			},
			size: {
				number: !0,
				min: 0
			},
			bidirectionalSize: {
				number: !0
			},
			bidirectionalSizes: {
				number: !0,
				multiple: !0
			},
			sizeMaybePercent: {
				number: !0,
				min: 0,
				allowPercent: !0
			},
			axisDirection: {
				enums: ["horizontal", "leftward", "rightward", "vertical", "upward", "downward", "auto"]
			},
			paddingRelativeTo: {
				enums: ["width", "height", "average", "min", "max"]
			},
			bgWH: {
				number: !0,
				min: 0,
				allowPercent: !0,
				enums: ["auto"],
				multiple: !0
			},
			bgPos: {
				number: !0,
				allowPercent: !0,
				multiple: !0
			},
			bgRelativeTo: {
				enums: ["inner", "include-padding"],
				multiple: !0
			},
			bgRepeat: {
				enums: ["repeat", "repeat-x", "repeat-y", "no-repeat"],
				multiple: !0
			},
			bgFit: {
				enums: ["none", "contain", "cover"],
				multiple: !0
			},
			bgCrossOrigin: {
				enums: ["anonymous", "use-credentials"],
				multiple: !0
			},
			bgClip: {
				enums: ["none", "node"],
				multiple: !0
			},
			color: {
				color: !0
			},
			colors: {
				color: !0,
				multiple: !0
			},
			fill: {
				enums: ["solid", "linear-gradient", "radial-gradient"]
			},
			bool: {
				enums: ["yes", "no"]
			},
			lineStyle: {
				enums: ["solid", "dotted", "dashed"]
			},
			lineCap: {
				enums: ["butt", "round", "square"]
			},
			borderStyle: {
				enums: ["solid", "dotted", "dashed", "double"]
			},
			curveStyle: {
				enums: ["bezier", "unbundled-bezier", "haystack", "segments", "straight", "taxi"]
			},
			fontFamily: {
				regex: '^([\\w- \\"]+(?:\\s*,\\s*[\\w- \\"]+)*)$'
			},
			fontStyle: {
				enums: ["italic", "normal", "oblique"]
			},
			fontWeight: {
				enums: ["normal", "bold", "bolder", "lighter", "100", "200", "300", "400", "500", "600", "800", "900", 100, 200, 300, 400, 500, 600, 700, 800, 900]
			},
			textDecoration: {
				enums: ["none", "underline", "overline", "line-through"]
			},
			textTransform: {
				enums: ["none", "uppercase", "lowercase"]
			},
			textWrap: {
				enums: ["none", "wrap", "ellipsis"]
			},
			textOverflowWrap: {
				enums: ["whitespace", "anywhere"]
			},
			textBackgroundShape: {
				enums: ["rectangle", "roundrectangle", "round-rectangle"]
			},
			nodeShape: {
				enums: ["rectangle", "roundrectangle", "round-rectangle", "cutrectangle", "cut-rectangle", "bottomroundrectangle", "bottom-round-rectangle", "barrel", "ellipse", "triangle", "square", "pentagon", "hexagon", "concavehexagon", "concave-hexagon", "heptagon", "octagon", "tag", "star", "diamond", "vee", "rhomboid", "polygon"]
			},
			compoundIncludeLabels: {
				enums: ["include", "exclude"]
			},
			arrowShape: {
				enums: ["tee", "triangle", "triangle-tee", "triangle-cross", "triangle-backcurve", "vee", "square", "circle", "diamond", "chevron", "none"]
			},
			arrowFill: {
				enums: ["filled", "hollow"]
			},
			display: {
				enums: ["element", "none"]
			},
			visibility: {
				enums: ["hidden", "visible"]
			},
			zCompoundDepth: {
				enums: ["bottom", "orphan", "auto", "top"]
			},
			zIndexCompare: {
				enums: ["auto", "manual"]
			},
			valign: {
				enums: ["top", "center", "bottom"]
			},
			halign: {
				enums: ["left", "center", "right"]
			},
			justification: {
				enums: ["left", "center", "right", "auto"]
			},
			text: {
				string: !0
			},
			data: {
				mapping: !0,
				regex: t("data")
			},
			layoutData: {
				mapping: !0,
				regex: t("layoutData")
			},
			scratch: {
				mapping: !0,
				regex: t("scratch")
			},
			mapData: {
				mapping: !0,
				regex: n("mapData")
			},
			mapLayoutData: {
				mapping: !0,
				regex: n("mapLayoutData")
			},
			mapScratch: {
				mapping: !0,
				regex: n("mapScratch")
			},
			fn: {
				mapping: !0,
				fn: !0
			},
			url: {
				regexes: r,
				singleRegexMatchValue: !0
			},
			urls: {
				regexes: r,
				singleRegexMatchValue: !0,
				multiple: !0
			},
			propList: {
				propList: !0
			},
			angle: {
				number: !0,
				units: "deg|rad",
				implicitUnits: "rad"
			},
			textRotation: {
				number: !0,
				units: "deg|rad",
				implicitUnits: "rad",
				enums: ["none", "autorotate"]
			},
			polygonPointList: {
				number: !0,
				multiple: !0,
				evenMultiple: !0,
				min: -1,
				max: 1,
				unitless: !0
			},
			edgeDistances: {
				enums: ["intersection", "node-position"]
			},
			edgeEndpoint: {
				number: !0,
				multiple: !0,
				units: "%|px|em|deg|rad",
				implicitUnits: "px",
				enums: ["inside-to-node", "outside-to-node", "outside-to-node-or-label", "outside-to-line", "outside-to-line-or-label"],
				singleEnum: !0,
				validate: function (e, t) {
					switch (e.length) {
						case 2:
							return "deg" !== t[0] && "rad" !== t[0] && "deg" !== t[1] && "rad" !== t[1];
						case 1:
							return d(e[0]) || "deg" === t[0] || "rad" === t[0];
						default:
							return !1
					}
				}
			},
			easing: {
				regexes: ["^(spring)\\s*\\(\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*\\)$", "^(cubic-bezier)\\s*\\(\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*\\)$"],
				enums: ["linear", "ease", "ease-in", "ease-out", "ease-in-out", "ease-in-sine", "ease-out-sine", "ease-in-out-sine", "ease-in-quad", "ease-out-quad", "ease-in-out-quad", "ease-in-cubic", "ease-out-cubic", "ease-in-out-cubic", "ease-in-quart", "ease-out-quart", "ease-in-out-quart", "ease-in-quint", "ease-out-quint", "ease-in-out-quint", "ease-in-expo", "ease-out-expo", "ease-in-out-expo", "ease-in-circ", "ease-out-circ", "ease-in-out-circ"]
			},
			gradientDirection: {
				enums: ["to-bottom", "to-top", "to-left", "to-right", "to-bottom-right", "to-bottom-left", "to-top-right", "to-top-left", "to-right-bottom", "to-left-bottom", "to-right-top", "to-left-top"]
			}
		};
		var i = {
			zeroNonZero: function (e, t) {
				return (null == e || null == t) && e !== t || (0 == e && 0 != t || 0 != e && 0 == t)
			},
			any: function (e, t) {
				return e != t
			}
		},
			a = Fa.types,
			o = [{
				name: "label",
				type: a.text,
				triggersBounds: i.any
			}, {
				name: "text-rotation",
				type: a.textRotation,
				triggersBounds: i.any
			}, {
				name: "text-margin-x",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "text-margin-y",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}],
			s = [{
				name: "source-label",
				type: a.text,
				triggersBounds: i.any
			}, {
				name: "source-text-rotation",
				type: a.textRotation,
				triggersBounds: i.any
			}, {
				name: "source-text-margin-x",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "source-text-margin-y",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "source-text-offset",
				type: a.size,
				triggersBounds: i.any
			}],
			l = [{
				name: "target-label",
				type: a.text,
				triggersBounds: i.any
			}, {
				name: "target-text-rotation",
				type: a.textRotation,
				triggersBounds: i.any
			}, {
				name: "target-text-margin-x",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "target-text-margin-y",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "target-text-offset",
				type: a.size,
				triggersBounds: i.any
			}],
			u = [{
				name: "font-family",
				type: a.fontFamily,
				triggersBounds: i.any
			}, {
				name: "font-style",
				type: a.fontStyle,
				triggersBounds: i.any
			}, {
				name: "font-weight",
				type: a.fontWeight,
				triggersBounds: i.any
			}, {
				name: "font-size",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "text-transform",
				type: a.textTransform,
				triggersBounds: i.any
			}, {
				name: "text-wrap",
				type: a.textWrap,
				triggersBounds: i.any
			}, {
				name: "text-overflow-wrap",
				type: a.textOverflowWrap,
				triggersBounds: i.any
			}, {
				name: "text-max-width",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "text-outline-width",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "line-height",
				type: a.positiveNumber,
				triggersBounds: i.any
			}],
			c = [{
				name: "text-valign",
				type: a.valign,
				triggersBounds: i.any
			}, {
				name: "text-halign",
				type: a.halign,
				triggersBounds: i.any
			}, {
				name: "color",
				type: a.color
			}, {
				name: "text-outline-color",
				type: a.color
			}, {
				name: "text-outline-opacity",
				type: a.zeroOneNumber
			}, {
				name: "text-background-color",
				type: a.color
			}, {
				name: "text-background-opacity",
				type: a.zeroOneNumber
			}, {
				name: "text-background-padding",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "text-border-opacity",
				type: a.zeroOneNumber
			}, {
				name: "text-border-color",
				type: a.color
			}, {
				name: "text-border-width",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "text-border-style",
				type: a.borderStyle,
				triggersBounds: i.any
			}, {
				name: "text-background-shape",
				type: a.textBackgroundShape,
				triggersBounds: i.any
			}, {
				name: "text-justification",
				type: a.justification
			}],
			h = [{
				name: "events",
				type: a.bool
			}, {
				name: "text-events",
				type: a.bool
			}],
			p = [{
				name: "display",
				type: a.display,
				triggersZOrder: i.any,
				triggersBounds: i.any,
				triggersBoundsOfParallelBeziers: !0
			}, {
				name: "visibility",
				type: a.visibility,
				triggersZOrder: i.any
			}, {
				name: "opacity",
				type: a.zeroOneNumber,
				triggersZOrder: i.zeroNonZero
			}, {
				name: "text-opacity",
				type: a.zeroOneNumber
			}, {
				name: "min-zoomed-font-size",
				type: a.size
			}, {
				name: "z-compound-depth",
				type: a.zCompoundDepth,
				triggersZOrder: i.any
			}, {
				name: "z-index-compare",
				type: a.zIndexCompare,
				triggersZOrder: i.any
			}, {
				name: "z-index",
				type: a.nonNegativeInt,
				triggersZOrder: i.any
			}],
			f = [{
				name: "overlay-padding",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "overlay-color",
				type: a.color
			}, {
				name: "overlay-opacity",
				type: a.zeroOneNumber,
				triggersBounds: i.zeroNonZero
			}],
			g = [{
				name: "transition-property",
				type: a.propList
			}, {
				name: "transition-duration",
				type: a.time
			}, {
				name: "transition-delay",
				type: a.time
			}, {
				name: "transition-timing-function",
				type: a.easing
			}],
			v = function (e, t) {
				return "label" === t.value ? -e.poolIndex() : t.pfValue
			},
			y = [{
				name: "height",
				type: a.nodeSize,
				triggersBounds: i.any,
				hashOverride: v
			}, {
				name: "width",
				type: a.nodeSize,
				triggersBounds: i.any,
				hashOverride: v
			}, {
				name: "shape",
				type: a.nodeShape,
				triggersBounds: i.any
			}, {
				name: "shape-polygon-points",
				type: a.polygonPointList,
				triggersBounds: i.any
			}, {
				name: "background-color",
				type: a.color
			}, {
				name: "background-fill",
				type: a.fill
			}, {
				name: "background-opacity",
				type: a.zeroOneNumber
			}, {
				name: "background-blacken",
				type: a.nOneOneNumber
			}, {
				name: "background-gradient-stop-colors",
				type: a.colors
			}, {
				name: "background-gradient-stop-positions",
				type: a.percentages
			}, {
				name: "background-gradient-direction",
				type: a.gradientDirection
			}, {
				name: "padding",
				type: a.sizeMaybePercent,
				triggersBounds: i.any
			}, {
				name: "padding-relative-to",
				type: a.paddingRelativeTo,
				triggersBounds: i.any
			}, {
				name: "bounds-expansion",
				type: a.size,
				triggersBounds: i.any
			}],
			m = [{
				name: "border-color",
				type: a.color
			}, {
				name: "border-opacity",
				type: a.zeroOneNumber
			}, {
				name: "border-width",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "border-style",
				type: a.borderStyle
			}],
			b = [{
				name: "background-image",
				type: a.urls
			}, {
				name: "background-image-crossorigin",
				type: a.bgCrossOrigin
			}, {
				name: "background-image-opacity",
				type: a.zeroOneNumbers
			}, {
				name: "background-position-x",
				type: a.bgPos
			}, {
				name: "background-position-y",
				type: a.bgPos
			}, {
				name: "background-width-relative-to",
				type: a.bgRelativeTo
			}, {
				name: "background-height-relative-to",
				type: a.bgRelativeTo
			}, {
				name: "background-repeat",
				type: a.bgRepeat
			}, {
				name: "background-fit",
				type: a.bgFit
			}, {
				name: "background-clip",
				type: a.bgClip
			}, {
				name: "background-width",
				type: a.bgWH
			}, {
				name: "background-height",
				type: a.bgWH
			}, {
				name: "background-offset-x",
				type: a.bgPos
			}, {
				name: "background-offset-y",
				type: a.bgPos
			}],
			x = [{
				name: "position",
				type: a.position,
				triggersBounds: i.any
			}, {
				name: "compound-sizing-wrt-labels",
				type: a.compoundIncludeLabels,
				triggersBounds: i.any
			}, {
				name: "min-width",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "min-width-bias-left",
				type: a.sizeMaybePercent,
				triggersBounds: i.any
			}, {
				name: "min-width-bias-right",
				type: a.sizeMaybePercent,
				triggersBounds: i.any
			}, {
				name: "min-height",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "min-height-bias-top",
				type: a.sizeMaybePercent,
				triggersBounds: i.any
			}, {
				name: "min-height-bias-bottom",
				type: a.sizeMaybePercent,
				triggersBounds: i.any
			}],
			w = [{
				name: "line-style",
				type: a.lineStyle
			}, {
				name: "line-color",
				type: a.color
			}, {
				name: "line-fill",
				type: a.fill
			}, {
				name: "line-cap",
				type: a.lineCap
			}, {
				name: "line-dash-pattern",
				type: a.numbers
			}, {
				name: "line-dash-offset",
				type: a.number
			}, {
				name: "line-gradient-stop-colors",
				type: a.colors
			}, {
				name: "line-gradient-stop-positions",
				type: a.percentages
			}, {
				name: "curve-style",
				type: a.curveStyle,
				triggersBounds: i.any,
				triggersBoundsOfParallelBeziers: !0
			}, {
				name: "haystack-radius",
				type: a.zeroOneNumber,
				triggersBounds: i.any
			}, {
				name: "source-endpoint",
				type: a.edgeEndpoint,
				triggersBounds: i.any
			}, {
				name: "target-endpoint",
				type: a.edgeEndpoint,
				triggersBounds: i.any
			}, {
				name: "control-point-step-size",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "control-point-distances",
				type: a.bidirectionalSizes,
				triggersBounds: i.any
			}, {
				name: "control-point-weights",
				type: a.numbers,
				triggersBounds: i.any
			}, {
				name: "segment-distances",
				type: a.bidirectionalSizes,
				triggersBounds: i.any
			}, {
				name: "segment-weights",
				type: a.numbers,
				triggersBounds: i.any
			}, {
				name: "taxi-turn",
				type: a.sizeMaybePercent,
				triggersBounds: i.any
			}, {
				name: "taxi-turn-min-distance",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "taxi-direction",
				type: a.axisDirection,
				triggersBounds: i.any
			}, {
				name: "edge-distances",
				type: a.edgeDistances,
				triggersBounds: i.any
			}, {
				name: "arrow-scale",
				type: a.positiveNumber,
				triggersBounds: i.any
			}, {
				name: "loop-direction",
				type: a.angle,
				triggersBounds: i.any
			}, {
				name: "loop-sweep",
				type: a.angle,
				triggersBounds: i.any
			}, {
				name: "source-distance-from-node",
				type: a.size,
				triggersBounds: i.any
			}, {
				name: "target-distance-from-node",
				type: a.size,
				triggersBounds: i.any
			}],
			E = [{
				name: "ghost",
				type: a.bool,
				triggersBounds: i.any
			}, {
				name: "ghost-offset-x",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "ghost-offset-y",
				type: a.bidirectionalSize,
				triggersBounds: i.any
			}, {
				name: "ghost-opacity",
				type: a.zeroOneNumber
			}],
			k = [{
				name: "selection-box-color",
				type: a.color
			}, {
				name: "selection-box-opacity",
				type: a.zeroOneNumber
			}, {
				name: "selection-box-border-color",
				type: a.color
			}, {
				name: "selection-box-border-width",
				type: a.size
			}, {
				name: "active-bg-color",
				type: a.color
			}, {
				name: "active-bg-opacity",
				type: a.zeroOneNumber
			}, {
				name: "active-bg-size",
				type: a.size
			}, {
				name: "outside-texture-bg-color",
				type: a.color
			}, {
				name: "outside-texture-bg-opacity",
				type: a.zeroOneNumber
			}],
			C = [];
		Fa.pieBackgroundN = 16, C.push({
			name: "pie-size",
			type: a.sizeMaybePercent
		});
		for (var S = 1; S <= Fa.pieBackgroundN; S++) C.push({
			name: "pie-" + S + "-background-color",
			type: a.color
		}), C.push({
			name: "pie-" + S + "-background-size",
			type: a.percent
		}), C.push({
			name: "pie-" + S + "-background-opacity",
			type: a.zeroOneNumber
		});
		var D = [],
			P = Fa.arrowPrefixes = ["source", "mid-source", "target", "mid-target"];
		[{
			name: "arrow-shape",
			type: a.arrowShape,
			triggersBounds: i.any
		}, {
			name: "arrow-color",
			type: a.color
		}, {
			name: "arrow-fill",
			type: a.arrowFill
		}].forEach(function (e) {
			P.forEach(function (t) {
				var n = t + "-" + e.name,
					r = e.type,
					i = e.triggersBounds;
				D.push({
					name: n,
					type: r,
					triggersBounds: i
				})
			})
		}, {});
		var T = Fa.properties = [].concat(h, g, p, f, E, c, u, o, s, l, y, m, b, C, x, w, D, k),
			M = Fa.propertyGroups = {
				behavior: h,
				transition: g,
				visibility: p,
				overlay: f,
				ghost: E,
				commonLabel: c,
				labelDimensions: u,
				mainLabel: o,
				sourceLabel: s,
				targetLabel: l,
				nodeBody: y,
				nodeBorder: m,
				backgroundImage: b,
				pie: C,
				compound: x,
				edgeLine: w,
				edgeArrow: D,
				core: k
			},
			B = Fa.propertyGroupNames = {};
		(Fa.propertyGroupKeys = Object.keys(M)).forEach(function (e) {
			B[e] = M[e].map(function (e) {
				return e.name
			}), M[e].forEach(function (t) {
				return t.groupKey = e
			})
		});
		var N = Fa.aliases = [{
			name: "content",
			pointsTo: "label"
		}, {
			name: "control-point-distance",
			pointsTo: "control-point-distances"
		}, {
			name: "control-point-weight",
			pointsTo: "control-point-weights"
		}, {
			name: "edge-text-rotation",
			pointsTo: "text-rotation"
		}, {
			name: "padding-left",
			pointsTo: "padding"
		}, {
			name: "padding-right",
			pointsTo: "padding"
		}, {
			name: "padding-top",
			pointsTo: "padding"
		}, {
			name: "padding-bottom",
			pointsTo: "padding"
		}];
		Fa.propertyNames = T.map(function (e) {
			return e.name
		});
		for (var I = 0; I < T.length; I++) {
			var z = T[I];
			T[z.name] = z
		}
		for (var L = 0; L < N.length; L++) {
			var A = N[L],
				O = T[A.pointsTo],
				R = {
					name: A.name,
					alias: !0,
					pointsTo: O
				};
			T.push(R), T[A.name] = R
		}
	}(), Fa.getDefaultProperty = function (e) {
		return this.getDefaultProperties()[e]
	}, Fa.getDefaultProperties = function () {
		var e = this._private;
		if (null != e.defaultProperties) return e.defaultProperties;
		for (var t = I({
			"selection-box-color": "#ddd",
			"selection-box-opacity": .65,
			"selection-box-border-color": "#aaa",
			"selection-box-border-width": 1,
			"active-bg-color": "black",
			"active-bg-opacity": .15,
			"active-bg-size": 30,
			"outside-texture-bg-color": "#000",
			"outside-texture-bg-opacity": .125,
			events: "yes",
			"text-events": "no",
			"text-valign": "top",
			"text-halign": "center",
			"text-justification": "auto",
			"line-height": 1,
			color: "#000",
			"text-outline-color": "#000",
			"text-outline-width": 0,
			"text-outline-opacity": 1,
			"text-opacity": 1,
			"text-decoration": "none",
			"text-transform": "none",
			"text-wrap": "none",
			"text-overflow-wrap": "whitespace",
			"text-max-width": 9999,
			"text-background-color": "#000",
			"text-background-opacity": 0,
			"text-background-shape": "rectangle",
			"text-background-padding": 0,
			"text-border-opacity": 0,
			"text-border-width": 0,
			"text-border-style": "solid",
			"text-border-color": "#000",
			"font-family": "Helvetica Neue, Helvetica, sans-serif",
			"font-style": "normal",
			"font-weight": "normal",
			"font-size": 16,
			"min-zoomed-font-size": 0,
			"text-rotation": "none",
			"source-text-rotation": "none",
			"target-text-rotation": "none",
			visibility: "visible",
			display: "element",
			opacity: 1,
			"z-compound-depth": "auto",
			"z-index-compare": "auto",
			"z-index": 0,
			label: "",
			"text-margin-x": 0,
			"text-margin-y": 0,
			"source-label": "",
			"source-text-offset": 0,
			"source-text-margin-x": 0,
			"source-text-margin-y": 0,
			"target-label": "",
			"target-text-offset": 0,
			"target-text-margin-x": 0,
			"target-text-margin-y": 0,
			"overlay-opacity": 0,
			"overlay-color": "#000",
			"overlay-padding": 10,
			"transition-property": "none",
			"transition-duration": 0,
			"transition-delay": 0,
			"transition-timing-function": "linear",
			"background-blacken": 0,
			"background-color": "#999",
			"background-fill": "solid",
			"background-opacity": 1,
			"background-image": "none",
			"background-image-crossorigin": "anonymous",
			"background-image-opacity": 1,
			"background-position-x": "50%",
			"background-position-y": "50%",
			"background-offset-x": 0,
			"background-offset-y": 0,
			"background-width-relative-to": "include-padding",
			"background-height-relative-to": "include-padding",
			"background-repeat": "no-repeat",
			"background-fit": "none",
			"background-clip": "node",
			"background-width": "auto",
			"background-height": "auto",
			"border-color": "#000",
			"border-opacity": 1,
			"border-width": 0,
			"border-style": "solid",
			height: 30,
			width: 30,
			shape: "ellipse",
			"shape-polygon-points": "-1, -1,   1, -1,   1, 1,   -1, 1",
			"bounds-expansion": 0,
			"background-gradient-direction": "to-bottom",
			"background-gradient-stop-colors": "#999",
			"background-gradient-stop-positions": "0%",
			ghost: "no",
			"ghost-offset-y": 0,
			"ghost-offset-x": 0,
			"ghost-opacity": 0,
			padding: 0,
			"padding-relative-to": "width",
			position: "origin",
			"compound-sizing-wrt-labels": "include",
			"min-width": 0,
			"min-width-bias-left": 0,
			"min-width-bias-right": 0,
			"min-height": 0,
			"min-height-bias-top": 0,
			"min-height-bias-bottom": 0
		}, {
				"pie-size": "100%"
			}, [{
				name: "pie-{{i}}-background-color",
				value: "black"
			}, {
				name: "pie-{{i}}-background-size",
				value: "0%"
			}, {
				name: "pie-{{i}}-background-opacity",
				value: 1
			}].reduce(function (e, t) {
				for (var n = 1; n <= Fa.pieBackgroundN; n++) {
					var r = t.name.replace("{{i}}", n),
						i = t.value;
					e[r] = i
				}
				return e
			}, {}), {
				"line-style": "solid",
				"line-color": "#999",
				"line-fill": "solid",
				"line-cap": "butt",
				"line-gradient-stop-colors": "#999",
				"line-gradient-stop-positions": "0%",
				"control-point-step-size": 40,
				"control-point-weights": .5,
				"segment-weights": .5,
				"segment-distances": 20,
				"taxi-turn": "50%",
				"taxi-turn-min-distance": 10,
				"taxi-direction": "auto",
				"edge-distances": "intersection",
				"curve-style": "haystack",
				"haystack-radius": 0,
				"arrow-scale": 1,
				"loop-direction": "-45deg",
				"loop-sweep": "-90deg",
				"source-distance-from-node": 0,
				"target-distance-from-node": 0,
				"source-endpoint": "outside-to-node",
				"target-endpoint": "outside-to-node",
				"line-dash-pattern": [6, 3],
				"line-dash-offset": 0
			}, [{
				name: "arrow-shape",
				value: "none"
			}, {
				name: "arrow-color",
				value: "#999"
			}, {
				name: "arrow-fill",
				value: "filled"
			}].reduce(function (e, t) {
				return Fa.arrowPrefixes.forEach(function (n) {
					var r = n + "-" + t.name,
						i = t.value;
					e[r] = i
				}), e
			}, {})), n = {}, r = 0; r < this.properties.length; r++) {
			var i = this.properties[r];
			if (!i.pointsTo) {
				var a = i.name,
					o = t[a],
					s = this.parse(a, o);
				n[a] = s
			}
		}
		return e.defaultProperties = n, e.defaultProperties
	}, Fa.addDefaultStylesheet = function () {
		this.selector(":parent").css({
			shape: "rectangle",
			padding: 10,
			"background-color": "#eee",
			"border-color": "#ccc",
			"border-width": 1
		}).selector("edge").css({
			width: 3
		}).selector(":loop").css({
			"curve-style": "bezier"
		}).selector("edge:compound").css({
			"curve-style": "bezier",
			"source-endpoint": "outside-to-line",
			"target-endpoint": "outside-to-line"
		}).selector(":selected").css({
			"background-color": "#0169D9",
			"line-color": "#0169D9",
			"source-arrow-color": "#0169D9",
			"target-arrow-color": "#0169D9",
			"mid-source-arrow-color": "#0169D9",
			"mid-target-arrow-color": "#0169D9"
		}).selector(":parent:selected").css({
			"background-color": "#CCE1F9",
			"border-color": "#aec8e5"
		}).selector(":active").css({
			"overlay-color": "black",
			"overlay-padding": 10,
			"overlay-opacity": .25
		}), this.defaultLength = this.length
	};
	var qa = {
		parse: function (e, t, n, r) {
			if (p(t)) return this.parseImplWarn(e, t, n, r);
			var i, a = de(e, "" + t, n ? "t" : "f", "mapping" === r || !0 === r || !1 === r || null == r ? "dontcare" : r),
				o = this.propCache = this.propCache || [];
			return (i = o[a]) || (i = o[a] = this.parseImplWarn(e, t, n, r)), (n || "mapping" === r) && (i = Se(i)) && (i.value = Se(i.value)), i
		},
		parseImplWarn: function (e, t, n, r) {
			var i = this.parseImpl(e, t, n, r);
			return i || null == t || Ce("The style property `".concat(e, ": ").concat(t, "` is invalid")), i
		}
	};
	qa.parseImpl = function (e, t, n, r) {
		e = P(e);
		var i = this.properties[e],
			a = t,
			o = this.types;
		if (!i) return null;
		if (void 0 === t) return null;
		i.alias && (i = i.pointsTo, e = i.name);
		var s = d(t);
		s && (t = t.trim());
		var l, u, c = i.type;
		if (!c) return null;
		if (n && ("" === t || null === t)) return {
			name: e,
			value: t,
			bypass: !0,
			deleteBypass: !0
		};
		if (p(t)) return {
			name: e,
			value: t,
			strValue: "fn",
			mapped: o.fn,
			bypass: n
		};
		if (!s || r || t.length < 7 || "a" !== t[1]);
		else {
			if (t.length >= 7 && "d" === t[0] && (l = new RegExp(o.data.regex).exec(t))) {
				if (n) return !1;
				var h = o.data;
				return {
					name: e,
					value: l,
					strValue: "" + t,
					mapped: h,
					field: l[1],
					bypass: n
				}
			}
			if (t.length >= 10 && "m" === t[0] && (u = new RegExp(o.mapData.regex).exec(t))) {
				if (n) return !1;
				if (c.multiple) return !1;
				var g = o.mapData;
				if (!c.color && !c.number) return !1;
				var y = this.parse(e, u[4]);
				if (!y || y.mapped) return !1;
				var m = this.parse(e, u[5]);
				if (!m || m.mapped) return !1;
				if (y.pfValue === m.pfValue || y.strValue === m.strValue) return Ce("`" + e + ": " + t + "` is not a valid mapper because the output range is zero; converting to `" + e + ": " + y.strValue + "`"), this.parse(e, y.strValue);
				if (c.color) {
					var b = y.value,
						x = m.value;
					if (!(b[0] !== x[0] || b[1] !== x[1] || b[2] !== x[2] || b[3] !== x[3] && (null != b[3] && 1 !== b[3] || null != x[3] && 1 !== x[3]))) return !1
				}
				return {
					name: e,
					value: u,
					strValue: "" + t,
					mapped: g,
					field: u[1],
					fieldMin: parseFloat(u[2]),
					fieldMax: parseFloat(u[3]),
					valueMin: y.value,
					valueMax: m.value,
					bypass: n
				}
			}
		} if (c.multiple && "multiple" !== r) {
			var w;
			if (w = s ? t.split(/\s+/) : f(t) ? t : [t], c.evenMultiple && w.length % 2 != 0) return null;
			for (var E = [], k = [], C = [], S = "", D = !1, T = 0; T < w.length; T++) {
				var M = this.parse(e, w[T], n, "multiple");
				D = D || d(M.value), E.push(M.value), C.push(null != M.pfValue ? M.pfValue : M.value), k.push(M.units), S += (T > 0 ? " " : "") + M.strValue
			}
			return c.validate && !c.validate(E, k) ? null : c.singleEnum && D ? 1 === E.length && d(E[0]) ? {
				name: e,
				value: E[0],
				strValue: E[0],
				bypass: n
			} : null : {
					name: e,
					value: E,
					pfValue: C,
					strValue: S,
					bypass: n,
					units: k
				}
		}
		var B, N, I = function () {
			for (var r = 0; r < c.enums.length; r++) {
				if (c.enums[r] === t) return {
					name: e,
					value: t,
					strValue: "" + t,
					bypass: n
				}
			}
			return null
		};
		if (c.number) {
			var L, A = "px";
			if (c.units && (L = c.units), c.implicitUnits && (A = c.implicitUnits), !c.unitless)
				if (s) {
					var O = "px|em" + (c.allowPercent ? "|\\%" : "");
					L && (O = L);
					var R = t.match("^(" + _ + ")(" + O + ")?$");
					R && (t = R[1], L = R[2] || A)
				} else L && !c.implicitUnits || (L = A);
			if (t = parseFloat(t), isNaN(t) && void 0 === c.enums) return null;
			if (isNaN(t) && void 0 !== c.enums) return t = a, I();
			if (c.integer && (!v(N = t) || Math.floor(N) !== N)) return null;
			if (void 0 !== c.min && (t < c.min || c.strictMin && t === c.min) || void 0 !== c.max && (t > c.max || c.strictMax && t === c.max)) return null;
			var V = {
				name: e,
				value: t,
				strValue: "" + t + (L || ""),
				units: L,
				bypass: n
			};
			return c.unitless || "px" !== L && "em" !== L ? V.pfValue = t : V.pfValue = "px" !== L && L ? this.getEmSizeInPixels() * t : t, "ms" !== L && "s" !== L || (V.pfValue = "ms" === L ? t : 1e3 * t), "deg" !== L && "rad" !== L || (V.pfValue = "rad" === L ? t : (B = t, Math.PI * B / 180)), "%" === L && (V.pfValue = t / 100), V
		}
		if (c.propList) {
			var F = [],
				q = "" + t;
			if ("none" === q);
			else {
				for (var Y = q.split(/\s*,\s*|\s+/), j = 0; j < Y.length; j++) {
					var X = Y[j].trim();
					this.properties[X] ? F.push(X) : Ce("`" + X + "` is not a valid property name")
				}
				if (0 === F.length) return null
			}
			return {
				name: e,
				value: F,
				strValue: 0 === F.length ? "none" : F.join(" "),
				bypass: n
			}
		}
		if (c.color) {
			var W = z(t);
			return W ? {
				name: e,
				value: W,
				pfValue: W,
				strValue: "rgb(" + W[0] + "," + W[1] + "," + W[2] + ")",
				bypass: n
			} : null
		}
		if (c.regex || c.regexes) {
			if (c.enums) {
				var H = I();
				if (H) return H
			}
			for (var K = c.regexes ? c.regexes : [c.regex], G = 0; G < K.length; G++) {
				var Z = new RegExp(K[G]).exec(t);
				if (Z) return {
					name: e,
					value: c.singleRegexMatchValue ? Z[1] : Z,
					strValue: "" + t,
					bypass: n
				}
			}
			return null
		}
		return c.string ? {
			name: e,
			value: "" + t,
			strValue: "" + t,
			bypass: n
		} : c.enums ? I() : null
	};
	var Ya = function e(t) {
		if (!(this instanceof e)) return new e(t);
		w(t) ? (this._private = {
			cy: t,
			coreStyle: {}
		}, this.length = 0, this.resetToDefault()) : Ee("A style must have a core reference")
	},
		ja = Ya.prototype;
	ja.instanceString = function () {
		return "style"
	}, ja.clear = function () {
		for (var e = 0; e < this.length; e++) this[e] = void 0;
		return this.length = 0, this._private.newStyle = !0, this
	}, ja.resetToDefault = function () {
		return this.clear(), this.addDefaultStylesheet(), this
	}, ja.core = function (e) {
		return this._private.coreStyle[e] || this.getDefaultProperty(e)
	}, ja.selector = function (e) {
		var t = "core" === e ? null : new Ir(e),
			n = this.length++;
		return this[n] = {
			selector: t,
			properties: [],
			mappedProperties: [],
			index: n
		}, this
	}, ja.css = function () {
		var e = arguments;
		if (1 === e.length)
			for (var t = e[0], n = 0; n < this.properties.length; n++) {
				var r = this.properties[n],
					i = t[r.name];
				void 0 === i && (i = t[T(r.name)]), void 0 !== i && this.cssRule(r.name, i)
			} else 2 === e.length && this.cssRule(e[0], e[1]);
		return this
	}, ja.style = ja.css, ja.cssRule = function (e, t) {
		var n = this.parse(e, t);
		if (n) {
			var r = this.length - 1;
			this[r].properties.push(n), this[r].properties[n.name] = n, n.name.match(/pie-(\d+)-background-size/) && n.value && (this._private.hasPie = !0), n.mapped && this[r].mappedProperties.push(n), !this[r].selector && (this._private.coreStyle[n.name] = n)
		}
		return this
	}, ja.append = function (e) {
		return E(e) ? e.appendToStyle(this) : f(e) ? this.appendFromJson(e) : d(e) && this.appendFromString(e), this
	}, Ya.fromJson = function (e, t) {
		var n = new Ya(e);
		return n.fromJson(t), n
	}, Ya.fromString = function (e, t) {
		return new Ya(e).fromString(t)
	}, [za, La, Aa, Oa, Ra, Va, Fa, qa].forEach(function (e) {
		I(ja, e)
	}), Ya.types = ja.types, Ya.properties = ja.properties, Ya.propertyGroups = ja.propertyGroups, Ya.propertyGroupNames = ja.propertyGroupNames, Ya.propertyGroupKeys = ja.propertyGroupKeys;
	var Xa = {
		style: function (e) {
			e && this.setStyle(e).update();
			return this._private.style
		},
		setStyle: function (e) {
			var t = this._private;
			return E(e) ? t.style = e.generateStyle(this) : f(e) ? t.style = Ya.fromJson(this, e) : d(e) ? t.style = Ya.fromString(this, e) : t.style = Ya(this), t.style
		}
	},
		Wa = {
			autolock: function (e) {
				return void 0 === e ? this._private.autolock : (this._private.autolock = !!e, this)
			},
			autoungrabify: function (e) {
				return void 0 === e ? this._private.autoungrabify : (this._private.autoungrabify = !!e, this)
			},
			autounselectify: function (e) {
				return void 0 === e ? this._private.autounselectify : (this._private.autounselectify = !!e, this)
			},
			selectionType: function (e) {
				var t = this._private;
				return null == t.selectionType && (t.selectionType = "single"), void 0 === e ? t.selectionType : ("additive" !== e && "single" !== e || (t.selectionType = e), this)
			},
			panningEnabled: function (e) {
				return void 0 === e ? this._private.panningEnabled : (this._private.panningEnabled = !!e, this)
			},
			userPanningEnabled: function (e) {
				return void 0 === e ? this._private.userPanningEnabled : (this._private.userPanningEnabled = !!e, this)
			},
			zoomingEnabled: function (e) {
				return void 0 === e ? this._private.zoomingEnabled : (this._private.zoomingEnabled = !!e, this)
			},
			userZoomingEnabled: function (e) {
				return void 0 === e ? this._private.userZoomingEnabled : (this._private.userZoomingEnabled = !!e, this)
			},
			boxSelectionEnabled: function (e) {
				return void 0 === e ? this._private.boxSelectionEnabled : (this._private.boxSelectionEnabled = !!e, this)
			},
			pan: function () {
				var e, t, n, r, i, a = arguments,
					o = this._private.pan;
				switch (a.length) {
					case 0:
						return o;
					case 1:
						if (d(a[0])) return o[e = a[0]];
						if (g(a[0])) {
							if (!this._private.panningEnabled) return this;
							r = (n = a[0]).x, i = n.y, v(r) && (o.x = r), v(i) && (o.y = i), this.emit("pan viewport")
						}
						break;
					case 2:
						if (!this._private.panningEnabled) return this;
						e = a[0], t = a[1], "x" !== e && "y" !== e || !v(t) || (o[e] = t), this.emit("pan viewport")
				}
				return this.notify("viewport"), this
			},
			panBy: function (e, t) {
				var n, r, i, a, o, s = arguments,
					l = this._private.pan;
				if (!this._private.panningEnabled) return this;
				switch (s.length) {
					case 1:
						g(e) && (a = (i = s[0]).x, o = i.y, v(a) && (l.x += a), v(o) && (l.y += o), this.emit("pan viewport"));
						break;
					case 2:
						r = t, "x" !== (n = e) && "y" !== n || !v(r) || (l[n] += r), this.emit("pan viewport")
				}
				return this.notify("viewport"), this
			},
			fit: function (e, t) {
				var n = this.getFitViewport(e, t);
				if (n) {
					var r = this._private;
					r.zoom = n.zoom, r.pan = n.pan, this.emit("pan zoom viewport"), this.notify("viewport")
				}
				return this
			},
			getFitViewport: function (e, t) {
				if (v(e) && void 0 === t && (t = e, e = void 0), this._private.panningEnabled && this._private.zoomingEnabled) {
					var n, r;
					if (d(e)) {
						var i = e;
						e = this.$(i)
					} else if (g(r = e) && v(r.x1) && v(r.x2) && v(r.y1) && v(r.y2)) {
						var a = e;
						(n = {
							x1: a.x1,
							y1: a.y1,
							x2: a.x2,
							y2: a.y2
						}).w = n.x2 - n.x1, n.h = n.y2 - n.y1
					} else m(e) || (e = this.mutableElements()); if (!m(e) || !e.empty()) {
						n = n || e.boundingBox();
						var o, s = this.width(),
							l = this.height();
						if (t = v(t) ? t : 0, !isNaN(s) && !isNaN(l) && s > 0 && l > 0 && !isNaN(n.w) && !isNaN(n.h) && n.w > 0 && n.h > 0) return {
							zoom: o = (o = (o = Math.min((s - 2 * t) / n.w, (l - 2 * t) / n.h)) > this._private.maxZoom ? this._private.maxZoom : o) < this._private.minZoom ? this._private.minZoom : o,
							pan: {
								x: (s - o * (n.x1 + n.x2)) / 2,
								y: (l - o * (n.y1 + n.y2)) / 2
							}
						}
					}
				}
			},
			zoomRange: function (e, t) {
				var n = this._private;
				if (null == t) {
					var r = e;
					e = r.min, t = r.max
				}
				return v(e) && v(t) && e <= t ? (n.minZoom = e, n.maxZoom = t) : v(e) && void 0 === t && e <= n.maxZoom ? n.minZoom = e : v(t) && void 0 === e && t >= n.minZoom && (n.maxZoom = t), this
			},
			minZoom: function (e) {
				return void 0 === e ? this._private.minZoom : this.zoomRange({
					min: e
				})
			},
			maxZoom: function (e) {
				return void 0 === e ? this._private.maxZoom : this.zoomRange({
					max: e
				})
			},
			getZoomedViewport: function (e) {
				var t, n, r = this._private,
					i = r.pan,
					a = r.zoom,
					o = !1;
				if (r.zoomingEnabled || (o = !0), v(e) ? n = e : g(e) && (n = e.level, null != e.position ? t = tt(e.position, a, i) : null != e.renderedPosition && (t = e.renderedPosition), null == t || r.panningEnabled || (o = !0)), n = (n = n > r.maxZoom ? r.maxZoom : n) < r.minZoom ? r.minZoom : n, o || !v(n) || n === a || null != t && (!v(t.x) || !v(t.y))) return null;
				if (null != t) {
					var s = i,
						l = a,
						u = n;
					return {
						zoomed: !0,
						panned: !0,
						zoom: u,
						pan: {
							x: -u / l * (t.x - s.x) + t.x,
							y: -u / l * (t.y - s.y) + t.y
						}
					}
				}
				return {
					zoomed: !0,
					panned: !1,
					zoom: n,
					pan: i
				}
			},
			zoom: function (e) {
				if (void 0 === e) return this._private.zoom;
				var t = this.getZoomedViewport(e),
					n = this._private;
				return null != t && t.zoomed ? (n.zoom = t.zoom, t.panned && (n.pan.x = t.pan.x, n.pan.y = t.pan.y), this.emit("zoom" + (t.panned ? " pan" : "") + " viewport"), this.notify("viewport"), this) : this
			},
			viewport: function (e) {
				var t = this._private,
					n = !0,
					r = !0,
					i = [],
					a = !1,
					o = !1;
				if (!e) return this;
				if (v(e.zoom) || (n = !1), g(e.pan) || (r = !1), !n && !r) return this;
				if (n) {
					var s = e.zoom;
					s < t.minZoom || s > t.maxZoom || !t.zoomingEnabled ? a = !0 : (t.zoom = s, i.push("zoom"))
				}
				if (r && (!a || !e.cancelOnFailedZoom) && t.panningEnabled) {
					var l = e.pan;
					v(l.x) && (t.pan.x = l.x, o = !1), v(l.y) && (t.pan.y = l.y, o = !1), o || i.push("pan")
				}
				return i.length > 0 && (i.push("viewport"), this.emit(i.join(" ")), this.notify("viewport")), this
			},
			center: function (e) {
				var t = this.getCenterPan(e);
				return t && (this._private.pan = t, this.emit("pan viewport"), this.notify("viewport")), this
			},
			getCenterPan: function (e, t) {
				if (this._private.panningEnabled) {
					if (d(e)) {
						var n = e;
						e = this.mutableElements().filter(n)
					} else m(e) || (e = this.mutableElements()); if (0 !== e.length) {
						var r = e.boundingBox(),
							i = this.width(),
							a = this.height();
						return {
							x: (i - (t = void 0 === t ? this._private.zoom : t) * (r.x1 + r.x2)) / 2,
							y: (a - t * (r.y1 + r.y2)) / 2
						}
					}
				}
			},
			reset: function () {
				return this._private.panningEnabled && this._private.zoomingEnabled ? (this.viewport({
					pan: {
						x: 0,
						y: 0
					},
					zoom: 1
				}), this) : this
			},
			invalidateSize: function () {
				this._private.sizeCache = null
			},
			size: function () {
				var e, t, n = this._private,
					r = n.container;
				return n.sizeCache = n.sizeCache || (r ? (e = a.getComputedStyle(r), t = function (t) {
					return parseFloat(e.getPropertyValue(t))
				}, {
						width: r.clientWidth - t("padding-left") - t("padding-right"),
						height: r.clientHeight - t("padding-top") - t("padding-bottom")
					}) : {
						width: 1,
						height: 1
					})
			},
			width: function () {
				return this.size().width
			},
			height: function () {
				return this.size().height
			},
			extent: function () {
				var e = this._private.pan,
					t = this._private.zoom,
					n = this.renderedExtent(),
					r = {
						x1: (n.x1 - e.x) / t,
						x2: (n.x2 - e.x) / t,
						y1: (n.y1 - e.y) / t,
						y2: (n.y2 - e.y) / t
					};
				return r.w = r.x2 - r.x1, r.h = r.y2 - r.y1, r
			},
			renderedExtent: function () {
				var e = this.width(),
					t = this.height();
				return {
					x1: 0,
					y1: 0,
					x2: e,
					y2: t,
					w: e,
					h: t
				}
			}
		};
	Wa.centre = Wa.center, Wa.autolockNodes = Wa.autolock, Wa.autoungrabifyNodes = Wa.autoungrabify;
	var Ha = {
		data: Un.data({
			field: "data",
			bindingEvent: "data",
			allowBinding: !0,
			allowSetting: !0,
			settingEvent: "data",
			settingTriggersEvent: !0,
			triggerFnName: "trigger",
			allowGetting: !0
		}),
		removeData: Un.removeData({
			field: "data",
			event: "data",
			triggerFnName: "trigger",
			triggerEvent: !0
		}),
		scratch: Un.data({
			field: "scratch",
			bindingEvent: "scratch",
			allowBinding: !0,
			allowSetting: !0,
			settingEvent: "scratch",
			settingTriggersEvent: !0,
			triggerFnName: "trigger",
			allowGetting: !0
		}),
		removeScratch: Un.removeData({
			field: "scratch",
			event: "scratch",
			triggerFnName: "trigger",
			triggerEvent: !0
		})
	};
	Ha.attr = Ha.data, Ha.removeAttr = Ha.removeData;
	var Ka = function (e) {
		var t = this,
			n = (e = I({}, e)).container;
		n && !y(n) && y(n[0]) && (n = n[0]);
		var r = n ? n._cyreg : null;
		(r = r || {}) && r.cy && (r.cy.destroy(), r = {});
		var i = r.readies = r.readies || [];
		n && (n._cyreg = r), r.cy = t;
		var o = void 0 !== a && void 0 !== n && !e.headless,
			s = e;
		s.layout = I({
			name: o ? "grid" : "null"
		}, s.layout), s.renderer = I({
			name: o ? "canvas" : "null"
		}, s.renderer);
		var l = function (e, t, n) {
			return void 0 !== t ? t : void 0 !== n ? n : e
		},
			u = this._private = {
				container: n,
				ready: !1,
				options: s,
				elements: new ca(this),
				listeners: [],
				aniEles: new ca(this),
				data: {},
				scratch: {},
				layout: null,
				renderer: null,
				destroyed: !1,
				notificationsEnabled: !0,
				minZoom: 1e-50,
				maxZoom: 1e50,
				zoomingEnabled: l(!0, s.zoomingEnabled),
				userZoomingEnabled: l(!0, s.userZoomingEnabled),
				panningEnabled: l(!0, s.panningEnabled),
				userPanningEnabled: l(!0, s.userPanningEnabled),
				boxSelectionEnabled: l(!0, s.boxSelectionEnabled),
				autolock: l(!1, s.autolock, s.autolockNodes),
				autoungrabify: l(!1, s.autoungrabify, s.autoungrabifyNodes),
				autounselectify: l(!1, s.autounselectify),
				styleEnabled: void 0 === s.styleEnabled ? o : s.styleEnabled,
				zoom: v(s.zoom) ? s.zoom : 1,
				pan: {
					x: g(s.pan) && v(s.pan.x) ? s.pan.x : 0,
					y: g(s.pan) && v(s.pan.y) ? s.pan.y : 0
				},
				animation: {
					current: [],
					queue: []
				},
				hasCompoundNodes: !1
			};
		this.createEmitter(), this.selectionType(s.selectionType), this.zoomRange({
			min: s.minZoom,
			max: s.maxZoom
		});
		u.styleEnabled && t.setStyle([]);
		var c = I({}, s, s.renderer);
		t.initRenderer(c);
		! function (e, t) {
			if (e.some(C)) return Kn.all(e).then(t);
			t(e)
		}([s.style, s.elements], function (e) {
			var n = e[0],
				a = e[1];
			u.styleEnabled && t.style().append(n),
				function (e, n, r) {
					t.notifications(!1);
					var i = t.mutableElements();
					i.length > 0 && i.remove(), null != e && (g(e) || f(e)) && t.add(e), t.one("layoutready", function (e) {
						t.notifications(!0), t.emit(e), t.one("load", n), t.emitAndNotify("load")
					}).one("layoutstop", function () {
						t.one("done", r), t.emit("done")
					});
					var a = I({}, t._private.options.layout);
					a.eles = t.elements(), t.layout(a).run()
				}(a, function () {
					t.startAnimationLoop(), u.ready = !0, p(s.ready) && t.on("ready", s.ready);
					for (var e = 0; e < i.length; e++) {
						var n = i[e];
						t.on("ready", n)
					}
					r && (r.readies = []), t.emit("ready")
				}, s.done)
		})
	},
		Ga = Ka.prototype;
	I(Ga, {
		instanceString: function () {
			return "core"
		},
		isReady: function () {
			return this._private.ready
		},
		destroyed: function () {
			return this._private.destroyed
		},
		ready: function (e) {
			return this.isReady() ? this.emitter().emit("ready", [], e) : this.on("ready", e), this
		},
		destroy: function () {
			var e = this;
			if (!e.destroyed()) return e.stopAnimationLoop(), e.destroyRenderer(), this.emit("destroy"), e._private.destroyed = !0, e
		},
		hasElementWithId: function (e) {
			return this._private.elements.hasElementWithId(e)
		},
		getElementById: function (e) {
			return this._private.elements.getElementById(e)
		},
		hasCompoundNodes: function () {
			return this._private.hasCompoundNodes
		},
		headless: function () {
			return this._private.renderer.isHeadless()
		},
		styleEnabled: function () {
			return this._private.styleEnabled
		},
		addToPool: function (e) {
			return this._private.elements.merge(e), this
		},
		removeFromPool: function (e) {
			return this._private.elements.unmerge(e), this
		},
		container: function () {
			return this._private.container || null
		},
		mount: function (e) {
			if (null != e) {
				var t = this,
					n = t._private,
					r = n.options;
				return !y(e) && y(e[0]) && (e = e[0]), t.stopAnimationLoop(), t.destroyRenderer(), n.container = e, n.styleEnabled = !0, t.invalidateSize(), t.initRenderer(I({}, r, r.renderer, {
					name: "null" === r.renderer.name ? "canvas" : r.renderer.name
				})), t.startAnimationLoop(), t.style(r.style), t.emit("mount"), t
			}
		},
		unmount: function () {
			var e = this;
			return e.stopAnimationLoop(), e.destroyRenderer(), e.initRenderer({
				name: "null"
			}), e.emit("unmount"), e
		},
		options: function () {
			return Se(this._private.options)
		},
		json: function (e) {
			var t = this,
				n = t._private,
				r = t.mutableElements();
			if (g(e)) {
				if (t.startBatch(), e.elements) {
					var i = {},
						a = function (e, n) {
							for (var r = [], a = [], o = 0; o < e.length; o++) {
								var s = e[o],
									l = "" + s.data.id,
									u = t.getElementById(l);
								i[l] = !0, 0 !== u.length ? a.push({
									ele: u,
									json: s
								}) : n ? (s.group = n, r.push(s)) : r.push(s)
							}
							t.add(r);
							for (var c = 0; c < a.length; c++) {
								var h = a[c],
									d = h.ele,
									p = h.json;
								d.json(p)
							}
						};
					if (f(e.elements)) a(e.elements);
					else
						for (var o = ["nodes", "edges"], s = 0; s < o.length; s++) {
							var l = o[s],
								u = e.elements[l];
							f(u) && a(u, l)
						}
					var c = t.collection();
					r.filter(function (e) {
						return !i[e.id()]
					}).forEach(function (e) {
						e.isParent() ? c.merge(e) : e.remove()
					}), c.forEach(function (e) {
						return e.children().move({
							parent: null
						})
					}), c.forEach(function (e) {
						return function (e) {
							return t.getElementById(e.id())
						}(e).remove()
					})
				}
				e.style && t.style(e.style), null != e.zoom && e.zoom !== n.zoom && t.zoom(e.zoom), e.pan && (e.pan.x === n.pan.x && e.pan.y === n.pan.y || t.pan(e.pan)), e.data && t.data(e.data);
				for (var h = ["minZoom", "maxZoom", "zoomingEnabled", "userZoomingEnabled", "panningEnabled", "userPanningEnabled", "boxSelectionEnabled", "autolock", "autoungrabify", "autounselectify"], d = 0; d < h.length; d++) {
					var p = h[d];
					null != e[p] && t[p](e[p])
				}
				return t.endBatch(), this
			}
			var v = {};
			!!e ? v.elements = this.elements().map(function (e) {
				return e.json()
			}) : (v.elements = {}, r.forEach(function (e) {
				var t = e.group();
				v.elements[t] || (v.elements[t] = []), v.elements[t].push(e.json())
			})), this._private.styleEnabled && (v.style = t.style().json()), v.data = Se(t.data());
			var y = n.options;
			return v.zoomingEnabled = n.zoomingEnabled, v.userZoomingEnabled = n.userZoomingEnabled, v.zoom = n.zoom, v.minZoom = n.minZoom, v.maxZoom = n.maxZoom, v.panningEnabled = n.panningEnabled, v.userPanningEnabled = n.userPanningEnabled, v.pan = Se(n.pan), v.boxSelectionEnabled = n.boxSelectionEnabled, v.renderer = Se(y.renderer), v.hideEdgesOnViewport = y.hideEdgesOnViewport, v.textureOnViewport = y.textureOnViewport, v.wheelSensitivity = y.wheelSensitivity, v.motionBlur = y.motionBlur, v
		}
	}), Ga.$id = Ga.getElementById, [da, Ca, Pa, Ta, Ma, Ba, Na, Ia, Xa, Wa, Ha].forEach(function (e) {
		I(Ga, e)
	});
	var Za = {
		fit: !0,
		directed: !1,
		padding: 30,
		circle: !1,
		grid: !1,
		spacingFactor: 1.75,
		boundingBox: void 0,
		avoidOverlap: !0,
		nodeDimensionsIncludeLabels: !1,
		roots: void 0,
		maximal: !1,
		animate: !1,
		animationDuration: 500,
		animationEasing: void 0,
		animateFilter: function (e, t) {
			return !0
		},
		ready: void 0,
		stop: void 0,
		transform: function (e, t) {
			return t
		}
	},
		Ua = function (e) {
			return e.scratch("breadthfirst")
		},
		$a = function (e, t) {
			return e.scratch("breadthfirst", t)
		};

	function Qa(e) {
		this.options = I({}, Za, e)
	}
	Qa.prototype.run = function () {
		var e, t = this.options,
			n = t,
			r = t.cy,
			i = n.eles,
			a = i.nodes().filter(function (e) {
				return !e.isParent()
			}),
			o = i,
			s = n.directed,
			l = n.maximal || n.maximalAdjustments > 0,
			u = pt(n.boundingBox ? n.boundingBox : {
				x1: 0,
				y1: 0,
				w: r.width(),
				h: r.height()
			});
		if (m(n.roots)) e = n.roots;
		else if (f(n.roots)) {
			for (var c = [], h = 0; h < n.roots.length; h++) {
				var p = n.roots[h],
					g = r.getElementById(p);
				c.push(g)
			}
			e = r.collection(c)
		} else if (d(n.roots)) e = r.$(n.roots);
		else if (s) e = a.roots();
		else {
			var v = i.components();
			e = r.collection();
			for (var y = function (t) {
				var n = v[t],
					r = n.maxDegree(!1),
					i = n.filter(function (e) {
						return e.degree(!1) === r
					});
				e = e.add(i)
			}, b = 0; b < v.length; b++) y(b)
		}
		var x = [],
			w = {},
			E = function (e, t) {
				null == x[t] && (x[t] = []);
				var n = x[t].length;
				x[t].push(e), $a(e, {
					index: n,
					depth: t
				})
			};
		o.bfs({
			roots: e,
			directed: n.directed,
			visit: function (e, t, n, r, i) {
				var a = e[0],
					o = a.id();
				E(a, i), w[o] = !0
			}
		});
		for (var k = [], C = 0; C < a.length; C++) {
			var S = a[C];
			w[S.id()] || k.push(S)
		}
		var D = function (e) {
			for (var t = x[e], n = 0; n < t.length; n++) {
				var r = t[n];
				null != r ? $a(r, {
					depth: e,
					index: n
				}) : (t.splice(n, 1), n--)
			}
		},
			P = function () {
				for (var e = 0; e < x.length; e++) D(e)
			},
			T = function (e, t) {
				for (var n = Ua(e), r = e.incomers().filter(function (e) {
					return e.isNode() && i.has(e)
				}), a = -1, o = e.id(), s = 0; s < r.length; s++) {
					var l = r[s],
						u = Ua(l);
					a = Math.max(a, u.depth)
				}
				return n.depth <= a && (t[o] ? null : (function (e, t) {
					var n = Ua(e),
						r = n.depth,
						i = n.index;
					x[r][i] = null, E(e, t)
				}(e, a + 1), t[o] = !0, !0))
			};
		if (s && l) {
			var M = [],
				B = {},
				_ = function (e) {
					return M.push(e)
				};
			for (a.forEach(function (e) {
				return M.push(e)
			}); M.length > 0;) {
				var I = M.shift(),
					z = T(I, B);
				if (z) I.outgoers().filter(function (e) {
					return e.isNode() && i.has(e)
				}).forEach(_);
				else if (null === z) {
					Ce("Detected double maximal shift for node `" + I.id() + "`.  Bailing maximal adjustment due to cycle.  Use `options.maximal: true` only on DAGs.");
					break
				}
			}
		}
		P();
		var L = 0;
		if (n.avoidOverlap)
			for (var A = 0; A < a.length; A++) {
				var O = a[A].layoutDimensions(n),
					R = O.w,
					V = O.h;
				L = Math.max(L, R, V)
			}
		for (var F = {}, q = function (e) {
			if (F[e.id()]) return F[e.id()];
			for (var t = Ua(e).depth, n = e.neighborhood(), r = 0, i = 0, o = 0; o < n.length; o++) {
				var s = n[o];
				if (!s.isEdge() && !s.isParent() && a.has(s)) {
					var l = Ua(s),
						u = l.index,
						c = l.depth;
					if (null != u && null != c) {
						var h = x[c].length;
						c < t && (r += u / (h - 1), i++)
					}
				}
			}
			return r /= i = Math.max(1, i), 0 === i && (r = 0), F[e.id()] = r, r
		}, Y = function (e, t) {
			var n = q(e) - q(t);
			return 0 === n ? N(e.id(), t.id()) : n
		}, j = 0; j < x.length; j++) x[j].sort(Y), D(j);
		for (var X = [], W = 0; W < k.length; W++) X.push(k[W]);
		x.unshift(X), P();
		for (var H = 0, K = 0; K < x.length; K++) H = Math.max(x[K].length, H);
		var G = u.x1 + u.w / 2,
			Z = u.x1 + u.h / 2,
			U = x.reduce(function (e, t) {
				return Math.max(e, t.length)
			}, 0);
		return a.layoutPositions(this, n, function (e) {
			var t = Ua(e),
				r = t.depth,
				i = t.index,
				a = x[r].length,
				o = Math.max(u.w / ((n.grid ? U : a) + 1), L),
				s = Math.max(u.h / (x.length + 1), L),
				l = Math.min(u.w / 2 / x.length, u.h / 2 / x.length);
			if (l = Math.max(l, L), n.circle) {
				var c = l * r + l - (x.length > 0 && x[0].length <= 3 ? l / 2 : 0),
					h = 2 * Math.PI / x[r].length * i;
				return 0 === r && 1 === x[0].length && (c = 1), {
					x: G + c * Math.cos(h),
					y: Z + c * Math.sin(h)
				}
			}
			return {
				x: G + (i + 1 - (a + 1) / 2) * o,
				y: (r + 1) * s
			}
		}), this
	};
	var Ja = {
		fit: !0,
		padding: 30,
		boundingBox: void 0,
		avoidOverlap: !0,
		nodeDimensionsIncludeLabels: !1,
		spacingFactor: void 0,
		radius: void 0,
		startAngle: 1.5 * Math.PI,
		sweep: void 0,
		clockwise: !0,
		sort: void 0,
		animate: !1,
		animationDuration: 500,
		animationEasing: void 0,
		animateFilter: function (e, t) {
			return !0
		},
		ready: void 0,
		stop: void 0,
		transform: function (e, t) {
			return t
		}
	};

	function eo(e) {
		this.options = I({}, Ja, e)
	}
	eo.prototype.run = function () {
		var e = this.options,
			t = e,
			n = e.cy,
			r = t.eles,
			i = void 0 !== t.counterclockwise ? !t.counterclockwise : t.clockwise,
			a = r.nodes().not(":parent");
		t.sort && (a = a.sort(t.sort));
		for (var o, s = pt(t.boundingBox ? t.boundingBox : {
			x1: 0,
			y1: 0,
			w: n.width(),
			h: n.height()
		}), l = s.x1 + s.w / 2, u = s.y1 + s.h / 2, c = (void 0 === t.sweep ? 2 * Math.PI - 2 * Math.PI / a.length : t.sweep) / Math.max(1, a.length - 1), h = 0, d = 0; d < a.length; d++) {
			var p = a[d].layoutDimensions(t),
				f = p.w,
				g = p.h;
			h = Math.max(h, f, g)
		}
		if (o = v(t.radius) ? t.radius : a.length <= 1 ? 0 : Math.min(s.h, s.w) / 2 - h, a.length > 1 && t.avoidOverlap) {
			h *= 1.75;
			var y = Math.cos(c) - Math.cos(0),
				m = Math.sin(c) - Math.sin(0),
				b = Math.sqrt(h * h / (y * y + m * m));
			o = Math.max(b, o)
		}
		return a.layoutPositions(this, t, function (e, n) {
			var r = t.startAngle + n * c * (i ? 1 : -1),
				a = o * Math.cos(r),
				s = o * Math.sin(r);
			return {
				x: l + a,
				y: u + s
			}
		}), this
	};
	var to, no = {
		fit: !0,
		padding: 30,
		startAngle: 1.5 * Math.PI,
		sweep: void 0,
		clockwise: !0,
		equidistant: !1,
		minNodeSpacing: 10,
		boundingBox: void 0,
		avoidOverlap: !0,
		nodeDimensionsIncludeLabels: !1,
		height: void 0,
		width: void 0,
		spacingFactor: void 0,
		concentric: function (e) {
			return e.degree()
		},
		levelWidth: function (e) {
			return e.maxDegree() / 4
		},
		animate: !1,
		animationDuration: 500,
		animationEasing: void 0,
		animateFilter: function (e, t) {
			return !0
		},
		ready: void 0,
		stop: void 0,
		transform: function (e, t) {
			return t
		}
	};

	function ro(e) {
		this.options = I({}, no, e)
	}
	ro.prototype.run = function () {
		for (var e = this.options, t = e, n = void 0 !== t.counterclockwise ? !t.counterclockwise : t.clockwise, r = e.cy, i = t.eles.nodes().not(":parent"), a = pt(t.boundingBox ? t.boundingBox : {
			x1: 0,
			y1: 0,
			w: r.width(),
			h: r.height()
		}), o = a.x1 + a.w / 2, s = a.y1 + a.h / 2, l = [], u = 0, c = 0; c < i.length; c++) {
			var h, d = i[c];
			h = t.concentric(d), l.push({
				value: h,
				node: d
			}), d._private.scratch.concentric = h
		}
		i.updateStyle();
		for (var p = 0; p < i.length; p++) {
			var f = i[p].layoutDimensions(t);
			u = Math.max(u, f.w, f.h)
		}
		l.sort(function (e, t) {
			return t.value - e.value
		});
		for (var g = t.levelWidth(i), v = [
			[]
		], y = v[0], m = 0; m < l.length; m++) {
			var b = l[m];
			if (y.length > 0) Math.abs(y[0].value - b.value) >= g && (y = [], v.push(y));
			y.push(b)
		}
		var x = u + t.minNodeSpacing;
		if (!t.avoidOverlap) {
			var w = v.length > 0 && v[0].length > 1,
				E = (Math.min(a.w, a.h) / 2 - x) / (v.length + w ? 1 : 0);
			x = Math.min(x, E)
		}
		for (var k = 0, C = 0; C < v.length; C++) {
			var S = v[C],
				D = void 0 === t.sweep ? 2 * Math.PI - 2 * Math.PI / S.length : t.sweep,
				P = S.dTheta = D / Math.max(1, S.length - 1);
			if (S.length > 1 && t.avoidOverlap) {
				var T = Math.cos(P) - Math.cos(0),
					M = Math.sin(P) - Math.sin(0),
					B = Math.sqrt(x * x / (T * T + M * M));
				k = Math.max(B, k)
			}
			S.r = k, k += x
		}
		if (t.equidistant) {
			for (var _ = 0, N = 0, I = 0; I < v.length; I++) {
				var z = v[I].r - N;
				_ = Math.max(_, z)
			}
			N = 0;
			for (var L = 0; L < v.length; L++) {
				var A = v[L];
				0 === L && (N = A.r), A.r = N, N += _
			}
		}
		for (var O = {}, R = 0; R < v.length; R++)
			for (var V = v[R], F = V.dTheta, q = V.r, Y = 0; Y < V.length; Y++) {
				var j = V[Y],
					X = t.startAngle + (n ? 1 : -1) * F * Y,
					W = {
						x: o + q * Math.cos(X),
						y: s + q * Math.sin(X)
					};
				O[j.node.id()] = W
			}
		return i.layoutPositions(this, t, function (e) {
			var t = e.id();
			return O[t]
		}), this
	};
	var io = {
		ready: function () { },
		stop: function () { },
		animate: !0,
		animationEasing: void 0,
		animationDuration: void 0,
		animateFilter: function (e, t) {
			return !0
		},
		animationThreshold: 250,
		refresh: 20,
		fit: !0,
		padding: 30,
		boundingBox: void 0,
		nodeDimensionsIncludeLabels: !1,
		randomize: !1,
		componentSpacing: 40,
		nodeRepulsion: function (e) {
			return 2048
		},
		nodeOverlap: 4,
		idealEdgeLength: function (e) {
			return 32
		},
		edgeElasticity: function (e) {
			return 32
		},
		nestingFactor: 1.2,
		gravity: 1,
		numIter: 1e3,
		initialTemp: 1e3,
		coolingFactor: .99,
		minTemp: 1
	};

	function ao(e) {
		this.options = I({}, io, e), this.options.layout = this
	}
	ao.prototype.run = function () {
		var e = this.options,
			t = e.cy,
			n = this;
		n.stopped = !1, !0 !== e.animate && !1 !== e.animate || n.emit({
			type: "layoutstart",
			layout: n
		}), to = !0 === e.debug;
		var r = oo(t, n, e);
		to && (void 0)(r), e.randomize && uo(r);
		var i = se(),
			a = function () {
				ho(r, t, e), !0 === e.fit && t.fit(e.padding)
			},
			o = function (t) {
				return !(n.stopped || t >= e.numIter) && (po(r, e), r.temperature = r.temperature * e.coolingFactor, !(r.temperature < e.minTemp))
			},
			s = function () {
				if (!0 === e.animate || !1 === e.animate) a(), n.one("layoutstop", e.stop), n.emit({
					type: "layoutstop",
					layout: n
				});
				else {
					var t = e.eles.nodes(),
						i = co(r, e, t);
					t.layoutPositions(n, e, i)
				}
			},
			l = 0,
			u = !0;
		if (!0 === e.animate) {
			! function t() {
				for (var n = 0; u && n < e.refresh;) u = o(l), l++ , n++;
				u ? (se() - i >= e.animationThreshold && a(), oe(t)) : (So(r, e), s())
			}()
		} else {
			for (; u;) u = o(l), l++;
			So(r, e), s()
		}
		return this
	}, ao.prototype.stop = function () {
		return this.stopped = !0, this.thread && this.thread.stop(), this.emit("layoutstop"), this
	}, ao.prototype.destroy = function () {
		return this.thread && this.thread.stop(), this
	};
	var oo = function (e, t, n) {
		for (var r = n.eles.edges(), i = n.eles.nodes(), a = {
			isCompound: e.hasCompoundNodes(),
			layoutNodes: [],
			idToIndex: {},
			nodeSize: i.size(),
			graphSet: [],
			indexToGraph: [],
			layoutEdges: [],
			edgeSize: r.size(),
			temperature: n.initialTemp,
			clientWidth: e.width(),
			clientHeight: e.width(),
			boundingBox: pt(n.boundingBox ? n.boundingBox : {
				x1: 0,
				y1: 0,
				w: e.width(),
				h: e.height()
			})
		}, o = n.eles.components(), s = {}, l = 0; l < o.length; l++)
			for (var u = o[l], c = 0; c < u.length; c++) {
				s[u[c].id()] = l
			}
		for (l = 0; l < a.nodeSize; l++) {
			var h = (y = i[l]).layoutDimensions(n);
			(I = {}).isLocked = y.locked(), I.id = y.data("id"), I.parentId = y.data("parent"), I.cmptId = s[y.id()], I.children = [], I.positionX = y.position("x"), I.positionY = y.position("y"), I.offsetX = 0, I.offsetY = 0, I.height = h.w, I.width = h.h, I.maxX = I.positionX + I.width / 2, I.minX = I.positionX - I.width / 2, I.maxY = I.positionY + I.height / 2, I.minY = I.positionY - I.height / 2, I.padLeft = parseFloat(y.style("padding")), I.padRight = parseFloat(y.style("padding")), I.padTop = parseFloat(y.style("padding")), I.padBottom = parseFloat(y.style("padding")), I.nodeRepulsion = p(n.nodeRepulsion) ? n.nodeRepulsion(y) : n.nodeRepulsion, a.layoutNodes.push(I), a.idToIndex[I.id] = l
		}
		var d = [],
			f = 0,
			g = -1,
			v = [];
		for (l = 0; l < a.nodeSize; l++) {
			var y, m = (y = a.layoutNodes[l]).parentId;
			null != m ? a.layoutNodes[a.idToIndex[m]].children.push(y.id) : (d[++g] = y.id, v.push(y.id))
		}
		for (a.graphSet.push(v); f <= g;) {
			var b = d[f++],
				x = a.idToIndex[b],
				w = a.layoutNodes[x].children;
			if (w.length > 0) {
				a.graphSet.push(w);
				for (l = 0; l < w.length; l++) d[++g] = w[l]
			}
		}
		for (l = 0; l < a.graphSet.length; l++) {
			var E = a.graphSet[l];
			for (c = 0; c < E.length; c++) {
				var k = a.idToIndex[E[c]];
				a.indexToGraph[k] = l
			}
		}
		for (l = 0; l < a.edgeSize; l++) {
			var C = r[l],
				S = {};
			S.id = C.data("id"), S.sourceId = C.data("source"), S.targetId = C.data("target");
			var D = p(n.idealEdgeLength) ? n.idealEdgeLength(C) : n.idealEdgeLength,
				P = p(n.edgeElasticity) ? n.edgeElasticity(C) : n.edgeElasticity,
				T = a.idToIndex[S.sourceId],
				M = a.idToIndex[S.targetId];
			if (a.indexToGraph[T] != a.indexToGraph[M]) {
				for (var B = so(S.sourceId, S.targetId, a), _ = a.graphSet[B], N = 0, I = a.layoutNodes[T]; - 1 === _.indexOf(I.id);) I = a.layoutNodes[a.idToIndex[I.parentId]], N++;
				for (I = a.layoutNodes[M]; - 1 === _.indexOf(I.id);) I = a.layoutNodes[a.idToIndex[I.parentId]], N++;
				D *= N * n.nestingFactor
			}
			S.idealLength = D, S.elasticity = P, a.layoutEdges.push(S)
		}
		return a
	},
		so = function (e, t, n) {
			var r = lo(e, t, 0, n);
			return 2 > r.count ? 0 : r.graph
		},
		lo = function e(t, n, r, i) {
			var a = i.graphSet[r];
			if (-1 < a.indexOf(t) && -1 < a.indexOf(n)) return {
				count: 2,
				graph: r
			};
			for (var o = 0, s = 0; s < a.length; s++) {
				var l = a[s],
					u = i.idToIndex[l],
					c = i.layoutNodes[u].children;
				if (0 !== c.length) {
					var h = e(t, n, i.indexToGraph[i.idToIndex[c[0]]], i);
					if (0 !== h.count) {
						if (1 !== h.count) return h;
						if (2 === ++o) break
					}
				}
			}
			return {
				count: o,
				graph: r
			}
		},
		uo = function (e, t) {
			for (var n = e.clientWidth, r = e.clientHeight, i = 0; i < e.nodeSize; i++) {
				var a = e.layoutNodes[i];
				0 !== a.children.length || a.isLocked || (a.positionX = Math.random() * n, a.positionY = Math.random() * r)
			}
		},
		co = function (e, t, n) {
			var r = e.boundingBox,
				i = {
					x1: 1 / 0,
					x2: -1 / 0,
					y1: 1 / 0,
					y2: -1 / 0
				};
			return t.boundingBox && (n.forEach(function (t) {
				var n = e.layoutNodes[e.idToIndex[t.data("id")]];
				i.x1 = Math.min(i.x1, n.positionX), i.x2 = Math.max(i.x2, n.positionX), i.y1 = Math.min(i.y1, n.positionY), i.y2 = Math.max(i.y2, n.positionY)
			}), i.w = i.x2 - i.x1, i.h = i.y2 - i.y1),
				function (n, a) {
					var o = e.layoutNodes[e.idToIndex[n.data("id")]];
					if (t.boundingBox) {
						var s = (o.positionX - i.x1) / i.w,
							l = (o.positionY - i.y1) / i.h;
						return {
							x: r.x1 + s * r.w,
							y: r.y1 + l * r.h
						}
					}
					return {
						x: o.positionX,
						y: o.positionY
					}
				}
		},
		ho = function (e, t, n) {
			var r = n.layout,
				i = n.eles.nodes(),
				a = co(e, n, i);
			i.positions(a), !0 !== e.ready && (e.ready = !0, r.one("layoutready", n.ready), r.emit({
				type: "layoutready",
				layout: this
			}))
		},
		po = function (e, t, n) {
			fo(e, t), bo(e), xo(e, t), wo(e), Eo(e)
		},
		fo = function (e, t) {
			for (var n = 0; n < e.graphSet.length; n++)
				for (var r = e.graphSet[n], i = r.length, a = 0; a < i; a++)
					for (var o = e.layoutNodes[e.idToIndex[r[a]]], s = a + 1; s < i; s++) {
						var l = e.layoutNodes[e.idToIndex[r[s]]];
						vo(o, l, e, t)
					}
		},
		go = function (e) {
			return -e + 2 * e * Math.random()
		},
		vo = function (e, t, n, r) {
			if (e.cmptId === t.cmptId || n.isCompound) {
				var i = t.positionX - e.positionX,
					a = t.positionY - e.positionY;
				0 === i && 0 === a && (i = go(1), a = go(1));
				var o = yo(e, t, i, a);
				if (o > 0) var s = (u = r.nodeOverlap * o) * i / (g = Math.sqrt(i * i + a * a)),
					l = u * a / g;
				else {
					var u, c = mo(e, i, a),
						h = mo(t, -1 * i, -1 * a),
						d = h.x - c.x,
						p = h.y - c.y,
						f = d * d + p * p,
						g = Math.sqrt(f);
					s = (u = (e.nodeRepulsion + t.nodeRepulsion) / f) * d / g, l = u * p / g
				}
				e.isLocked || (e.offsetX -= s, e.offsetY -= l), t.isLocked || (t.offsetX += s, t.offsetY += l)
			}
		},
		yo = function (e, t, n, r) {
			if (n > 0) var i = e.maxX - t.minX;
			else i = t.maxX - e.minX; if (r > 0) var a = e.maxY - t.minY;
			else a = t.maxY - e.minY;
			return i >= 0 && a >= 0 ? Math.sqrt(i * i + a * a) : 0
		},
		mo = function (e, t, n) {
			var r = e.positionX,
				i = e.positionY,
				a = e.height || 1,
				o = e.width || 1,
				s = n / t,
				l = a / o,
				u = {};
			return 0 === t && 0 < n ? (u.x = r, u.y = i + a / 2, u) : 0 === t && 0 > n ? (u.x = r, u.y = i + a / 2, u) : 0 < t && -1 * l <= s && s <= l ? (u.x = r + o / 2, u.y = i + o * n / 2 / t, u) : 0 > t && -1 * l <= s && s <= l ? (u.x = r - o / 2, u.y = i - o * n / 2 / t, u) : 0 < n && (s <= -1 * l || s >= l) ? (u.x = r + a * t / 2 / n, u.y = i + a / 2, u) : 0 > n && (s <= -1 * l || s >= l) ? (u.x = r - a * t / 2 / n, u.y = i - a / 2, u) : u
		},
		bo = function (e, t) {
			for (var n = 0; n < e.edgeSize; n++) {
				var r = e.layoutEdges[n],
					i = e.idToIndex[r.sourceId],
					a = e.layoutNodes[i],
					o = e.idToIndex[r.targetId],
					s = e.layoutNodes[o],
					l = s.positionX - a.positionX,
					u = s.positionY - a.positionY;
				if (0 !== l || 0 !== u) {
					var c = mo(a, l, u),
						h = mo(s, -1 * l, -1 * u),
						d = h.x - c.x,
						p = h.y - c.y,
						f = Math.sqrt(d * d + p * p),
						g = Math.pow(r.idealLength - f, 2) / r.elasticity;
					if (0 !== f) var v = g * d / f,
						y = g * p / f;
					else v = 0, y = 0;
					a.isLocked || (a.offsetX += v, a.offsetY += y), s.isLocked || (s.offsetX -= v, s.offsetY -= y)
				}
			}
		},
		xo = function (e, t) {
			for (var n = 0; n < e.graphSet.length; n++) {
				var r = e.graphSet[n],
					i = r.length;
				if (0 === n) var a = e.clientHeight / 2,
					o = e.clientWidth / 2;
				else {
					var s = e.layoutNodes[e.idToIndex[r[0]]],
						l = e.layoutNodes[e.idToIndex[s.parentId]];
					a = l.positionX, o = l.positionY
				}
				for (var u = 0; u < i; u++) {
					var c = e.layoutNodes[e.idToIndex[r[u]]];
					if (!c.isLocked) {
						var h = a - c.positionX,
							d = o - c.positionY,
							p = Math.sqrt(h * h + d * d);
						if (p > 1) {
							var f = t.gravity * h / p,
								g = t.gravity * d / p;
							c.offsetX += f, c.offsetY += g
						}
					}
				}
			}
		},
		wo = function (e, t) {
			var n = [],
				r = 0,
				i = -1;
			for (n.push.apply(n, e.graphSet[0]), i += e.graphSet[0].length; r <= i;) {
				var a = n[r++],
					o = e.idToIndex[a],
					s = e.layoutNodes[o],
					l = s.children;
				if (0 < l.length && !s.isLocked) {
					for (var u = s.offsetX, c = s.offsetY, h = 0; h < l.length; h++) {
						var d = e.layoutNodes[e.idToIndex[l[h]]];
						d.offsetX += u, d.offsetY += c, n[++i] = l[h]
					}
					s.offsetX = 0, s.offsetY = 0
				}
			}
		},
		Eo = function (e, t) {
			for (var n = 0; n < e.nodeSize; n++) {
				0 < (i = e.layoutNodes[n]).children.length && (i.maxX = void 0, i.minX = void 0, i.maxY = void 0, i.minY = void 0)
			}
			for (n = 0; n < e.nodeSize; n++) {
				if (!(0 < (i = e.layoutNodes[n]).children.length || i.isLocked)) {
					var r = ko(i.offsetX, i.offsetY, e.temperature);
					i.positionX += r.x, i.positionY += r.y, i.offsetX = 0, i.offsetY = 0, i.minX = i.positionX - i.width, i.maxX = i.positionX + i.width, i.minY = i.positionY - i.height, i.maxY = i.positionY + i.height, Co(i, e)
				}
			}
			for (n = 0; n < e.nodeSize; n++) {
				var i;
				0 < (i = e.layoutNodes[n]).children.length && !i.isLocked && (i.positionX = (i.maxX + i.minX) / 2, i.positionY = (i.maxY + i.minY) / 2, i.width = i.maxX - i.minX, i.height = i.maxY - i.minY)
			}
		},
		ko = function (e, t, n) {
			var r = Math.sqrt(e * e + t * t);
			if (r > n) var i = {
				x: n * e / r,
				y: n * t / r
			};
			else i = {
				x: e,
				y: t
			};
			return i
		},
		Co = function e(t, n) {
			var r = t.parentId;
			if (null != r) {
				var i = n.layoutNodes[n.idToIndex[r]],
					a = !1;
				return (null == i.maxX || t.maxX + i.padRight > i.maxX) && (i.maxX = t.maxX + i.padRight, a = !0), (null == i.minX || t.minX - i.padLeft < i.minX) && (i.minX = t.minX - i.padLeft, a = !0), (null == i.maxY || t.maxY + i.padBottom > i.maxY) && (i.maxY = t.maxY + i.padBottom, a = !0), (null == i.minY || t.minY - i.padTop < i.minY) && (i.minY = t.minY - i.padTop, a = !0), a ? e(i, n) : void 0
			}
		},
		So = function (e, t) {
			for (var n = e.layoutNodes, r = [], i = 0; i < n.length; i++) {
				var a = n[i],
					o = a.cmptId;
				(r[o] = r[o] || []).push(a)
			}
			var s = 0;
			for (i = 0; i < r.length; i++) {
				if (g = r[i]) {
					g.x1 = 1 / 0, g.x2 = -1 / 0, g.y1 = 1 / 0, g.y2 = -1 / 0;
					for (var l = 0; l < g.length; l++) {
						var u = g[l];
						g.x1 = Math.min(g.x1, u.positionX - u.width / 2), g.x2 = Math.max(g.x2, u.positionX + u.width / 2), g.y1 = Math.min(g.y1, u.positionY - u.height / 2), g.y2 = Math.max(g.y2, u.positionY + u.height / 2)
					}
					g.w = g.x2 - g.x1, g.h = g.y2 - g.y1, s += g.w * g.h
				}
			}
			r.sort(function (e, t) {
				return t.w * t.h - e.w * e.h
			});
			var c = 0,
				h = 0,
				d = 0,
				p = 0,
				f = Math.sqrt(s) * e.clientWidth / e.clientHeight;
			for (i = 0; i < r.length; i++) {
				var g;
				if (g = r[i]) {
					for (l = 0; l < g.length; l++) {
						(u = g[l]).isLocked || (u.positionX += c - g.x1, u.positionY += h - g.y1)
					}
					c += g.w + t.componentSpacing, d += g.w + t.componentSpacing, p = Math.max(p, g.h), d > f && (h += p + t.componentSpacing, c = 0, d = 0, p = 0)
				}
			}
		},
		Do = {
			fit: !0,
			padding: 30,
			boundingBox: void 0,
			avoidOverlap: !0,
			avoidOverlapPadding: 10,
			nodeDimensionsIncludeLabels: !1,
			spacingFactor: void 0,
			condense: !1,
			rows: void 0,
			cols: void 0,
			position: function (e) { },
			sort: void 0,
			animate: !1,
			animationDuration: 500,
			animationEasing: void 0,
			animateFilter: function (e, t) {
				return !0
			},
			ready: void 0,
			stop: void 0,
			transform: function (e, t) {
				return t
			}
		};

	function Po(e) {
		this.options = I({}, Do, e)
	}
	Po.prototype.run = function () {
		var e = this.options,
			t = e,
			n = e.cy,
			r = t.eles.nodes().not(":parent");
		t.sort && (r = r.sort(t.sort));
		var i = pt(t.boundingBox ? t.boundingBox : {
			x1: 0,
			y1: 0,
			w: n.width(),
			h: n.height()
		});
		if (0 === i.h || 0 === i.w) r.layoutPositions(this, t, function (e) {
			return {
				x: i.x1,
				y: i.y1
			}
		});
		else {
			var a = r.size(),
				o = Math.sqrt(a * i.h / i.w),
				s = Math.round(o),
				l = Math.round(i.w / i.h * o),
				u = function (e) {
					if (null == e) return Math.min(s, l);
					Math.min(s, l) == s ? s = e : l = e
				},
				c = function (e) {
					if (null == e) return Math.max(s, l);
					Math.max(s, l) == s ? s = e : l = e
				},
				h = t.rows,
				d = null != t.cols ? t.cols : t.columns;
			if (null != h && null != d) s = h, l = d;
			else if (null != h && null == d) s = h, l = Math.ceil(a / s);
			else if (null == h && null != d) l = d, s = Math.ceil(a / l);
			else if (l * s > a) {
				var p = u(),
					f = c();
				(p - 1) * f >= a ? u(p - 1) : (f - 1) * p >= a && c(f - 1)
			} else
				for (; l * s < a;) {
					var g = u(),
						v = c();
					(v + 1) * g >= a ? c(v + 1) : u(g + 1)
				}
			var y = i.w / l,
				m = i.h / s;
			if (t.condense && (y = 0, m = 0), t.avoidOverlap)
				for (var b = 0; b < r.length; b++) {
					var x = r[b],
						w = x._private.position;
					null != w.x && null != w.y || (w.x = 0, w.y = 0);
					var E = x.layoutDimensions(t),
						k = t.avoidOverlapPadding,
						C = E.w + k,
						S = E.h + k;
					y = Math.max(y, C), m = Math.max(m, S)
				}
			for (var D = {}, P = function (e, t) {
				return !!D["c-" + e + "-" + t]
			}, T = function (e, t) {
				D["c-" + e + "-" + t] = !0
			}, M = 0, B = 0, _ = function () {
				++B >= l && (B = 0, M++)
			}, N = {}, I = 0; I < r.length; I++) {
				var z = r[I],
					L = t.position(z);
				if (L && (void 0 !== L.row || void 0 !== L.col)) {
					var A = {
						row: L.row,
						col: L.col
					};
					if (void 0 === A.col)
						for (A.col = 0; P(A.row, A.col);) A.col++;
					else if (void 0 === A.row)
						for (A.row = 0; P(A.row, A.col);) A.row++;
					N[z.id()] = A, T(A.row, A.col)
				}
			}
			r.layoutPositions(this, t, function (e, t) {
				var n, r;
				if (e.locked() || e.isParent()) return !1;
				var a = N[e.id()];
				if (a) n = a.col * y + y / 2 + i.x1, r = a.row * m + m / 2 + i.y1;
				else {
					for (; P(M, B);) _();
					n = B * y + y / 2 + i.x1, r = M * m + m / 2 + i.y1, T(M, B), _()
				}
				return {
					x: n,
					y: r
				}
			})
		}
		return this
	};
	var To = {
		ready: function () { },
		stop: function () { }
	};

	function Mo(e) {
		this.options = I({}, To, e)
	}
	Mo.prototype.run = function () {
		var e = this.options,
			t = e.eles;
		e.cy;
		return this.emit("layoutstart"), t.nodes().positions(function () {
			return {
				x: 0,
				y: 0
			}
		}), this.one("layoutready", e.ready), this.emit("layoutready"), this.one("layoutstop", e.stop), this.emit("layoutstop"), this
	}, Mo.prototype.stop = function () {
		return this
	};
	var Bo = {
		positions: void 0,
		zoom: void 0,
		pan: void 0,
		fit: !0,
		padding: 30,
		animate: !1,
		animationDuration: 500,
		animationEasing: void 0,
		animateFilter: function (e, t) {
			return !0
		},
		ready: void 0,
		stop: void 0,
		transform: function (e, t) {
			return t
		}
	};

	function _o(e) {
		this.options = I({}, Bo, e)
	}
	_o.prototype.run = function () {
		var e = this.options,
			t = e.eles.nodes(),
			n = p(e.positions);
		return t.layoutPositions(this, e, function (t, r) {
			var i = function (t) {
				if (null == e.positions) return et(t.position());
				if (n) return e.positions(t);
				var r = e.positions[t._private.data.id];
				return null == r ? null : r
			}(t);
			return !t.locked() && null != i && i
		}), this
	};
	var No = {
		fit: !0,
		padding: 30,
		boundingBox: void 0,
		animate: !1,
		animationDuration: 500,
		animationEasing: void 0,
		animateFilter: function (e, t) {
			return !0
		},
		ready: void 0,
		stop: void 0,
		transform: function (e, t) {
			return t
		}
	};

	function Io(e) {
		this.options = I({}, No, e)
	}
	Io.prototype.run = function () {
		var e = this.options,
			t = e.cy,
			n = e.eles.nodes().not(":parent"),
			r = pt(e.boundingBox ? e.boundingBox : {
				x1: 0,
				y1: 0,
				w: t.width(),
				h: t.height()
			});
		return n.layoutPositions(this, e, function (e, t) {
			return {
				x: r.x1 + Math.round(Math.random() * r.w),
				y: r.y1 + Math.round(Math.random() * r.h)
			}
		}), this
	};
	var zo = [{
		name: "breadthfirst",
		impl: Qa
	}, {
		name: "circle",
		impl: eo
	}, {
		name: "concentric",
		impl: ro
	}, {
		name: "cose",
		impl: ao
	}, {
		name: "grid",
		impl: Po
	}, {
		name: "null",
		impl: Mo
	}, {
		name: "preset",
		impl: _o
	}, {
		name: "random",
		impl: Io
	}];

	function Lo(e) {
		this.options = e, this.notifications = 0
	}
	var Ao = function () { };
	Lo.prototype = {
		recalculateRenderedStyle: Ao,
		notify: function () {
			this.notifications++
		},
		init: Ao,
		isHeadless: function () {
			return !0
		}
	};
	var Oo = {
		arrowShapeWidth: .3,
		registerArrowShapes: function () {
			var e = this.arrowShapes = {},
				t = this,
				n = function (e, t, n, r, i, a, o) {
					var s = i.x - n / 2 - o,
						l = i.x + n / 2 + o,
						u = i.y - n / 2 - o,
						c = i.y + n / 2 + o;
					return s <= e && e <= l && u <= t && t <= c
				},
				r = function (e, t, n, r, i) {
					var a = e * Math.cos(r) - t * Math.sin(r),
						o = (e * Math.sin(r) + t * Math.cos(r)) * n;
					return {
						x: a * n + i.x,
						y: o + i.y
					}
				},
				i = function (e, t, n, i) {
					for (var a = [], o = 0; o < e.length; o += 2) {
						var s = e[o],
							l = e[o + 1];
						a.push(r(s, l, t, n, i))
					}
					return a
				},
				a = function (e) {
					for (var t = [], n = 0; n < e.length; n++) {
						var r = e[n];
						t.push(r.x, r.y)
					}
					return t
				},
				o = function (e) {
					return e.pstyle("width").pfValue * e.pstyle("arrow-scale").pfValue * 2
				},
				s = function (r, s) {
					d(s) && (s = e[s]), e[r] = I({
						name: r,
						points: [-.15, -.3, .15, -.3, .15, .3, -.15, .3],
						collide: function (e, t, n, r, o, s) {
							var l = a(i(this.points, n + 2 * s, r, o));
							return Dt(e, t, l)
						},
						roughCollide: n,
						draw: function (e, n, r, a) {
							var o = i(this.points, n, r, a);
							t.arrowShapeImpl("polygon")(e, o)
						},
						spacing: function (e) {
							return 0
						},
						gap: o
					}, s)
				};
			s("none", {
				collide: be,
				roughCollide: be,
				draw: we,
				spacing: xe,
				gap: xe
			}), s("triangle", {
				points: [-.15, -.3, 0, 0, .15, -.3]
			}), s("arrow", "triangle"), s("triangle-backcurve", {
				points: e.triangle.points,
				controlPoint: [0, -.15],
				roughCollide: n,
				draw: function (e, n, a, o, s) {
					var l = i(this.points, n, a, o),
						u = this.controlPoint,
						c = r(u[0], u[1], n, a, o);
					t.arrowShapeImpl(this.name)(e, l, c)
				},
				gap: function (e) {
					return .8 * o(e)
				}
			}), s("triangle-tee", {
				points: [0, 0, .15, -.3, -.15, -.3, 0, 0],
				pointsTee: [-.15, -.4, -.15, -.5, .15, -.5, .15, -.4],
				collide: function (e, t, n, r, o, s, l) {
					var u = a(i(this.points, n + 2 * l, r, o)),
						c = a(i(this.pointsTee, n + 2 * l, r, o));
					return Dt(e, t, u) || Dt(e, t, c)
				},
				draw: function (e, n, r, a, o) {
					var s = i(this.points, n, r, a),
						l = i(this.pointsTee, n, r, a);
					t.arrowShapeImpl(this.name)(e, s, l)
				}
			}), s("triangle-cross", {
				points: [0, 0, .15, -.3, -.15, -.3, 0, 0],
				baseCrossLinePts: [-.15, -.4, -.15, -.4, .15, -.4, .15, -.4],
				crossLinePts: function (e, t) {
					var n = this.baseCrossLinePts.slice(),
						r = t / e;
					return n[3] = n[3] - r, n[5] = n[5] - r, n
				},
				collide: function (e, t, n, r, o, s, l) {
					var u = a(i(this.points, n + 2 * l, r, o)),
						c = a(i(this.crossLinePts(n, s), n + 2 * l, r, o));
					return Dt(e, t, u) || Dt(e, t, c)
				},
				draw: function (e, n, r, a, o) {
					var s = i(this.points, n, r, a),
						l = i(this.crossLinePts(n, o), n, r, a);
					t.arrowShapeImpl(this.name)(e, s, l)
				}
			}), s("vee", {
				points: [-.15, -.3, 0, 0, .15, -.3, 0, -.15],
				gap: function (e) {
					return .525 * o(e)
				}
			}), s("circle", {
				radius: .15,
				collide: function (e, t, n, r, i, a, o) {
					var s = i;
					return Math.pow(s.x - e, 2) + Math.pow(s.y - t, 2) <= Math.pow((n + 2 * o) * this.radius, 2)
				},
				draw: function (e, n, r, i, a) {
					t.arrowShapeImpl(this.name)(e, i.x, i.y, this.radius * n)
				},
				spacing: function (e) {
					return t.getArrowWidth(e.pstyle("width").pfValue, e.pstyle("arrow-scale").value) * this.radius
				}
			}), s("tee", {
				points: [-.15, 0, -.15, -.1, .15, -.1, .15, 0],
				spacing: function (e) {
					return 1
				},
				gap: function (e) {
					return 1
				}
			}), s("square", {
				points: [-.15, 0, .15, 0, .15, -.3, -.15, -.3]
			}), s("diamond", {
				points: [-.15, -.15, 0, -.3, .15, -.15, 0, 0],
				gap: function (e) {
					return e.pstyle("width").pfValue * e.pstyle("arrow-scale").value
				}
			}), s("chevron", {
				points: [0, 0, -.15, -.15, -.1, -.2, 0, -.1, .1, -.2, .15, -.15],
				gap: function (e) {
					return .95 * e.pstyle("width").pfValue * e.pstyle("arrow-scale").value
				}
			})
		}
	},
		Ro = {
			projectIntoViewport: function (e, t) {
				var n = this.cy,
					r = this.findContainerClientCoords(),
					i = r[0],
					a = r[1],
					o = r[4],
					s = n.pan(),
					l = n.zoom();
				return [((e - i) / o - s.x) / l, ((t - a) / o - s.y) / l]
			},
			findContainerClientCoords: function () {
				if (this.containerBB) return this.containerBB;
				try {
					this.container.getBoundingClientRect();
				} catch (e) {
					return
				}
				
				var e = this.container,
					t = e.getBoundingClientRect(),
					n = a.getComputedStyle(e),
					r = function (e) {
						return parseFloat(n.getPropertyValue(e))
					},
					i = r("padding-left"),
					o = r("padding-right"),
					s = r("padding-top"),
					l = r("padding-bottom"),
					u = r("border-left-width"),
					c = r("border-right-width"),
					h = r("border-top-width"),
					d = (r("border-bottom-width"), e.clientWidth),
					p = e.clientHeight,
					f = i + o,
					g = s + l,
					v = u + c,
					y = t.width / (d + v),
					m = d - f,
					b = p - g,
					x = t.left + i + u,
					w = t.top + s + h;
				return this.containerBB = [x, w, m, b, y]
			},
			invalidateContainerClientCoordsCache: function () {
				this.containerBB = null
			},
			findNearestElement: function (e, t, n, r) {
				return this.findNearestElements(e, t, n, r)[0]
			},
			findNearestElements: function (e, t, n, r) {
				var i, a, o = this,
					s = this,
					l = s.getCachedZSortedEles(),
					u = [],
					c = s.cy.zoom(),
					h = s.cy.hasCompoundNodes(),
					d = (r ? 24 : 8) / c,
					p = (r ? 8 : 2) / c,
					f = (r ? 8 : 2) / c,
					g = 1 / 0;

				function v(e, t) {
					if (e.isNode()) {
						if (a) return;
						a = e, u.push(e)
					}
					if (e.isEdge() && (null == t || t < g))
						if (i) {
							if (i.pstyle("z-compound-depth").value === e.pstyle("z-compound-depth").value && i.pstyle("z-compound-depth").value === e.pstyle("z-compound-depth").value)
								for (var n = 0; n < u.length; n++)
									if (u[n].isEdge()) {
										u[n] = e, i = e, g = null != t ? t : g;
										break
									}
						} else u.push(e), i = e, g = null != t ? t : g
				}

				function y(n) {
					var r = n.outerWidth() + 2 * p,
						i = n.outerHeight() + 2 * p,
						a = r / 2,
						l = i / 2,
						u = n.position();
					if (u.x - a <= e && e <= u.x + a && u.y - l <= t && t <= u.y + l && s.nodeShapes[o.getNodeShape(n)].checkPoint(e, t, 0, r, i, u.x, u.y)) return v(n, 0), !0
				}

				function m(n) {
					var r, i = n._private,
						a = i.rscratch,
						l = n.pstyle("width").pfValue,
						c = n.pstyle("arrow-scale").value,
						p = l / 2 + d,
						f = p * p,
						g = 2 * p,
						m = i.source,
						b = i.target;
					if ("segments" === a.edgeType || "straight" === a.edgeType || "haystack" === a.edgeType) {
						for (var x = a.allpts, w = 0; w + 3 < x.length; w += 2)
							if (Et(e, t, x[w], x[w + 1], x[w + 2], x[w + 3], g) && f > (r = St(e, t, x[w], x[w + 1], x[w + 2], x[w + 3]))) return v(n, r), !0
					} else if ("bezier" === a.edgeType || "multibezier" === a.edgeType || "self" === a.edgeType || "compound" === a.edgeType)
						for (x = a.allpts, w = 0; w + 5 < a.allpts.length; w += 4)
							if (kt(e, t, x[w], x[w + 1], x[w + 2], x[w + 3], x[w + 4], x[w + 5], g) && f > (r = Ct(e, t, x[w], x[w + 1], x[w + 2], x[w + 3], x[w + 4], x[w + 5]))) return v(n, r), !0;
					m = m || i.source, b = b || i.target;
					var E = o.getArrowWidth(l, c),
						k = [{
							name: "source",
							x: a.arrowStartX,
							y: a.arrowStartY,
							angle: a.srcArrowAngle
						}, {
							name: "target",
							x: a.arrowEndX,
							y: a.arrowEndY,
							angle: a.tgtArrowAngle
						}, {
							name: "mid-source",
							x: a.midX,
							y: a.midY,
							angle: a.midsrcArrowAngle
						}, {
							name: "mid-target",
							x: a.midX,
							y: a.midY,
							angle: a.midtgtArrowAngle
						}];
					for (w = 0; w < k.length; w++) {
						var C = k[w],
							S = s.arrowShapes[n.pstyle(C.name + "-arrow-shape").value],
							D = n.pstyle("width").pfValue;
						if (S.roughCollide(e, t, E, C.angle, {
							x: C.x,
							y: C.y
						}, D, d) && S.collide(e, t, E, C.angle, {
							x: C.x,
							y: C.y
						}, D, d)) return v(n), !0
					}
					h && u.length > 0 && (y(m), y(b))
				}

				function b(e, t, n) {
					return Ne(e, t, n)
				}

				function x(n, r) {
					var i, a = n._private,
						o = f;
					i = r ? r + "-" : "";
					var s = a.labelBounds[r || "main"],
						l = n.pstyle(i + "label").value;
					if ("yes" === n.pstyle("text-events").strValue && l) {
						var u = a.rstyle,
							c = b(u, "labelX", r),
							h = b(u, "labelY", r),
							d = b(a.rscratch, "labelAngle", r),
							p = s.x1 - o,
							g = s.x2 + o,
							y = s.y1 - o,
							m = s.y2 + o;
						if (d) {
							var x = Math.cos(d),
								w = Math.sin(d),
								E = function (e, t) {
									return {
										x: (e -= c) * x - (t -= h) * w + c,
										y: e * w + t * x + h
									}
								},
								k = E(p, y),
								C = E(p, m),
								S = E(g, y),
								D = E(g, m),
								P = [k.x, k.y, S.x, S.y, D.x, D.y, C.x, C.y];
							if (Dt(e, t, P)) return v(n), !0
						} else if (bt(s, e, t)) return v(n), !0
					}
				}
				n && (l = l.interactive);
				for (var w = l.length - 1; w >= 0; w--) {
					var E = l[w];
					E.isNode() ? y(E) || x(E) : m(E) || x(E) || x(E, "source") || x(E, "target")
				}
				return u
			},
			getAllInBox: function (e, t, n, r) {
				for (var i, a, o = this.getCachedZSortedEles().interactive, s = [], l = Math.min(e, n), u = Math.max(e, n), c = Math.min(t, r), h = Math.max(t, r), d = pt({
					x1: e = l,
					y1: t = c,
					x2: n = u,
					y2: r = h
				}), p = 0; p < o.length; p++) {
					var f = o[p];
					if (f.isNode()) {
						var g = f,
							v = g.boundingBox({
								includeNodes: !0,
								includeEdges: !1,
								includeLabels: !1
							});
						mt(d, v) && !xt(v, d) && s.push(g)
					} else {
						var y = f,
							m = y._private,
							b = m.rscratch;
						if (null != b.startX && null != b.startY && !bt(d, b.startX, b.startY)) continue;
						if (null != b.endX && null != b.endY && !bt(d, b.endX, b.endY)) continue;
						if ("bezier" === b.edgeType || "multibezier" === b.edgeType || "self" === b.edgeType || "compound" === b.edgeType || "segments" === b.edgeType || "haystack" === b.edgeType) {
							for (var x = m.rstyle.bezierPts || m.rstyle.linePts || m.rstyle.haystackPts, w = !0, E = 0; E < x.length; E++)
								if (i = d, a = x[E], !bt(i, a.x, a.y)) {
									w = !1;
									break
								}
							w && s.push(y)
						} else "haystack" !== b.edgeType && "straight" !== b.edgeType || s.push(y)
					}
				}
				return s
			}
		},
		Vo = {
			calculateArrowAngles: function (e) {
				var t, n, r, i, a, o, s = e._private.rscratch,
					l = "haystack" === s.edgeType,
					u = "bezier" === s.edgeType,
					c = "multibezier" === s.edgeType,
					h = "segments" === s.edgeType,
					d = "compound" === s.edgeType,
					p = "self" === s.edgeType;
				if (l ? (r = s.haystackPts[0], i = s.haystackPts[1], a = s.haystackPts[2], o = s.haystackPts[3]) : (r = s.arrowStartX, i = s.arrowStartY, a = s.arrowEndX, o = s.arrowEndY), g = s.midX, v = s.midY, h) t = r - s.segpts[0], n = i - s.segpts[1];
				else if (c || d || p || u) {
					var f = s.allpts;
					t = r - ct(f[0], f[2], f[4], .1), n = i - ct(f[1], f[3], f[5], .1)
				} else t = r - g, n = i - v;
				s.srcArrowAngle = it(t, n);
				var g = s.midX,
					v = s.midY;
				if (l && (g = (r + a) / 2, v = (i + o) / 2), t = a - r, n = o - i, h)
					if ((f = s.allpts).length / 2 % 2 == 0) {
						var y = (m = f.length / 2) - 2;
						t = f[m] - f[y], n = f[m + 1] - f[y + 1]
					} else {
						y = (m = f.length / 2 - 1) - 2;
						var m, b = m + 2;
						t = f[m] - f[y], n = f[m + 1] - f[y + 1]
					} else if (c || d || p) {
						var x, w, E, k, f = s.allpts;
						if (s.ctrlpts.length / 2 % 2 == 0) {
							var C = (S = (D = f.length / 2 - 1) + 2) + 2;
							x = ct(f[D], f[S], f[C], 0), w = ct(f[D + 1], f[S + 1], f[C + 1], 0), E = ct(f[D], f[S], f[C], 1e-4), k = ct(f[D + 1], f[S + 1], f[C + 1], 1e-4)
						} else {
							var S, D;
							C = (S = f.length / 2 - 1) + 2;
							x = ct(f[D = S - 2], f[S], f[C], .4999), w = ct(f[D + 1], f[S + 1], f[C + 1], .4999), E = ct(f[D], f[S], f[C], .5), k = ct(f[D + 1], f[S + 1], f[C + 1], .5)
						}
						t = E - x, n = k - w
					} (s.midtgtArrowAngle = it(t, n), s.midDispX = t, s.midDispY = n, t *= -1, n *= -1, h) && ((f = s.allpts).length / 2 % 2 == 0 || (t = -(f[b = (m = f.length / 2 - 1) + 2] - f[m]), n = -(f[b + 1] - f[m + 1])));
				if (s.midsrcArrowAngle = it(t, n), h) t = a - s.segpts[s.segpts.length - 2], n = o - s.segpts[s.segpts.length - 1];
				else if (c || d || p || u) {
					var P = (f = s.allpts).length;
					t = a - ct(f[P - 6], f[P - 4], f[P - 2], .9), n = o - ct(f[P - 5], f[P - 3], f[P - 1], .9)
				} else t = a - g, n = o - v;
				s.tgtArrowAngle = it(t, n)
			}
		};
	Vo.getArrowWidth = Vo.getArrowHeight = function (e, t) {
		var n = this.arrowWidthCache = this.arrowWidthCache || {},
			r = n[e + ", " + t];
		return r || (r = Math.max(Math.pow(13.37 * e, .9), 29) * t, n[e + ", " + t] = r, r)
	};
	var Fo = {};

	function qo(e) {
		var t = [];
		if (null != e) {
			for (var n = 0; n < e.length; n += 2) {
				var r = e[n],
					i = e[n + 1];
				t.push({
					x: r,
					y: i
				})
			}
			return t
		}
	}
	Fo.findHaystackPoints = function (e) {
		for (var t = 0; t < e.length; t++) {
			var n = e[t],
				r = n._private,
				i = r.rscratch;
			if (!i.haystack) {
				var a = 2 * Math.random() * Math.PI;
				i.source = {
					x: Math.cos(a),
					y: Math.sin(a)
				}, a = 2 * Math.random() * Math.PI, i.target = {
					x: Math.cos(a),
					y: Math.sin(a)
				}
			}
			var o = r.source,
				s = r.target,
				l = o.position(),
				u = s.position(),
				c = o.width(),
				h = s.width(),
				d = o.height(),
				p = s.height(),
				f = n.pstyle("haystack-radius").value / 2;
			i.haystackPts = i.allpts = [i.source.x * c * f + l.x, i.source.y * d * f + l.y, i.target.x * h * f + u.x, i.target.y * p * f + u.y], i.midX = (i.allpts[0] + i.allpts[2]) / 2, i.midY = (i.allpts[1] + i.allpts[3]) / 2, i.edgeType = "haystack", i.haystack = !0, this.storeEdgeProjections(n), this.calculateArrowAngles(n), this.recalculateEdgeLabelProjections(n), this.calculateLabelAngles(n)
		}
	}, Fo.findSegmentsPoints = function (e, t) {
		var n = e._private.rscratch,
			r = t.posPts,
			i = t.intersectionPts,
			a = t.vectorNormInverse,
			o = e.pstyle("edge-distances").value,
			s = e.pstyle("segment-weights"),
			l = e.pstyle("segment-distances"),
			u = Math.min(s.pfValue.length, l.pfValue.length);
		n.edgeType = "segments", n.segpts = [];
		for (var c = 0; c < u; c++) {
			var h = s.pfValue[c],
				d = l.pfValue[c],
				p = 1 - h,
				f = h,
				g = "node-position" === o ? r : i,
				v = {
					x: g.x1 * p + g.x2 * f,
					y: g.y1 * p + g.y2 * f
				};
			n.segpts.push(v.x + a.x * d, v.y + a.y * d)
		}
	}, Fo.findLoopPoints = function (e, t, n, r) {
		var i = e._private.rscratch,
			a = t.dirCounts,
			o = t.srcPos,
			s = e.pstyle("control-point-distances"),
			l = s ? s.pfValue[0] : void 0,
			u = e.pstyle("loop-direction").pfValue,
			c = e.pstyle("loop-sweep").pfValue,
			h = e.pstyle("control-point-step-size").pfValue;
		i.edgeType = "self";
		var d = n,
			p = h;
		r && (d = 0, p = l);
		var f = u - Math.PI / 2,
			g = f - c / 2,
			v = f + c / 2,
			y = String(u + "_" + c);
		d = void 0 === a[y] ? a[y] = 0 : ++a[y], i.ctrlpts = [o.x + 1.4 * Math.cos(g) * p * (d / 3 + 1), o.y + 1.4 * Math.sin(g) * p * (d / 3 + 1), o.x + 1.4 * Math.cos(v) * p * (d / 3 + 1), o.y + 1.4 * Math.sin(v) * p * (d / 3 + 1)]
	}, Fo.findCompoundLoopPoints = function (e, t, n, r) {
		var i = e._private.rscratch;
		i.edgeType = "compound";
		var a = t.srcPos,
			o = t.tgtPos,
			s = t.srcW,
			l = t.srcH,
			u = t.tgtW,
			c = t.tgtH,
			h = e.pstyle("control-point-step-size").pfValue,
			d = e.pstyle("control-point-distances"),
			p = d ? d.pfValue[0] : void 0,
			f = n,
			g = h;
		r && (f = 0, g = p);
		var v = {
			x: a.x - s / 2,
			y: a.y - l / 2
		},
			y = {
				x: o.x - u / 2,
				y: o.y - c / 2
			},
			m = {
				x: Math.min(v.x, y.x),
				y: Math.min(v.y, y.y)
			},
			b = Math.max(.5, Math.log(.01 * s)),
			x = Math.max(.5, Math.log(.01 * u));
		i.ctrlpts = [m.x, m.y - (1 + Math.pow(50, 1.12) / 100) * g * (f / 3 + 1) * b, m.x - (1 + Math.pow(50, 1.12) / 100) * g * (f / 3 + 1) * x, m.y]
	}, Fo.findStraightEdgePoints = function (e) {
		e._private.rscratch.edgeType = "straight"
	}, Fo.findBezierPoints = function (e, t, n, r, i) {
		var a = e._private.rscratch,
			o = t.vectorNormInverse,
			s = t.posPts,
			l = t.intersectionPts,
			u = e.pstyle("edge-distances").value,
			c = e.pstyle("control-point-step-size").pfValue,
			h = e.pstyle("control-point-distances"),
			d = e.pstyle("control-point-weights"),
			p = h && d ? Math.min(h.value.length, d.value.length) : 1,
			f = h ? h.pfValue[0] : void 0,
			g = d.value[0],
			v = r;
		a.edgeType = v ? "multibezier" : "bezier", a.ctrlpts = [];
		for (var y = 0; y < p; y++) {
			var m = (.5 - t.eles.length / 2 + n) * c * (i ? -1 : 1),
				b = void 0,
				x = ot(m);
			v && (f = h ? h.pfValue[y] : c, g = d.value[y]);
			var w = void 0 !== (b = r ? f : void 0 !== f ? x * f : void 0) ? b : m,
				E = 1 - g,
				k = g,
				C = "node-position" === u ? s : l,
				S = {
					x: C.x1 * E + C.x2 * k,
					y: C.y1 * E + C.y2 * k
				};
			a.ctrlpts.push(S.x + o.x * w, S.y + o.y * w)
		}
	}, Fo.findTaxiPoints = function (e, t) {
		var n = e._private.rscratch;
		n.edgeType = "segments";
		var r = t.posPts,
			i = t.srcW,
			a = t.srcH,
			o = t.tgtW,
			s = t.tgtH,
			l = "node-position" !== e.pstyle("edge-distances").value,
			u = e.pstyle("taxi-direction").value,
			c = u,
			h = e.pstyle("taxi-turn"),
			d = h.pfValue,
			p = e.pstyle("taxi-turn-min-distance").pfValue,
			f = "%" === h.units,
			g = l ? (i + o) / 2 : 0,
			v = l ? (a + s) / 2 : 0,
			y = r.x2 - r.x1,
			m = r.y2 - r.y1,
			b = function (e, t) {
				return e > 0 ? Math.max(e - t, 0) : Math.min(e + t, 0)
			},
			x = b(y, g),
			w = b(m, v),
			E = !1;
		"auto" === u ? u = Math.abs(x) > Math.abs(w) ? "horizontal" : "vertical" : "upward" === u || "downward" === u ? (u = "vertical", E = !0) : "leftward" !== u && "rightward" !== u || (u = "horizontal", E = !0);
		var k = "vertical" === u,
			C = k ? w : x,
			S = k ? m : y,
			D = ot(S),
			P = !1;
		E && f || !("downward" === c && S < 0 || "upward" === c && S > 0 || "leftward" === c && S > 0 || "rightward" === c && S < 0) || (C = (D *= -1) * Math.abs(C), P = !0);
		var T = f ? d * C : d * D,
			M = function (e) {
				return Math.abs(e) < p || Math.abs(e) >= Math.abs(C)
			},
			B = M(T),
			_ = M(C - T);
		if ((B || _) && !P)
			if (k) {
				var N = Math.abs(S) <= a / 2,
					I = Math.abs(y) <= o / 2;
				if (N) {
					var z = (r.x1 + r.x2) / 2,
						L = r.y1,
						A = r.y2;
					n.segpts = [z, L, z, A]
				} else if (I) {
					var O = (r.y1 + r.y2) / 2,
						R = r.x1,
						V = r.x2;
					n.segpts = [R, O, V, O]
				} else n.segpts = [r.x1, r.y2]
			} else {
				var F = Math.abs(S) <= i / 2,
					q = Math.abs(m) <= s / 2;
				if (F) {
					var Y = (r.y1 + r.y2) / 2,
						j = r.x1,
						X = r.x2;
					n.segpts = [j, Y, X, Y]
				} else if (q) {
					var W = (r.x1 + r.x2) / 2,
						H = r.y1,
						K = r.y2;
					n.segpts = [W, H, W, K]
				} else n.segpts = [r.x2, r.y1]
			} else if (k) {
				var G = r.y1 + T + (l ? a / 2 * D : 0),
					Z = r.x1,
					U = r.x2;
				n.segpts = [Z, G, U, G]
			} else {
			var $ = r.x1 + T + (l ? i / 2 * D : 0),
				Q = r.y1,
				J = r.y2;
			n.segpts = [$, Q, $, J]
		}
	}, Fo.tryToCorrectInvalidPoints = function (e, t) {
		var n = e._private.rscratch;
		if ("bezier" === n.edgeType) {
			var r = t.srcPos,
				i = t.tgtPos,
				a = t.srcW,
				o = t.srcH,
				s = t.tgtW,
				l = t.tgtH,
				u = t.srcShape,
				c = t.tgtShape,
				h = !v(n.startX) || !v(n.startY),
				d = !v(n.arrowStartX) || !v(n.arrowStartY),
				p = !v(n.endX) || !v(n.endY),
				f = !v(n.arrowEndX) || !v(n.arrowEndY),
				g = 3 * (this.getArrowWidth(e.pstyle("width").pfValue, e.pstyle("arrow-scale").value) * this.arrowShapeWidth),
				y = st({
					x: n.ctrlpts[0],
					y: n.ctrlpts[1]
				}, {
						x: n.startX,
						y: n.startY
					}),
				m = y < g,
				b = st({
					x: n.ctrlpts[0],
					y: n.ctrlpts[1]
				}, {
						x: n.endX,
						y: n.endY
					}),
				x = b < g,
				w = !1;
			if (h || d || m) {
				w = !0;
				var E = {
					x: n.ctrlpts[0] - r.x,
					y: n.ctrlpts[1] - r.y
				},
					k = Math.sqrt(E.x * E.x + E.y * E.y),
					C = {
						x: E.x / k,
						y: E.y / k
					},
					S = Math.max(a, o),
					D = {
						x: n.ctrlpts[0] + 2 * C.x * S,
						y: n.ctrlpts[1] + 2 * C.y * S
					},
					P = u.intersectLine(r.x, r.y, a, o, D.x, D.y, 0);
				m ? (n.ctrlpts[0] = n.ctrlpts[0] + C.x * (g - y), n.ctrlpts[1] = n.ctrlpts[1] + C.y * (g - y)) : (n.ctrlpts[0] = P[0] + C.x * g, n.ctrlpts[1] = P[1] + C.y * g)
			}
			if (p || f || x) {
				w = !0;
				var T = {
					x: n.ctrlpts[0] - i.x,
					y: n.ctrlpts[1] - i.y
				},
					M = Math.sqrt(T.x * T.x + T.y * T.y),
					B = {
						x: T.x / M,
						y: T.y / M
					},
					_ = Math.max(a, o),
					N = {
						x: n.ctrlpts[0] + 2 * B.x * _,
						y: n.ctrlpts[1] + 2 * B.y * _
					},
					I = c.intersectLine(i.x, i.y, s, l, N.x, N.y, 0);
				x ? (n.ctrlpts[0] = n.ctrlpts[0] + B.x * (g - b), n.ctrlpts[1] = n.ctrlpts[1] + B.y * (g - b)) : (n.ctrlpts[0] = I[0] + B.x * g, n.ctrlpts[1] = I[1] + B.y * g)
			}
			w && this.findEndpoints(e)
		}
	}, Fo.storeAllpts = function (e) {
		var t = e._private.rscratch;
		if ("multibezier" === t.edgeType || "bezier" === t.edgeType || "self" === t.edgeType || "compound" === t.edgeType) {
			t.allpts = [], t.allpts.push(t.startX, t.startY);
			for (var n = 0; n + 1 < t.ctrlpts.length; n += 2) t.allpts.push(t.ctrlpts[n], t.ctrlpts[n + 1]), n + 3 < t.ctrlpts.length && t.allpts.push((t.ctrlpts[n] + t.ctrlpts[n + 2]) / 2, (t.ctrlpts[n + 1] + t.ctrlpts[n + 3]) / 2);
			var r;
			t.allpts.push(t.endX, t.endY), t.ctrlpts.length / 2 % 2 == 0 ? (r = t.allpts.length / 2 - 1, t.midX = t.allpts[r], t.midY = t.allpts[r + 1]) : (r = t.allpts.length / 2 - 3, .5, t.midX = ct(t.allpts[r], t.allpts[r + 2], t.allpts[r + 4], .5), t.midY = ct(t.allpts[r + 1], t.allpts[r + 3], t.allpts[r + 5], .5))
		} else if ("straight" === t.edgeType) t.allpts = [t.startX, t.startY, t.endX, t.endY], t.midX = (t.startX + t.endX + t.arrowStartX + t.arrowEndX) / 4, t.midY = (t.startY + t.endY + t.arrowStartY + t.arrowEndY) / 4;
		else if ("segments" === t.edgeType)
			if (t.allpts = [], t.allpts.push(t.startX, t.startY), t.allpts.push.apply(t.allpts, t.segpts), t.allpts.push(t.endX, t.endY), t.segpts.length % 4 == 0) {
				var i = t.segpts.length / 2,
					a = i - 2;
				t.midX = (t.segpts[a] + t.segpts[i]) / 2, t.midY = (t.segpts[a + 1] + t.segpts[i + 1]) / 2
			} else {
				var o = t.segpts.length / 2 - 1;
				t.midX = t.segpts[o], t.midY = t.segpts[o + 1]
			}
	}, Fo.checkForInvalidEdgeWarning = function (e) {
		var t = e[0]._private.rscratch;
		t.nodesOverlap || v(t.startX) && v(t.startY) && v(t.endX) && v(t.endY) ? t.loggedErr = !1 : t.loggedErr || (t.loggedErr = !0, Ce("Edge `" + e.id() + "` has invalid endpoints and so it is impossible to draw.  Adjust your edge style (e.g. control points) accordingly or use an alternative edge type.  This is expected behaviour when the source node and the target node overlap."))
	}, Fo.findEdgeControlPoints = function (e) {
		var t = this;
		if (e && 0 !== e.length) {
			for (var n = this, r = n.cy.hasCompoundNodes(), i = {
				map: new ze,
				get: function (e) {
					var t = this.map.get(e[0]);
					return null != t ? t.get(e[1]) : null
				},
				set: function (e, t) {
					var n = this.map.get(e[0]);
					null == n && (n = new ze, this.map.set(e[0], n)), n.set(e[1], t)
				}
			}, a = [], o = [], s = 0; s < e.length; s++) {
				var l = e[s],
					u = l._private,
					c = l.pstyle("curve-style").value;
				if (!l.removed() && l.takesUpSpace())
					if ("haystack" !== c) {
						var h = "unbundled-bezier" === c || "segments" === c || "straight" === c || "taxi" === c,
							d = "unbundled-bezier" === c || "bezier" === c,
							p = u.source,
							f = u.target,
							g = [p.poolIndex(), f.poolIndex()].sort(),
							y = i.get(g);
						null == y && (y = {
							eles: []
						}, i.set(g, y), a.push(g)), y.eles.push(l), h && (y.hasUnbundled = !0), d && (y.hasBezier = !0)
					} else o.push(l)
			}
			for (var m = function (e) {
				var o = a[e],
					s = i.get(o),
					l = void 0;
				if (!s.hasUnbundled) {
					var u = s.eles[0].parallelEdges().filter(function (e) {
						return e.isBundledBezier()
					});
					_e(s.eles), u.forEach(function (e) {
						return s.eles.push(e)
					}), s.eles.sort(function (e, t) {
						return e.poolIndex() - t.poolIndex()
					})
				}
				var c = s.eles[0],
					h = c.source(),
					d = c.target();
				if (h.poolIndex() > d.poolIndex()) {
					var p = h;
					h = d, d = p
				}
				var f = s.srcPos = h.position(),
					g = s.tgtPos = d.position(),
					y = s.srcW = h.outerWidth(),
					m = s.srcH = h.outerHeight(),
					b = s.tgtW = d.outerWidth(),
					x = s.tgtH = d.outerHeight(),
					w = s.srcShape = n.nodeShapes[t.getNodeShape(h)],
					E = s.tgtShape = n.nodeShapes[t.getNodeShape(d)];
				s.dirCounts = {
					north: 0,
					west: 0,
					south: 0,
					east: 0,
					northwest: 0,
					southwest: 0,
					northeast: 0,
					southeast: 0
				};
				for (var k = 0; k < s.eles.length; k++) {
					var C = s.eles[k],
						S = C[0]._private.rscratch,
						D = C.pstyle("curve-style").value,
						P = "unbundled-bezier" === D || "segments" === D || "taxi" === D,
						T = !h.same(C.source());
					if (!s.calculatedIntersection && h !== d && (s.hasBezier || s.hasUnbundled)) {
						s.calculatedIntersection = !0;
						var M = w.intersectLine(f.x, f.y, y, m, g.x, g.y, 0),
							B = s.srcIntn = M,
							_ = E.intersectLine(g.x, g.y, b, x, f.x, f.y, 0),
							N = s.tgtIntn = _,
							I = s.intersectionPts = {
								x1: M[0],
								x2: _[0],
								y1: M[1],
								y2: _[1]
							},
							z = s.posPts = {
								x1: f.x,
								x2: g.x,
								y1: f.y,
								y2: g.y
							},
							L = _[1] - M[1],
							A = _[0] - M[0],
							O = Math.sqrt(A * A + L * L),
							R = s.vector = {
								x: A,
								y: L
							},
							V = s.vectorNorm = {
								x: R.x / O,
								y: R.y / O
							},
							F = {
								x: -V.y,
								y: V.x
							};
						s.nodesOverlap = !v(O) || E.checkPoint(M[0], M[1], 0, b, x, g.x, g.y) || w.checkPoint(_[0], _[1], 0, y, m, f.x, f.y), s.vectorNormInverse = F, l = {
							nodesOverlap: s.nodesOverlap,
							dirCounts: s.dirCounts,
							calculatedIntersection: !0,
							hasBezier: s.hasBezier,
							hasUnbundled: s.hasUnbundled,
							eles: s.eles,
							srcPos: g,
							tgtPos: f,
							srcW: b,
							srcH: x,
							tgtW: y,
							tgtH: m,
							srcIntn: N,
							tgtIntn: B,
							srcShape: E,
							tgtShape: w,
							posPts: {
								x1: z.x2,
								y1: z.y2,
								x2: z.x1,
								y2: z.y1
							},
							intersectionPts: {
								x1: I.x2,
								y1: I.y2,
								x2: I.x1,
								y2: I.y1
							},
							vector: {
								x: -R.x,
								y: -R.y
							},
							vectorNorm: {
								x: -V.x,
								y: -V.y
							},
							vectorNormInverse: {
								x: -F.x,
								y: -F.y
							}
						}
					}
					var q = T ? l : s;
					S.nodesOverlap = q.nodesOverlap, S.srcIntn = q.srcIntn, S.tgtIntn = q.tgtIntn, r && (h.isParent() || h.isChild() || d.isParent() || d.isChild()) && (h.parents().anySame(d) || d.parents().anySame(h) || h.same(d)) ? t.findCompoundLoopPoints(C, q, k, P) : h === d ? t.findLoopPoints(C, q, k, P) : "segments" === D ? t.findSegmentsPoints(C, q) : "taxi" === D ? t.findTaxiPoints(C, q) : "straight" === D || !P && s.eles.length % 2 == 1 && k === Math.floor(s.eles.length / 2) ? t.findStraightEdgePoints(C) : t.findBezierPoints(C, q, k, P, T), t.findEndpoints(C), t.tryToCorrectInvalidPoints(C, q), t.checkForInvalidEdgeWarning(C), t.storeAllpts(C), t.storeEdgeProjections(C), t.calculateArrowAngles(C), t.recalculateEdgeLabelProjections(C), t.calculateLabelAngles(C)
				}
			}, b = 0; b < a.length; b++) m(b);
			this.findHaystackPoints(o)
		}
	}, Fo.getSegmentPoints = function (e) {
		var t = e[0]._private.rscratch;
		if ("segments" === t.edgeType) return this.recalculateRenderedStyle(e), qo(t.segpts)
	}, Fo.getControlPoints = function (e) {
		var t = e[0]._private.rscratch,
			n = t.edgeType;
		if ("bezier" === n || "multibezier" === n || "self" === n || "compound" === n) return this.recalculateRenderedStyle(e), qo(t.ctrlpts)
	}, Fo.getEdgeMidpoint = function (e) {
		var t = e[0]._private.rscratch;
		return this.recalculateRenderedStyle(e), {
			x: t.midX,
			y: t.midY
		}
	};
	var Yo = {
		manualEndptToPx: function (e, t) {
			var n = e.position(),
				r = e.outerWidth(),
				i = e.outerHeight();
			if (2 === t.value.length) {
				var a = [t.pfValue[0], t.pfValue[1]];
				return "%" === t.units[0] && (a[0] = a[0] * r), "%" === t.units[1] && (a[1] = a[1] * i), a[0] += n.x, a[1] += n.y, a
			}
			var o = t.pfValue[0];
			o = -Math.PI / 2 + o;
			var s = 2 * Math.max(r, i),
				l = [n.x + Math.cos(o) * s, n.y + Math.sin(o) * s];
			return this.nodeShapes[this.getNodeShape(e)].intersectLine(n.x, n.y, r, i, l[0], l[1], 0)
		},
		findEndpoints: function (e) {
			var t, n, r, i, a, o = this,
				s = e.source()[0],
				l = e.target()[0],
				u = s.position(),
				c = l.position(),
				h = e.pstyle("target-arrow-shape").value,
				d = e.pstyle("source-arrow-shape").value,
				p = e.pstyle("target-distance-from-node").pfValue,
				f = e.pstyle("source-distance-from-node").pfValue,
				g = e.pstyle("curve-style").value,
				y = e._private.rscratch,
				m = y.edgeType,
				b = "self" === m || "compound" === m,
				x = "bezier" === m || "multibezier" === m || b,
				w = "bezier" !== m,
				E = "straight" === m || "segments" === m,
				k = "segments" === m,
				C = x || w || E,
				S = b || "taxi" === g,
				D = e.pstyle("source-endpoint"),
				P = S ? "outside-to-node" : D.value,
				T = e.pstyle("target-endpoint"),
				M = S ? "outside-to-node" : T.value;
			if (y.srcManEndpt = D, y.tgtManEndpt = T, x) {
				var B = [y.ctrlpts[0], y.ctrlpts[1]];
				n = w ? [y.ctrlpts[y.ctrlpts.length - 2], y.ctrlpts[y.ctrlpts.length - 1]] : B, r = B
			} else if (E) {
				var _ = k ? y.segpts.slice(0, 2) : [c.x, c.y];
				n = k ? y.segpts.slice(y.segpts.length - 2) : [u.x, u.y], r = _
			}
			if ("inside-to-node" === M) t = [c.x, c.y];
			else if (T.units) t = this.manualEndptToPx(l, T);
			else if ("outside-to-line" === M) t = y.tgtIntn;
			else if ("outside-to-node" === M || "outside-to-node-or-label" === M ? i = n : "outside-to-line" !== M && "outside-to-line-or-label" !== M || (i = [u.x, u.y]), t = o.nodeShapes[this.getNodeShape(l)].intersectLine(c.x, c.y, l.outerWidth(), l.outerHeight(), i[0], i[1], 0), "outside-to-node-or-label" === M || "outside-to-line-or-label" === M) {
				var N = l._private.rscratch,
					I = N.labelWidth,
					z = N.labelHeight,
					L = N.labelX,
					A = N.labelY,
					O = l.pstyle("text-valign").value;
				"top" === O ? A -= z / 2 : "bottom" === O && (A += z / 2);
				var R = l.pstyle("text-halign").value;
				"left" === R ? L -= I / 2 : "right" === R && (L += I / 2);
				var V = o.nodeShapes.rectangle.intersectLine(L, A, I, z, i[0], i[1], 0),
					F = u,
					q = lt(F, rt(t));
				lt(F, rt(V)) < q && (t = V)
			}
			var Y = Lt(t, n, o.arrowShapes[h].spacing(e) + p),
				j = Lt(t, n, o.arrowShapes[h].gap(e) + p);
			if (y.endX = j[0], y.endY = j[1], y.arrowEndX = Y[0], y.arrowEndY = Y[1], "inside-to-node" === P) t = [u.x, u.y];
			else if (D.units) t = this.manualEndptToPx(s, D);
			else if ("outside-to-line" === P) t = y.srcIntn;
			else if ("outside-to-node" === P || "outside-to-node-or-label" === P ? a = r : "outside-to-line" !== P && "outside-to-line-or-label" !== P || (a = [c.x, c.y]), t = o.nodeShapes[this.getNodeShape(s)].intersectLine(u.x, u.y, s.outerWidth(), s.outerHeight(), a[0], a[1], 0), "outside-to-node-or-label" === P || "outside-to-line-or-label" === P) {
				var X = s._private.rscratch,
					W = X.labelWidth,
					H = X.labelHeight,
					K = X.labelX,
					G = X.labelY,
					Z = s.pstyle("text-valign").value;
				"top" === Z ? G -= H / 2 : "bottom" === Z && (G += H / 2);
				var U = s.pstyle("text-halign").value;
				"left" === U ? K -= W / 2 : "right" === U && (K += W / 2);
				var $ = o.nodeShapes.rectangle.intersectLine(K, G, W, H, a[0], a[1], 0),
					Q = c,
					J = lt(Q, rt(t));
				lt(Q, rt($)) < J && (t = $)
			}
			var ee = Lt(t, r, o.arrowShapes[d].spacing(e) + f),
				te = Lt(t, r, o.arrowShapes[d].gap(e) + f);
			y.startX = te[0], y.startY = te[1], y.arrowStartX = ee[0], y.arrowStartY = ee[1], C && (v(y.startX) && v(y.startY) && v(y.endX) && v(y.endY) ? y.badLine = !1 : y.badLine = !0)
		},
		getSourceEndpoint: function (e) {
			var t = e[0]._private.rscratch;
			switch (this.recalculateRenderedStyle(e), t.edgeType) {
				case "haystack":
					return {
						x: t.haystackPts[0],
						y: t.haystackPts[1]
					};
				default:
					return {
						x: t.arrowStartX,
						y: t.arrowStartY
					}
			}
		},
		getTargetEndpoint: function (e) {
			var t = e[0]._private.rscratch;
			switch (this.recalculateRenderedStyle(e), t.edgeType) {
				case "haystack":
					return {
						x: t.haystackPts[2],
						y: t.haystackPts[3]
					};
				default:
					return {
						x: t.arrowEndX,
						y: t.arrowEndY
					}
			}
		}
	},
		jo = {};

	function Xo(e, t, n) {
		for (var r = function (e, t, n, r) {
			return ct(e, t, n, r)
		}, i = t._private.rstyle.bezierPts, a = 0; a < e.bezierProjPcts.length; a++) {
			var o = e.bezierProjPcts[a];
			i.push({
				x: r(n[0], n[2], n[4], o),
				y: r(n[1], n[3], n[5], o)
			})
		}
	}
	jo.storeEdgeProjections = function (e) {
		var t = e._private,
			n = t.rscratch,
			r = n.edgeType;
		if (t.rstyle.bezierPts = null, t.rstyle.linePts = null, t.rstyle.haystackPts = null, "multibezier" === r || "bezier" === r || "self" === r || "compound" === r) {
			t.rstyle.bezierPts = [];
			for (var i = 0; i + 5 < n.allpts.length; i += 4) Xo(this, e, n.allpts.slice(i, i + 6))
		} else if ("segments" === r) {
			var a = t.rstyle.linePts = [];
			for (i = 0; i + 1 < n.allpts.length; i += 2) a.push({
				x: n.allpts[i],
				y: n.allpts[i + 1]
			})
		} else if ("haystack" === r) {
			var o = n.haystackPts;
			t.rstyle.haystackPts = [{
				x: o[0],
				y: o[1]
			}, {
				x: o[2],
				y: o[3]
			}]
		}
		t.rstyle.arrowWidth = this.getArrowWidth(e.pstyle("width").pfValue, e.pstyle("arrow-scale").value) * this.arrowShapeWidth
	}, jo.recalculateEdgeProjections = function (e) {
		this.findEdgeControlPoints(e)
	};
	var Wo = {
		recalculateNodeLabelProjection: function (e) {
			var t = e.pstyle("label").strValue;
			if (!k(t)) {
				var n, r, i = e._private,
					a = e.width(),
					o = e.height(),
					s = e.padding(),
					l = e.position(),
					u = e.pstyle("text-halign").strValue,
					c = e.pstyle("text-valign").strValue,
					h = i.rscratch,
					d = i.rstyle;
				switch (u) {
					case "left":
						n = l.x - a / 2 - s;
						break;
					case "right":
						n = l.x + a / 2 + s;
						break;
					default:
						n = l.x
				}
				switch (c) {
					case "top":
						r = l.y - o / 2 - s;
						break;
					case "bottom":
						r = l.y + o / 2 + s;
						break;
					default:
						r = l.y
				}
				h.labelX = n, h.labelY = r, d.labelX = n, d.labelY = r, this.applyLabelDimensions(e)
			}
		}
	},
		Ho = function (e, t) {
			var n = Math.atan(t / e);
			return 0 === e && n < 0 && (n *= -1), n
		},
		Ko = function (e, t) {
			var n = t.x - e.x,
				r = t.y - e.y;
			return Ho(n, r)
		};
	Wo.recalculateEdgeLabelProjections = function (e) {
		var t, n = e._private,
			r = n.rscratch,
			i = this,
			a = {
				mid: e.pstyle("label").strValue,
				source: e.pstyle("source-label").strValue,
				target: e.pstyle("target-label").strValue
			};
		if (a.mid || a.source || a.target) {
			t = {
				x: r.midX,
				y: r.midY
			};
			var o = function (e, t, r) {
				Ie(n.rscratch, e, t, r), Ie(n.rstyle, e, t, r)
			};
			o("labelX", null, t.x), o("labelY", null, t.y);
			var s = Ho(r.midDispX, r.midDispY);
			o("labelAutoAngle", null, s);
			var l = function (s) {
				var l, u = "source" === s;
				if (a[s]) {
					var c = e.pstyle(s + "-text-offset").pfValue;
					switch (r.edgeType) {
						case "self":
						case "compound":
						case "bezier":
						case "multibezier":
							for (var h, d = function e() {
								if (e.cache) return e.cache;
								for (var t = [], a = 0; a + 5 < r.allpts.length; a += 4) {
									var o = {
										x: r.allpts[a],
										y: r.allpts[a + 1]
									},
										s = {
											x: r.allpts[a + 2],
											y: r.allpts[a + 3]
										},
										l = {
											x: r.allpts[a + 4],
											y: r.allpts[a + 5]
										};
									t.push({
										p0: o,
										p1: s,
										p2: l,
										startDist: 0,
										length: 0,
										segments: []
									})
								}
								var u = n.rstyle.bezierPts,
									c = i.bezierProjPcts.length;

								function h(e, t, n, r, i) {
									var a = st(t, n),
										o = e.segments[e.segments.length - 1],
										s = {
											p0: t,
											p1: n,
											t0: r,
											t1: i,
											startDist: o ? o.startDist + o.length : 0,
											length: a
										};
									e.segments.push(s), e.length += a
								}
								for (var d = 0; d < t.length; d++) {
									var p = t[d],
										f = t[d - 1];
									f && (p.startDist = f.startDist + f.length), h(p, p.p0, u[d * c], 0, i.bezierProjPcts[0]);
									for (var g = 0; g < c - 1; g++) h(p, u[d * c + g], u[d * c + g + 1], i.bezierProjPcts[g], i.bezierProjPcts[g + 1]);
									h(p, u[d * c + c - 1], p.p2, i.bezierProjPcts[c - 1], 1)
								}
								return e.cache = t
							}(), p = 0, f = 0, g = 0; g < d.length; g++) {
								for (var v = d[u ? g : d.length - 1 - g], y = 0; y < v.segments.length; y++) {
									var m = v.segments[u ? y : v.segments.length - 1 - y],
										b = g === d.length - 1 && y === v.segments.length - 1;
									if (p = f, (f += m.length) >= c || b) {
										h = {
											cp: v,
											segment: m
										};
										break
									}
								}
								if (h) break
							}
							var x = h.cp,
								w = h.segment,
								E = (c - p) / w.length,
								k = w.t1 - w.t0,
								C = u ? w.t0 + k * E : w.t1 - k * E;
							C = dt(0, C, 1), t = ht(x.p0, x.p1, x.p2, C), l = function (e, t, n, r) {
								var i = dt(0, r - .001, 1),
									a = dt(0, r + .001, 1),
									o = ht(e, t, n, i),
									s = ht(e, t, n, a);
								return Ko(o, s)
							}(x.p0, x.p1, x.p2, C);
							break;
						case "straight":
						case "segments":
						case "haystack":
							for (var S, D, P, T, M = 0, B = r.allpts.length, _ = 0; _ + 3 < B && (u ? (P = {
								x: r.allpts[_],
								y: r.allpts[_ + 1]
							}, T = {
								x: r.allpts[_ + 2],
								y: r.allpts[_ + 3]
							}) : (P = {
								x: r.allpts[B - 2 - _],
								y: r.allpts[B - 1 - _]
							}, T = {
								x: r.allpts[B - 4 - _],
								y: r.allpts[B - 3 - _]
							}), D = M, !((M += S = st(P, T)) >= c)); _ += 2);
							var N = (c - D) / S;
							N = dt(0, N, 1), t = function (e, t, n, r) {
								var i = t.x - e.x,
									a = t.y - e.y,
									o = st(e, t),
									s = i / o,
									l = a / o;
								return n = null == n ? 0 : n, r = null != r ? r : n * o, {
									x: e.x + s * r,
									y: e.y + l * r
								}
							}(P, T, N), l = Ko(P, T)
					}
					o("labelX", s, t.x), o("labelY", s, t.y), o("labelAutoAngle", s, l)
				}
			};
			l("source"), l("target"), this.applyLabelDimensions(e)
		}
	}, Wo.applyLabelDimensions = function (e) {
		this.applyPrefixedLabelDimensions(e), e.isEdge() && (this.applyPrefixedLabelDimensions(e, "source"), this.applyPrefixedLabelDimensions(e, "target"))
	}, Wo.applyPrefixedLabelDimensions = function (e, t) {
		var n = e._private,
			r = this.getLabelText(e, t),
			i = this.calculateLabelDimensions(e, r),
			a = e.pstyle("line-height").pfValue,
			o = e.pstyle("text-wrap").strValue,
			s = Ne(n.rscratch, "labelWrapCachedLines", t) || [],
			l = "wrap" !== o ? 1 : Math.max(s.length, 1),
			u = i.height / l,
			c = u * a,
			h = i.width,
			d = i.height + (l - 1) * (a - 1) * u;
		Ie(n.rstyle, "labelWidth", t, h), Ie(n.rscratch, "labelWidth", t, h), Ie(n.rstyle, "labelHeight", t, d), Ie(n.rscratch, "labelHeight", t, d), Ie(n.rscratch, "labelLineHeight", t, c)
	}, Wo.getLabelText = function (e, t) {
		var n = e._private,
			r = t ? t + "-" : "",
			i = e.pstyle(r + "label").strValue,
			a = e.pstyle("text-transform").value,
			o = function (e, r) {
				return r ? (Ie(n.rscratch, e, t, r), r) : Ne(n.rscratch, e, t)
			};
		if (!i) return "";
		"none" == a || ("uppercase" == a ? i = i.toUpperCase() : "lowercase" == a && (i = i.toLowerCase()));
		var s = e.pstyle("text-wrap").value;
		if ("wrap" === s) {
			var l = o("labelKey");
			if (null != l && o("labelWrapKey") === l) return o("labelWrapCachedText");
			for (var u = i.split("\n"), c = e.pstyle("text-max-width").pfValue, h = "anywhere" === e.pstyle("text-overflow-wrap").value, d = [], p = /[\s\u200b]+/, f = h ? "" : " ", g = 0; g < u.length; g++) {
				var v = u[g],
					y = this.calculateLabelDimensions(e, v).width;
				if (h) {
					var m = v.split("").join("​");
					v = m
				}
				if (y > c) {
					for (var b = v.split(p), x = "", w = 0; w < b.length; w++) {
						var E = b[w],
							k = 0 === x.length ? E : x + f + E;
						this.calculateLabelDimensions(e, k).width <= c ? x += E + f : (d.push(x), x = E + f)
					}
					x.match(/^[\s\u200b]+$/) || d.push(x)
				} else d.push(v)
			}
			o("labelWrapCachedLines", d), i = o("labelWrapCachedText", d.join("\n")), o("labelWrapKey", l)
		} else if ("ellipsis" === s) {
			for (var C = e.pstyle("text-max-width").pfValue, S = "", D = !1, P = 0; P < i.length; P++) {
				if (this.calculateLabelDimensions(e, S + i[P] + "…").width > C) break;
				S += i[P], P === i.length - 1 && (D = !0)
			}
			return D || (S += "…"), S
		}
		return i
	}, Wo.getLabelJustification = function (e) {
		var t = e.pstyle("text-justification").strValue,
			n = e.pstyle("text-halign").strValue;
		if ("auto" !== t) return t;
		if (!e.isNode()) return "center";
		switch (n) {
			case "left":
				return "right";
			case "right":
				return "left";
			default:
				return "center"
		}
	}, Wo.calculateLabelDimensions = function (e, t) {
		var n = he(t, e._private.labelDimsKey),
			r = this.labelDimCache || (this.labelDimCache = []),
			i = r[n];
		if (null != i) return i;
		var a = e.pstyle("font-style").strValue,
			o = 1 * e.pstyle("font-size").pfValue + "px",
			s = e.pstyle("font-family").strValue,
			l = e.pstyle("font-weight").strValue,
			u = this.labelCalcDiv;
		u || (u = this.labelCalcDiv = document.createElement("div"), document.body.appendChild(u));
		var c = u.style;
		return c.fontFamily = s, c.fontStyle = a, c.fontSize = o, c.fontWeight = l, c.position = "absolute", c.left = "-9999px", c.top = "-9999px", c.zIndex = "-1", c.visibility = "hidden", c.pointerEvents = "none", c.padding = "0", c.lineHeight = "1", "wrap" === e.pstyle("text-wrap").value ? c.whiteSpace = "pre" : c.whiteSpace = "normal", u.textContent = t, r[n] = {
			width: Math.ceil(u.clientWidth / 1),
			height: Math.ceil(u.clientHeight / 1)
		}
	}, Wo.calculateLabelAngle = function (e, t) {
		var n = e._private.rscratch,
			r = e.isEdge(),
			i = t ? t + "-" : "",
			a = e.pstyle(i + "text-rotation"),
			o = a.strValue;
		return "none" === o ? 0 : r && "autorotate" === o ? n.labelAutoAngle : "autorotate" === o ? 0 : a.pfValue
	}, Wo.calculateLabelAngles = function (e) {
		var t = this,
			n = e.isEdge(),
			r = e._private.rscratch;
		r.labelAngle = t.calculateLabelAngle(e), n && (r.sourceLabelAngle = t.calculateLabelAngle(e, "source"), r.targetLabelAngle = t.calculateLabelAngle(e, "target"))
	};
	var Go = {},
		Zo = !1;
	Go.getNodeShape = function (e) {
		var t = e.pstyle("shape").value;
		if ("cutrectangle" === t && (e.width() < 28 || e.height() < 28)) return Zo || (Ce("The `cutrectangle` node shape can not be used at small sizes so `rectangle` is used instead"), Zo = !0), "rectangle";
		if (e.isParent()) return "rectangle" === t || "roundrectangle" === t || "cutrectangle" === t || "barrel" === t ? t : "rectangle";
		if ("polygon" === t) {
			var n = e.pstyle("shape-polygon-points").value;
			return this.nodeShapes.makePolygon(n).name
		}
		return t
	};
	var Uo = {
		registerCalculationListeners: function () {
			var e = this.cy,
				t = e.collection(),
				n = this,
				r = function (e) {
					var n = !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1];
					if (t.merge(e), n)
						for (var r = 0; r < e.length; r++) {
							var i = e[r]._private.rstyle;
							i.clean = !1, i.cleanConnected = !1
						}
				};
			n.binder(e).on("bounds.* dirty.*", function (e) {
				var t = e.target;
				r(t)
			}).on("style.* background.*", function (e) {
				var t = e.target;
				r(t, !1)
			});
			var i = function (i) {
				if (i) {
					for (var a = n.onUpdateEleCalcsFns, o = 0; o < t.length; o++) {
						var s = t[o],
							l = s._private.rstyle;
						s.isNode() && !l.cleanConnected && (r(s.connectedEdges()), l.cleanConnected = !0)
					}
					if (a)
						for (o = 0; o < a.length; o++) {
							(0, a[o])(i, t)
						}
					n.recalculateRenderedStyle(t), t = e.collection()
				}
			};
			n.flushRenderedStyleQueue = function () {
				i(!0)
			}, n.beforeRender(i, n.beforeRenderPriorities.eleCalcs)
		},
		onUpdateEleCalcs: function (e) {
			(this.onUpdateEleCalcsFns = this.onUpdateEleCalcsFns || []).push(e)
		},
		recalculateRenderedStyle: function (e, t) {
			var n = [],
				r = [];
			if (!this.destroyed) {
				void 0 === t && (t = !0);
				for (var i = 0; i < e.length; i++) {
					var a = (l = (s = e[i])._private).rstyle;
					t && a.clean || s.removed() || "none" !== s.pstyle("display").value && ("nodes" === l.group ? r.push(s) : n.push(s), a.clean = !0)
				}
				for (i = 0; i < r.length; i++) {
					a = (l = (s = r[i])._private).rstyle;
					var o = s.position();
					this.recalculateNodeLabelProjection(s), a.nodeX = o.x, a.nodeY = o.y, a.nodeW = s.pstyle("width").pfValue, a.nodeH = s.pstyle("height").pfValue
				}
				this.recalculateEdgeProjections(n);
				for (i = 0; i < n.length; i++) {
					a = (l = (s = n[i])._private).rstyle;
					var s, l, u = l.rscratch;
					a.srcX = u.arrowStartX, a.srcY = u.arrowStartY, a.tgtX = u.arrowEndX, a.tgtY = u.arrowEndY, a.midX = u.midX, a.midY = u.midY, a.labelAngle = u.labelAngle, a.sourceLabelAngle = u.sourceLabelAngle, a.targetLabelAngle = u.targetLabelAngle
				}
			}
		}
	},
		$o = {
			updateCachedGrabbedEles: function () {
				var e = this.cachedZSortedEles;
				if (e) {
					e.drag = [], e.nondrag = [];
					for (var t = [], n = 0; n < e.length; n++) {
						var r = (i = e[n])._private.rscratch;
						i.grabbed() && !i.isParent() ? t.push(i) : r.inDragLayer ? e.drag.push(i) : e.nondrag.push(i)
					}
					for (n = 0; n < t.length; n++) {
						var i = t[n];
						e.drag.push(i)
					}
				}
			},
			invalidateCachedZSortedEles: function () {
				this.cachedZSortedEles = null
			},
			getCachedZSortedEles: function (e) {
				if (e || !this.cachedZSortedEles) {
					var t = this.cy.mutableElements().toArray();
					t.sort(Ri), t.interactive = t.filter(function (e) {
						return e.interactive()
					}), this.cachedZSortedEles = t, this.updateCachedGrabbedEles()
				} else t = this.cachedZSortedEles;
				return t
			}
		},
		Qo = {};
	[Ro, Vo, Fo, Yo, jo, Wo, Go, Uo, $o].forEach(function (e) {
		I(Qo, e)
	});
	var Jo = {
		getCachedImage: function (e, t, n) {
			var r = this.imageCache = this.imageCache || {},
				i = r[e];
			if (i) return i.image.complete || i.image.addEventListener("load", n), i.image;
			var a = (i = r[e] = r[e] || {}).image = new Image;
			a.addEventListener("load", n), a.addEventListener("error", function () {
				a.error = !0
			});
			return "data:" === e.substring(0, "data:".length).toLowerCase() || (a.crossOrigin = t), a.src = e, a
		}
	},
		es = {
			registerBinding: function (e, t, n, r) {
				var i = Array.prototype.slice.apply(arguments, [1]),
					a = this.binder(e);
				return a.on.apply(a, i)
			}
		};
	es.binder = function (e) {
		var t, n = this,
			r = e === window || e === document || e === document.body || (t = e, "undefined" != typeof HTMLElement && t instanceof HTMLElement);
		if (null == n.supportsPassiveEvents) {
			var i = !1;
			try {
				var a = Object.defineProperty({}, "passive", {
					get: function () {
						return i = !0, !0
					}
				});
				window.addEventListener("test", null, a)
			} catch (e) { }
			n.supportsPassiveEvents = i
		}
		var o = function (t, i, a) {
			var o = Array.prototype.slice.call(arguments);
			return r && n.supportsPassiveEvents && (o[2] = {
				capture: null != a && a,
				passive: !1,
				once: !1
			}), n.bindings.push({
				target: e,
				args: o
			}), (e.addEventListener || e.on).apply(e, o), this
		};
		return {
			on: o,
			addEventListener: o,
			addListener: o,
			bind: o
		}
	}, es.nodeIsDraggable = function (e) {
		return e && e.isNode() && !e.locked() && e.grabbable()
	}, es.nodeIsGrabbable = function (e) {
		return this.nodeIsDraggable(e) && e.interactive()
	}, es.load = function () {
		var e = this,
			t = function (e) {
				return e.selected()
			},
			n = function (t, n, r, i) {
				null == t && (t = e.cy);
				for (var a = 0; a < n.length; a++) {
					var o = n[a];
					t.emit({
						originalEvent: r,
						type: o,
						position: i
					})
				}
			},
			r = function (e) {
				return e.shiftKey || e.metaKey || e.ctrlKey
			},
			i = function (t, n) {
				var r = !0;
				if (e.cy.hasCompoundNodes() && t && t.pannable())
					for (var i = 0; n && i < n.length; i++) {
						if ((t = n[i]).isNode() && t.isParent()) {
							r = !1;
							break
						}
					} else r = !0;
				return r
			},
			a = function (e) {
				e[0]._private.rscratch.inDragLayer = !0
			},
			o = function (e) {
				e[0]._private.rscratch.isGrabTarget = !0
			},
			s = function (e, t) {
				var n = t.addToList;
				n.has(e) || (n.merge(e), function (e) {
					e[0]._private.grabbed = !0
				}(e))
			},
			l = function (t, n) {
				n = n || {};
				var r = t.cy().hasCompoundNodes();
				n.inDragLayer && (t.forEach(a), t.neighborhood().stdFilter(function (e) {
					return !r || e.isEdge()
				}).forEach(a)), n.addToList && t.forEach(function (e) {
					s(e, n)
				}),
					function (e, t) {
						if (e.cy().hasCompoundNodes() && (null != t.inDragLayer || null != t.addToList)) {
							var n = e.descendants();
							t.inDragLayer && (n.forEach(a), n.connectedEdges().forEach(a)), t.addToList && t.addToList.unmerge(n)
						}
					}(t, n), h(t, {
						inDragLayer: n.inDragLayer
					}), e.updateCachedGrabbedEles()
			},
			u = l,
			c = function (t) {
				t && (e.getCachedZSortedEles().forEach(function (e) {
					! function (e) {
						e[0]._private.grabbed = !1
					}(e),
						function (e) {
							e[0]._private.rscratch.inDragLayer = !1
						}(e),
						function (e) {
							e[0]._private.rscratch.isGrabTarget = !1
						}(e)
				}), e.updateCachedGrabbedEles())
			},
			h = function (e, t) {
				if ((null != t.inDragLayer || null != t.addToList) && e.cy().hasCompoundNodes()) {
					var n = e.ancestors().orphans();
					if (!n.same(e)) {
						var r = n.descendants().spawnSelf().merge(n).unmerge(e).unmerge(e.descendants()),
							i = r.connectedEdges();
						t.inDragLayer && (i.forEach(a), r.forEach(a)), t.addToList && r.forEach(function (e) {
							s(e, t)
						})
					}
				}
			},
			d = function () {
				null != document.activeElement && null != document.activeElement.blur && document.activeElement.blur()
			},
			p = "undefined" != typeof MutationObserver;
		p ? (e.removeObserver = new MutationObserver(function (t) {
			for (var n = 0; n < t.length; n++) {
				var r = t[n].removedNodes;
				if (r)
					for (var i = 0; i < r.length; i++) {
						if (r[i] === e.container) {
							e.destroy();
							break
						}
					}
			}
		}), e.container.parentNode && e.removeObserver.observe(e.container.parentNode, {
			childList: !0
		})) : e.registerBinding(e.container, "DOMNodeRemoved", function (t) {
			e.destroy()
		});
		var f = ne(function () {
			e.cy.resize()
		}, 100);
		p && (e.styleObserver = new MutationObserver(f), e.styleObserver.observe(e.container, {
			attributes: !0
		})), e.registerBinding(window, "resize", f);
		var g = function () {
			e.invalidateContainerClientCoordsCache()
		};
		! function (e, t) {
			for (; null != e;) t(e), e = e.parentNode
		}(e.container, function (t) {
			e.registerBinding(t, "transitionend", g), e.registerBinding(t, "animationend", g), e.registerBinding(t, "scroll", g)
		}), e.registerBinding(e.container, "contextmenu", function (e) {
			e.preventDefault()
		});
		var y = function (t) {
			try {
				for (var n = e.findContainerClientCoords(), r = n[0], i = n[1], a = n[2], o = n[3], s = t.touches ? t.touches : [t], l = !1, u = 0; u < s.length; u++) {
					var c = s[u];
					if (r <= c.clientX && c.clientX <= r + a && i <= c.clientY && c.clientY <= i + o) {
						l = !0;
						break
					}
				}
				if (!l) return !1;
				for (var h = e.container, d = t.target.parentNode, p = !1; d;) {
					if (d === h) {
						p = !0;
						break
					}
					d = d.parentNode
				}
				return !!p
			} catch (e) {
				return null;
			}
			
		};
		e.registerBinding(e.container, "mousedown", function (t) {
			if (y(t)) {
				t.preventDefault(), d(), e.hoverData.capture = !0, e.hoverData.which = t.which;
				var r = e.cy,
					i = [t.clientX, t.clientY],
					a = e.projectIntoViewport(i[0], i[1]),
					s = e.selection,
					c = e.findNearestElements(a[0], a[1], !0, !1),
					h = c[0],
					p = e.dragData.possibleDragElements;
				e.hoverData.mdownPos = a, e.hoverData.mdownGPos = i;
				if (3 == t.which) {
					e.hoverData.cxtStarted = !0;
					var f = {
						originalEvent: t,
						type: "cxttapstart",
						position: {
							x: a[0],
							y: a[1]
						}
					};
					h ? (h.activate(), h.emit(f), e.hoverData.down = h) : r.emit(f), e.hoverData.downTime = (new Date).getTime(), e.hoverData.cxtDragged = !1
				} else if (1 == t.which) {
					if (h && h.activate(), null != h && e.nodeIsGrabbable(h)) {
						var g = function (e) {
							return {
								originalEvent: t,
								type: e,
								position: {
									x: a[0],
									y: a[1]
								}
							}
						};
						if (o(h), h.selected()) {
							p = e.dragData.possibleDragElements = r.collection();
							var v = r.$(function (t) {
								return t.isNode() && t.selected() && e.nodeIsGrabbable(t)
							});
							l(v, {
								addToList: p
							}), h.emit(g("grabon")), v.forEach(function (e) {
								e.emit(g("grab"))
							})
						} else p = e.dragData.possibleDragElements = r.collection(), u(h, {
							addToList: p
						}), h.emit(g("grabon")).emit(g("grab"));
						e.redrawHint("eles", !0), e.redrawHint("drag", !0)
					}
					e.hoverData.down = h, e.hoverData.downs = c, e.hoverData.downTime = (new Date).getTime(), n(h, ["mousedown", "tapstart", "vmousedown"], t, {
						x: a[0],
						y: a[1]
					}), null == h ? (s[4] = 1, e.data.bgActivePosistion = {
						x: a[0],
						y: a[1]
					}, e.redrawHint("select", !0), e.redraw()) : h.pannable() && (s[4] = 1), e.hoverData.tapholdCancelled = !1, clearTimeout(e.hoverData.tapholdTimeout), e.hoverData.tapholdTimeout = setTimeout(function () {
						if (!e.hoverData.tapholdCancelled) {
							var n = e.hoverData.down;
							n ? n.emit({
								originalEvent: t,
								type: "taphold",
								position: {
									x: a[0],
									y: a[1]
								}
							}) : r.emit({
								originalEvent: t,
								type: "taphold",
								position: {
									x: a[0],
									y: a[1]
								}
							})
						}
					}, e.tapholdDuration)
				}
				s[0] = s[2] = a[0], s[1] = s[3] = a[1]
			}
		}, !1), e.registerBinding(window, "mousemove", function (t) {
			if (e.hoverData.capture || y(t)) {
				var a = !1,
					o = e.cy,
					s = o.zoom(),
					u = [t.clientX, t.clientY],
					h = e.projectIntoViewport(u[0], u[1]),
					d = e.hoverData.mdownPos,
					p = e.hoverData.mdownGPos,
					f = e.selection,
					g = null;
				e.hoverData.draggingEles || e.hoverData.dragging || e.hoverData.selecting || (g = e.findNearestElement(h[0], h[1], !0, !1));
				var m, b = e.hoverData.last,
					x = e.hoverData.down,
					w = [h[0] - f[2], h[1] - f[3]],
					E = e.dragData.possibleDragElements;
				if (p) {
					var k = u[0] - p[0],
						C = k * k,
						S = u[1] - p[1],
						D = C + S * S;
					e.hoverData.isOverThresholdDrag = m = D >= e.desktopTapThreshold2
				}
				var P = r(t);
				m && (e.hoverData.tapholdCancelled = !0);
				a = !0, n(g, ["mousemove", "vmousemove", "tapdrag"], t, {
					x: h[0],
					y: h[1]
				});
				var T = function () {
					e.data.bgActivePosistion = void 0, e.hoverData.selecting || o.emit({
						originalEvent: t,
						type: "boxstart",
						position: {
							x: h[0],
							y: h[1]
						}
					}), f[4] = 1, e.hoverData.selecting = !0, e.redrawHint("select", !0), e.redraw()
				};
				if (3 === e.hoverData.which) {
					if (m) {
						var M = {
							originalEvent: t,
							type: "cxtdrag",
							position: {
								x: h[0],
								y: h[1]
							}
						};
						x ? x.emit(M) : o.emit(M), e.hoverData.cxtDragged = !0, e.hoverData.cxtOver && g === e.hoverData.cxtOver || (e.hoverData.cxtOver && e.hoverData.cxtOver.emit({
							originalEvent: t,
							type: "cxtdragout",
							position: {
								x: h[0],
								y: h[1]
							}
						}), e.hoverData.cxtOver = g, g && g.emit({
							originalEvent: t,
							type: "cxtdragover",
							position: {
								x: h[0],
								y: h[1]
							}
						}))
					}
				} else if (e.hoverData.dragging) {
					if (a = !0, o.panningEnabled() && o.userPanningEnabled()) {
						var B;
						if (e.hoverData.justStartedPan) {
							var _ = e.hoverData.mdownPos;
							B = {
								x: (h[0] - _[0]) * s,
								y: (h[1] - _[1]) * s
							}, e.hoverData.justStartedPan = !1
						} else B = {
							x: w[0] * s,
							y: w[1] * s
						};
						o.panBy(B), e.hoverData.dragged = !0
					}
					h = e.projectIntoViewport(t.clientX, t.clientY)
				} else if (1 != f[4] || null != x && !x.pannable()) {
					if (x && x.pannable() && x.active() && x.unactivate(), x && x.grabbed() || g == b || (b && n(b, ["mouseout", "tapdragout"], t, {
						x: h[0],
						y: h[1]
					}), g && n(g, ["mouseover", "tapdragover"], t, {
						x: h[0],
						y: h[1]
					}), e.hoverData.last = g), x)
						if (m) {
							if (o.boxSelectionEnabled() && P) x && x.grabbed() && (c(E), x.emit("freeon"), E.emit("free"), e.dragData.didDrag && (x.emit("dragfreeon"), E.emit("dragfree"))), T();
							else if (x && x.grabbed() && e.nodeIsDraggable(x)) {
								var N = !e.dragData.didDrag;
								N && e.redrawHint("eles", !0), e.dragData.didDrag = !0;
								var I = o.collection();
								e.hoverData.draggingEles || l(E, {
									inDragLayer: !0
								});
								var z = {
									x: 0,
									y: 0
								};
								if (v(w[0]) && v(w[1]) && (z.x += w[0], z.y += w[1], N)) {
									var L = e.hoverData.dragDelta;
									L && v(L[0]) && v(L[1]) && (z.x += L[0], z.y += L[1])
								}
								for (var A = 0; A < E.length; A++) {
									var O = E[A];
									e.nodeIsDraggable(O) && O.grabbed() && I.merge(O)
								}
								e.hoverData.draggingEles = !0, I.silentShift(z).emit("position drag"), e.redrawHint("drag", !0), e.redraw()
							}
						} else ! function () {
							var t = e.hoverData.dragDelta = e.hoverData.dragDelta || [];
							0 === t.length ? (t.push(w[0]), t.push(w[1])) : (t[0] += w[0], t[1] += w[1])
						}();
					a = !0
				} else if (m) {
					if (e.hoverData.dragging || !o.boxSelectionEnabled() || !P && o.panningEnabled() && o.userPanningEnabled()) {
						if (!e.hoverData.selecting && o.panningEnabled() && o.userPanningEnabled()) {
							i(x, e.hoverData.downs) && (e.hoverData.dragging = !0, e.hoverData.justStartedPan = !0, f[4] = 0, e.data.bgActivePosistion = rt(d), e.redrawHint("select", !0), e.redraw())
						}
					} else T();
					x && x.pannable() && x.active() && x.unactivate()
				}
				return f[2] = h[0], f[3] = h[1], a ? (t.stopPropagation && t.stopPropagation(), t.preventDefault && t.preventDefault(), !1) : void 0
			}
		}, !1), e.registerBinding(window, "mouseup", function (i) {
			if (e.hoverData.capture) {
				e.hoverData.capture = !1;
				var a = e.cy,
					o = e.projectIntoViewport(i.clientX, i.clientY),
					s = e.selection,
					l = e.findNearestElement(o[0], o[1], !0, !1),
					u = e.dragData.possibleDragElements,
					h = e.hoverData.down,
					d = r(i);
				if (e.data.bgActivePosistion && (e.redrawHint("select", !0), e.redraw()), e.hoverData.tapholdCancelled = !0, e.data.bgActivePosistion = void 0, h && h.unactivate(), 3 === e.hoverData.which) {
					var p = {
						originalEvent: i,
						type: "cxttapend",
						position: {
							x: o[0],
							y: o[1]
						}
					};
					if (h ? h.emit(p) : a.emit(p), !e.hoverData.cxtDragged) {
						var f = {
							originalEvent: i,
							type: "cxttap",
							position: {
								x: o[0],
								y: o[1]
							}
						};
						h ? h.emit(f) : a.emit(f)
					}
					e.hoverData.cxtDragged = !1, e.hoverData.which = null
				} else if (1 === e.hoverData.which) {
					if (n(l, ["mouseup", "tapend", "vmouseup"], i, {
						x: o[0],
						y: o[1]
					}), e.dragData.didDrag || e.hoverData.dragged || e.hoverData.selecting || e.hoverData.isOverThresholdDrag || n(h, ["click", "tap", "vclick"], i, {
						x: o[0],
						y: o[1]
					}), null != h || e.dragData.didDrag || e.hoverData.selecting || e.hoverData.dragged || r(i) || (a.$(t).unselect(["tapunselect"]), u.length > 0 && e.redrawHint("eles", !0), e.dragData.possibleDragElements = u = a.collection()), l != h || e.dragData.didDrag || e.hoverData.selecting || null != l && l._private.selectable && (e.hoverData.dragging || ("additive" === a.selectionType() || d ? l.selected() ? l.unselect(["tapunselect"]) : l.select(["tapselect"]) : d || (a.$(t).unmerge(l).unselect(["tapunselect"]), l.select(["tapselect"]))), e.redrawHint("eles", !0)), e.hoverData.selecting) {
						var g = a.collection(e.getAllInBox(s[0], s[1], s[2], s[3]));
						e.redrawHint("select", !0), g.length > 0 && e.redrawHint("eles", !0), a.emit({
							type: "boxend",
							originalEvent: i,
							position: {
								x: o[0],
								y: o[1]
							}
						});
						var v = function (e) {
							return e.selectable() && !e.selected()
						};
						"additive" === a.selectionType() ? g.emit("box").stdFilter(v).select().emit("boxselect") : (d || a.$(t).unmerge(g).unselect(), g.emit("box").stdFilter(v).select().emit("boxselect")), e.redraw()
					}
					if (e.hoverData.dragging && (e.hoverData.dragging = !1, e.redrawHint("select", !0), e.redrawHint("eles", !0), e.redraw()), !s[4]) {
						e.redrawHint("drag", !0), e.redrawHint("eles", !0);
						var y = h && h.grabbed();
						c(u), y && (h.emit("freeon"), u.emit("free"), e.dragData.didDrag && (h.emit("dragfreeon"), u.emit("dragfree")))
					}
				}
				s[4] = 0, e.hoverData.down = null, e.hoverData.cxtStarted = !1, e.hoverData.draggingEles = !1, e.hoverData.selecting = !1, e.hoverData.isOverThresholdDrag = !1, e.dragData.didDrag = !1, e.hoverData.dragged = !1, e.hoverData.dragDelta = [], e.hoverData.mdownPos = null, e.hoverData.mdownGPos = null
			}
		}, !1);
		var m, b, x, w, E, k, C, S, D, P, T, M, B;
		e.registerBinding(e.container, "wheel", function (t) {
			if (!e.scrollingPage) {
				var n, r = e.cy,
					i = e.projectIntoViewport(t.clientX, t.clientY),
					a = [i[0] * r.zoom() + r.pan().x, i[1] * r.zoom() + r.pan().y];
				e.hoverData.draggingEles || e.hoverData.dragging || e.hoverData.cxtStarted || 0 !== e.selection[4] ? t.preventDefault() : r.panningEnabled() && r.userPanningEnabled() && r.zoomingEnabled() && r.userZoomingEnabled() && (t.preventDefault(), e.data.wheelZooming = !0, clearTimeout(e.data.wheelTimeout), e.data.wheelTimeout = setTimeout(function () {
					e.data.wheelZooming = !1, e.redrawHint("eles", !0), e.redraw()
				}, 150), n = null != t.deltaY ? t.deltaY / -250 : null != t.wheelDeltaY ? t.wheelDeltaY / 1e3 : t.wheelDelta / 1e3, n *= e.wheelSensitivity, 1 === t.deltaMode && (n *= 33), r.zoom({
					level: r.zoom() * Math.pow(10, n),
					renderedPosition: {
						x: a[0],
						y: a[1]
					}
				}))
			}
		}, !0), e.registerBinding(window, "scroll", function (t) {
			e.scrollingPage = !0, clearTimeout(e.scrollingPageTimeout), e.scrollingPageTimeout = setTimeout(function () {
				e.scrollingPage = !1
			}, 250)
		}, !0), e.registerBinding(e.container, "mouseout", function (t) {
			var n = e.projectIntoViewport(t.clientX, t.clientY);
			e.cy.emit({
				originalEvent: t,
				type: "mouseout",
				position: {
					x: n[0],
					y: n[1]
				}
			})
		}, !1), e.registerBinding(e.container, "mouseover", function (t) {
			var n = e.projectIntoViewport(t.clientX, t.clientY);
			e.cy.emit({
				originalEvent: t,
				type: "mouseover",
				position: {
					x: n[0],
					y: n[1]
				}
			})
		}, !1);
		var _, N, I, z, L = function (e, t, n, r) {
			return Math.sqrt((n - e) * (n - e) + (r - t) * (r - t))
		},
			A = function (e, t, n, r) {
				return (n - e) * (n - e) + (r - t) * (r - t)
			};
		if (e.registerBinding(e.container, "touchstart", _ = function (t) {
			if (y(t)) {
				d(), e.touchData.capture = !0, e.data.bgActivePosistion = void 0;
				var r = e.cy,
					i = e.touchData.now,
					a = e.touchData.earlier;
				if (t.touches[0]) {
					var s = e.projectIntoViewport(t.touches[0].clientX, t.touches[0].clientY);
					i[0] = s[0], i[1] = s[1]
				}
				if (t.touches[1]) {
					s = e.projectIntoViewport(t.touches[1].clientX, t.touches[1].clientY);
					i[2] = s[0], i[3] = s[1]
				}
				if (t.touches[2]) {
					s = e.projectIntoViewport(t.touches[2].clientX, t.touches[2].clientY);
					i[4] = s[0], i[5] = s[1]
				}
				if (t.touches[1]) {
					e.touchData.singleTouchMoved = !0, c(e.dragData.touchDragEles);
					var h = e.findContainerClientCoords();
					D = h[0], P = h[1], T = h[2], M = h[3], m = t.touches[0].clientX - D, b = t.touches[0].clientY - P, x = t.touches[1].clientX - D, w = t.touches[1].clientY - P, B = 0 <= m && m <= T && 0 <= x && x <= T && 0 <= b && b <= M && 0 <= w && w <= M;
					var p = r.pan(),
						f = r.zoom();
					E = L(m, b, x, w), k = A(m, b, x, w), S = [((C = [(m + x) / 2, (b + w) / 2])[0] - p.x) / f, (C[1] - p.y) / f];
					if (k < 4e4 && !t.touches[2]) {
						var g = e.findNearestElement(i[0], i[1], !0, !0),
							v = e.findNearestElement(i[2], i[3], !0, !0);
						return g && g.isNode() ? (g.activate().emit({
							originalEvent: t,
							type: "cxttapstart",
							position: {
								x: i[0],
								y: i[1]
							}
						}), e.touchData.start = g) : v && v.isNode() ? (v.activate().emit({
							originalEvent: t,
							type: "cxttapstart",
							position: {
								x: i[0],
								y: i[1]
							}
						}), e.touchData.start = v) : r.emit({
							originalEvent: t,
							type: "cxttapstart",
							position: {
								x: i[0],
								y: i[1]
							}
						}), e.touchData.start && (e.touchData.start._private.grabbed = !1), e.touchData.cxt = !0, e.touchData.cxtDragged = !1, e.data.bgActivePosistion = void 0, void e.redraw()
					}
				}
				if (t.touches[2]) r.boxSelectionEnabled() && t.preventDefault();
				else if (t.touches[1]);
				else if (t.touches[0]) {
					var _ = e.findNearestElements(i[0], i[1], !0, !0),
						N = _[0];
					if (null != N && (N.activate(), e.touchData.start = N, e.touchData.starts = _, e.nodeIsGrabbable(N))) {
						var I = e.dragData.touchDragEles = r.collection(),
							z = null;
						e.redrawHint("eles", !0), e.redrawHint("drag", !0), N.selected() ? (z = r.$(function (t) {
							return t.selected() && e.nodeIsGrabbable(t)
						}), l(z, {
							addToList: I
						})) : u(N, {
							addToList: I
						}), o(N);
						var O = function (e) {
							return {
								originalEvent: t,
								type: e,
								position: {
									x: i[0],
									y: i[1]
								}
							}
						};
						N.emit(O("grabon")), z ? z.forEach(function (e) {
							e.emit(O("grab"))
						}) : N.emit(O("grab"))
					}
					n(N, ["touchstart", "tapstart", "vmousedown"], t, {
						x: i[0],
						y: i[1]
					}), null == N && (e.data.bgActivePosistion = {
						x: s[0],
						y: s[1]
					}, e.redrawHint("select", !0), e.redraw()), e.touchData.singleTouchMoved = !1, e.touchData.singleTouchStartTime = +new Date, clearTimeout(e.touchData.tapholdTimeout), e.touchData.tapholdTimeout = setTimeout(function () {
						!1 !== e.touchData.singleTouchMoved || e.pinching || e.touchData.selecting || n(e.touchData.start, ["taphold"], t, {
							x: i[0],
							y: i[1]
						})
					}, e.tapholdDuration)
				}
				if (t.touches.length >= 1) {
					for (var R = e.touchData.startPosition = [], V = 0; V < i.length; V++) R[V] = a[V] = i[V];
					var F = t.touches[0];
					e.touchData.startGPosition = [F.clientX, F.clientY]
				}
			}
		}, !1), e.registerBinding(window, "touchmove", N = function (t) {
			var r = e.touchData.capture;
			if (r || y(t)) {
				var a = e.selection,
					o = e.cy,
					s = e.touchData.now,
					u = e.touchData.earlier,
					h = o.zoom();
				if (t.touches[0]) {
					var d = e.projectIntoViewport(t.touches[0].clientX, t.touches[0].clientY);
					s[0] = d[0], s[1] = d[1]
				}
				if (t.touches[1]) {
					d = e.projectIntoViewport(t.touches[1].clientX, t.touches[1].clientY);
					s[2] = d[0], s[3] = d[1]
				}
				if (t.touches[2]) {
					d = e.projectIntoViewport(t.touches[2].clientX, t.touches[2].clientY);
					s[4] = d[0], s[5] = d[1]
				}
				var p, f = e.touchData.startGPosition;
				if (r && t.touches[0] && f) {
					for (var g = [], C = 0; C < s.length; C++) g[C] = s[C] - u[C];
					var T = t.touches[0].clientX - f[0],
						M = T * T,
						_ = t.touches[0].clientY - f[1];
					p = M + _ * _ >= e.touchTapThreshold2
				}
				if (r && e.touchData.cxt) {
					t.preventDefault();
					var N = t.touches[0].clientX - D,
						I = t.touches[0].clientY - P,
						z = t.touches[1].clientX - D,
						O = t.touches[1].clientY - P,
						R = A(N, I, z, O);
					if (R / k >= 2.25 || R >= 22500) {
						e.touchData.cxt = !1, e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
						var V = {
							originalEvent: t,
							type: "cxttapend",
							position: {
								x: s[0],
								y: s[1]
							}
						};
						e.touchData.start ? (e.touchData.start.unactivate().emit(V), e.touchData.start = null) : o.emit(V)
					}
				}
				if (r && e.touchData.cxt) {
					V = {
						originalEvent: t,
						type: "cxtdrag",
						position: {
							x: s[0],
							y: s[1]
						}
					};
					e.data.bgActivePosistion = void 0, e.redrawHint("select", !0), e.touchData.start ? e.touchData.start.emit(V) : o.emit(V), e.touchData.start && (e.touchData.start._private.grabbed = !1), e.touchData.cxtDragged = !0;
					var F = e.findNearestElement(s[0], s[1], !0, !0);
					e.touchData.cxtOver && F === e.touchData.cxtOver || (e.touchData.cxtOver && e.touchData.cxtOver.emit({
						originalEvent: t,
						type: "cxtdragout",
						position: {
							x: s[0],
							y: s[1]
						}
					}), e.touchData.cxtOver = F, F && F.emit({
						originalEvent: t,
						type: "cxtdragover",
						position: {
							x: s[0],
							y: s[1]
						}
					}))
				} else if (r && t.touches[2] && o.boxSelectionEnabled()) t.preventDefault(), e.data.bgActivePosistion = void 0, this.lastThreeTouch = +new Date, e.touchData.selecting || o.emit({
					originalEvent: t,
					type: "boxstart",
					position: {
						x: s[0],
						y: s[1]
					}
				}), e.touchData.selecting = !0, e.touchData.didSelect = !0, a[4] = 1, a && 0 !== a.length && void 0 !== a[0] ? (a[2] = (s[0] + s[2] + s[4]) / 3, a[3] = (s[1] + s[3] + s[5]) / 3) : (a[0] = (s[0] + s[2] + s[4]) / 3, a[1] = (s[1] + s[3] + s[5]) / 3, a[2] = (s[0] + s[2] + s[4]) / 3 + 1, a[3] = (s[1] + s[3] + s[5]) / 3 + 1), e.redrawHint("select", !0), e.redraw();
				else if (r && t.touches[1] && !e.touchData.didSelect && o.zoomingEnabled() && o.panningEnabled() && o.userZoomingEnabled() && o.userPanningEnabled()) {
					if (t.preventDefault(), e.data.bgActivePosistion = void 0, e.redrawHint("select", !0), ee = e.dragData.touchDragEles) {
						e.redrawHint("drag", !0);
						for (var q = 0; q < ee.length; q++) {
							var Y = ee[q]._private;
							Y.grabbed = !1, Y.rscratch.inDragLayer = !1
						}
					}
					var j = e.touchData.start,
						X = (N = t.touches[0].clientX - D, I = t.touches[0].clientY - P, z = t.touches[1].clientX - D, O = t.touches[1].clientY - P, L(N, I, z, O)),
						W = X / E;
					if (B) {
						var H = (N - m + (z - x)) / 2,
							K = (I - b + (O - w)) / 2,
							G = o.zoom(),
							Z = G * W,
							U = o.pan(),
							$ = S[0] * G + U.x,
							Q = S[1] * G + U.y,
							J = {
								x: -Z / G * ($ - U.x - H) + $,
								y: -Z / G * (Q - U.y - K) + Q
							};
						if (j && j.active()) {
							var ee = e.dragData.touchDragEles;
							c(ee), e.redrawHint("drag", !0), e.redrawHint("eles", !0), j.unactivate().emit("freeon"), ee.emit("free"), e.dragData.didDrag && (j.emit("dragfreeon"), ee.emit("dragfree"))
						}
						o.viewport({
							zoom: Z,
							pan: J,
							cancelOnFailedZoom: !0
						}), E = X, m = N, b = I, x = z, w = O, e.pinching = !0
					}
					if (t.touches[0]) {
						d = e.projectIntoViewport(t.touches[0].clientX, t.touches[0].clientY);
						s[0] = d[0], s[1] = d[1]
					}
					if (t.touches[1]) {
						d = e.projectIntoViewport(t.touches[1].clientX, t.touches[1].clientY);
						s[2] = d[0], s[3] = d[1]
					}
					if (t.touches[2]) {
						d = e.projectIntoViewport(t.touches[2].clientX, t.touches[2].clientY);
						s[4] = d[0], s[5] = d[1]
					}
				} else if (t.touches[0] && !e.touchData.didSelect) {
					var te = e.touchData.start,
						ne = e.touchData.last;
					if (e.hoverData.draggingEles || e.swipePanning || (F = e.findNearestElement(s[0], s[1], !0, !0)), r && null != te && t.preventDefault(), r && null != te && e.nodeIsDraggable(te))
						if (p) {
							ee = e.dragData.touchDragEles;
							var re = !e.dragData.didDrag;
							re && l(ee, {
								inDragLayer: !0
							}), e.dragData.didDrag = !0;
							var ie = {
								x: 0,
								y: 0
							};
							if (v(g[0]) && v(g[1]))
								if (ie.x += g[0], ie.y += g[1], re) e.redrawHint("eles", !0), (ae = e.touchData.dragDelta) && v(ae[0]) && v(ae[1]) && (ie.x += ae[0], ie.y += ae[1]);
							e.hoverData.draggingEles = !0, ee.silentShift(ie).emit("position drag"), e.redrawHint("drag", !0), e.touchData.startPosition[0] == u[0] && e.touchData.startPosition[1] == u[1] && e.redrawHint("eles", !0), e.redraw()
						} else {
							var ae;
							0 === (ae = e.touchData.dragDelta = e.touchData.dragDelta || []).length ? (ae.push(g[0]), ae.push(g[1])) : (ae[0] += g[0], ae[1] += g[1])
						}
					if (n(te || F, ["touchmove", "tapdrag", "vmousemove"], t, {
						x: s[0],
						y: s[1]
					}), te && te.grabbed() || F == ne || (ne && ne.emit({
						originalEvent: t,
						type: "tapdragout",
						position: {
							x: s[0],
							y: s[1]
						}
					}), F && F.emit({
						originalEvent: t,
						type: "tapdragover",
						position: {
							x: s[0],
							y: s[1]
						}
					})), e.touchData.last = F, r)
						for (q = 0; q < s.length; q++) s[q] && e.touchData.startPosition[q] && p && (e.touchData.singleTouchMoved = !0);
					if (r && (null == te || te.pannable()) && o.panningEnabled() && o.userPanningEnabled()) {
						i(te, e.touchData.starts) && (t.preventDefault(), e.data.bgActivePosistion || (e.data.bgActivePosistion = rt(e.touchData.startPosition)), e.swipePanning ? o.panBy({
							x: g[0] * h,
							y: g[1] * h
						}) : p && (e.swipePanning = !0, o.panBy({
							x: T * h,
							y: _ * h
						}), te && (te.unactivate(), e.redrawHint("select", !0), e.touchData.start = null)));
						d = e.projectIntoViewport(t.touches[0].clientX, t.touches[0].clientY);
						s[0] = d[0], s[1] = d[1]
					}
				}
				for (C = 0; C < s.length; C++) u[C] = s[C];
				r && t.touches.length > 0 && !e.hoverData.draggingEles && !e.swipePanning && null != e.data.bgActivePosistion && (e.data.bgActivePosistion = void 0, e.redrawHint("select", !0), e.redraw())
			}
		}, !1), e.registerBinding(window, "touchcancel", I = function (t) {
			var n = e.touchData.start;
			e.touchData.capture = !1, n && n.unactivate()
		}), e.registerBinding(window, "touchend", z = function (r) {
			var i = e.touchData.start;
			if (e.touchData.capture) {
				0 === r.touches.length && (e.touchData.capture = !1), r.preventDefault();
				var a = e.selection;
				e.swipePanning = !1, e.hoverData.draggingEles = !1;
				var o, s = e.cy,
					l = s.zoom(),
					u = e.touchData.now,
					h = e.touchData.earlier;
				if (r.touches[0]) {
					var d = e.projectIntoViewport(r.touches[0].clientX, r.touches[0].clientY);
					u[0] = d[0], u[1] = d[1]
				}
				if (r.touches[1]) {
					d = e.projectIntoViewport(r.touches[1].clientX, r.touches[1].clientY);
					u[2] = d[0], u[3] = d[1]
				}
				if (r.touches[2]) {
					d = e.projectIntoViewport(r.touches[2].clientX, r.touches[2].clientY);
					u[4] = d[0], u[5] = d[1]
				}
				if (i && i.unactivate(), e.touchData.cxt) {
					if (o = {
						originalEvent: r,
						type: "cxttapend",
						position: {
							x: u[0],
							y: u[1]
						}
					}, i ? i.emit(o) : s.emit(o), !e.touchData.cxtDragged) {
						var p = {
							originalEvent: r,
							type: "cxttap",
							position: {
								x: u[0],
								y: u[1]
							}
						};
						i ? i.emit(p) : s.emit(p)
					}
					return e.touchData.start && (e.touchData.start._private.grabbed = !1), e.touchData.cxt = !1, e.touchData.start = null, void e.redraw()
				}
				if (!r.touches[2] && s.boxSelectionEnabled() && e.touchData.selecting) {
					e.touchData.selecting = !1;
					var f = s.collection(e.getAllInBox(a[0], a[1], a[2], a[3]));
					a[0] = void 0, a[1] = void 0, a[2] = void 0, a[3] = void 0, a[4] = 0, e.redrawHint("select", !0), s.emit({
						type: "boxend",
						originalEvent: r,
						position: {
							x: u[0],
							y: u[1]
						}
					});
					f.emit("box").stdFilter(function (e) {
						return e.selectable() && !e.selected()
					}).select().emit("boxselect"), f.nonempty() && e.redrawHint("eles", !0), e.redraw()
				}
				if (null != i && i.unactivate(), r.touches[2]) e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
				else if (r.touches[1]);
				else if (r.touches[0]);
				else if (!r.touches[0]) {
					e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
					var g = e.dragData.touchDragEles;
					if (null != i) {
						var v = i._private.grabbed;
						c(g), e.redrawHint("drag", !0), e.redrawHint("eles", !0), v && (i.emit("freeon"), g.emit("free"), e.dragData.didDrag && (i.emit("dragfreeon"), g.emit("dragfree"))), n(i, ["touchend", "tapend", "vmouseup", "tapdragout"], r, {
							x: u[0],
							y: u[1]
						}), i.unactivate(), e.touchData.start = null
					} else {
						var y = e.findNearestElement(u[0], u[1], !0, !0);
						n(y, ["touchend", "tapend", "vmouseup", "tapdragout"], r, {
							x: u[0],
							y: u[1]
						})
					}
					var m = e.touchData.startPosition[0] - u[0],
						b = m * m,
						x = e.touchData.startPosition[1] - u[1],
						w = (b + x * x) * l * l;
					e.touchData.singleTouchMoved || (i || s.$(":selected").unselect(["tapunselect"]), n(i, ["tap", "vclick"], r, {
						x: u[0],
						y: u[1]
					})), null != i && !e.dragData.didDrag && i._private.selectable && w < e.touchTapThreshold2 && !e.pinching && ("single" === s.selectionType() ? (s.$(t).unmerge(i).unselect(["tapunselect"]), i.select(["tapselect"])) : i.selected() ? i.unselect(["tapunselect"]) : i.select(["tapselect"]), e.redrawHint("eles", !0)), e.touchData.singleTouchMoved = !0
				}
				for (var E = 0; E < u.length; E++) h[E] = u[E];
				e.dragData.didDrag = !1, 0 === r.touches.length && (e.touchData.dragDelta = [], e.touchData.startPosition = null, e.touchData.startGPosition = null, e.touchData.didSelect = !1), r.touches.length < 2 && (1 === r.touches.length && (e.touchData.startGPosition = [r.touches[0].clientX, r.touches[0].clientY]), e.pinching = !1, e.redrawHint("eles", !0), e.redraw())
			}
		}, !1), "undefined" == typeof TouchEvent) {
			var O = [],
				R = function (e) {
					return {
						clientX: e.clientX,
						clientY: e.clientY,
						force: 1,
						identifier: e.pointerId,
						pageX: e.pageX,
						pageY: e.pageY,
						radiusX: e.width / 2,
						radiusY: e.height / 2,
						screenX: e.screenX,
						screenY: e.screenY,
						target: e.target
					}
				},
				V = function (e) {
					O.push(function (e) {
						return {
							event: e,
							touch: R(e)
						}
					}(e))
				},
				F = function (e) {
					for (var t = 0; t < O.length; t++) {
						if (O[t].event.pointerId === e.pointerId) return void O.splice(t, 1)
					}
				},
				q = function (e) {
					e.touches = O.map(function (e) {
						return e.touch
					})
				},
				Y = function (e) {
					return "mouse" === e.pointerType || 4 === e.pointerType
				};
			e.registerBinding(e.container, "pointerdown", function (e) {
				Y(e) || (e.preventDefault(), V(e), q(e), _(e))
			}), e.registerBinding(e.container, "pointerup", function (e) {
				Y(e) || (F(e), q(e), z(e))
			}), e.registerBinding(e.container, "pointercancel", function (e) {
				Y(e) || (F(e), q(e), I())
			}), e.registerBinding(e.container, "pointermove", function (e) {
				Y(e) || (e.preventDefault(), function (e) {
					var t = O.filter(function (t) {
						return t.event.pointerId === e.pointerId
					})[0];
					t.event = e, t.touch = R(e)
				}(e), q(e), N(e))
			})
		}
	};
	var ts = {
		generatePolygon: function (e, t) {
			return this.nodeShapes[e] = {
				renderer: this,
				name: e,
				points: t,
				draw: function (e, t, n, r, i) {
					this.renderer.nodeShapeImpl("polygon", e, t, n, r, i, this.points)
				},
				intersectLine: function (e, t, n, r, i, a, o) {
					return zt(i, a, this.points, e, t, n / 2, r / 2, o)
				},
				checkPoint: function (e, t, n, r, i, a, o) {
					return Pt(e, t, this.points, a, o, r, i, [0, -1], n)
				}
			}
		}
	};
	ts.generateEllipse = function () {
		return this.nodeShapes.ellipse = {
			renderer: this,
			name: "ellipse",
			draw: function (e, t, n, r, i) {
				this.renderer.nodeShapeImpl(this.name, e, t, n, r, i)
			},
			intersectLine: function (e, t, n, r, i, a, o) {
				return function (e, t, n, r, i, a) {
					var o = n - e,
						s = r - t;
					o /= i, s /= a;
					var l = Math.sqrt(o * o + s * s),
						u = l - 1;
					if (u < 0) return [];
					var c = u / l;
					return [(n - e) * c + e, (r - t) * c + t]
				}(i, a, e, t, n / 2 + o, r / 2 + o)
			},
			checkPoint: function (e, t, n, r, i, a, o) {
				return Bt(e, t, r, i, a, o, n)
			}
		}
	}, ts.generateRoundRectangle = function () {
		return this.nodeShapes["round-rectangle"] = this.nodeShapes.roundrectangle = {
			renderer: this,
			name: "round-rectangle",
			points: At(4, 0),
			draw: function (e, t, n, r, i) {
				this.renderer.nodeShapeImpl(this.name, e, t, n, r, i)
			},
			intersectLine: function (e, t, n, r, i, a, o) {
				return wt(i, a, e, t, n, r, o)
			},
			checkPoint: function (e, t, n, r, i, a, o) {
				var s = Vt(r, i),
					l = 2 * s;
				return !!Pt(e, t, this.points, a, o, r, i - l, [0, -1], n) || (!!Pt(e, t, this.points, a, o, r - l, i, [0, -1], n) || (!!Bt(e, t, l, l, a - r / 2 + s, o - i / 2 + s, n) || (!!Bt(e, t, l, l, a + r / 2 - s, o - i / 2 + s, n) || (!!Bt(e, t, l, l, a + r / 2 - s, o + i / 2 - s, n) || !!Bt(e, t, l, l, a - r / 2 + s, o + i / 2 - s, n)))))
			}
		}
	}, ts.generateCutRectangle = function () {
		return this.nodeShapes["cut-rectangle"] = this.nodeShapes.cutrectangle = {
			renderer: this,
			name: "cut-rectangle",
			cornerLength: 8,
			points: At(4, 0),
			draw: function (e, t, n, r, i) {
				this.renderer.nodeShapeImpl(this.name, e, t, n, r, i)
			},
			generateCutTrianglePts: function (e, t, n, r) {
				var i = this.cornerLength,
					a = t / 2,
					o = e / 2,
					s = n - o,
					l = n + o,
					u = r - a,
					c = r + a;
				return {
					topLeft: [s, u + i, s + i, u, s + i, u + i],
					topRight: [l - i, u, l, u + i, l - i, u + i],
					bottomRight: [l, c - i, l - i, c, l - i, c - i],
					bottomLeft: [s + i, c, s, c - i, s + i, c - i]
				}
			},
			intersectLine: function (e, t, n, r, i, a, o) {
				var s = this.generateCutTrianglePts(n + 2 * o, r + 2 * o, e, t),
					l = [].concat.apply([], [s.topLeft.splice(0, 4), s.topRight.splice(0, 4), s.bottomRight.splice(0, 4), s.bottomLeft.splice(0, 4)]);
				return zt(i, a, l, e, t)
			},
			checkPoint: function (e, t, n, r, i, a, o) {
				if (Pt(e, t, this.points, a, o, r, i - 2 * this.cornerLength, [0, -1], n)) return !0;
				if (Pt(e, t, this.points, a, o, r - 2 * this.cornerLength, i, [0, -1], n)) return !0;
				var s = this.generateCutTrianglePts(r, i, a, o);
				return Dt(e, t, s.topLeft) || Dt(e, t, s.topRight) || Dt(e, t, s.bottomRight) || Dt(e, t, s.bottomLeft)
			}
		}
	}, ts.generateBarrel = function () {
		return this.nodeShapes.barrel = {
			renderer: this,
			name: "barrel",
			points: At(4, 0),
			draw: function (e, t, n, r, i) {
				this.renderer.nodeShapeImpl(this.name, e, t, n, r, i)
			},
			intersectLine: function (e, t, n, r, i, a, o) {
				var s = this.generateBarrelBezierPts(n + 2 * o, r + 2 * o, e, t),
					l = function (e) {
						var t = ht({
							x: e[0],
							y: e[1]
						}, {
								x: e[2],
								y: e[3]
							}, {
								x: e[4],
								y: e[5]
							}, .15),
							n = ht({
								x: e[0],
								y: e[1]
							}, {
									x: e[2],
									y: e[3]
								}, {
									x: e[4],
									y: e[5]
								}, .5),
							r = ht({
								x: e[0],
								y: e[1]
							}, {
									x: e[2],
									y: e[3]
								}, {
									x: e[4],
									y: e[5]
								}, .85);
						return [e[0], e[1], t.x, t.y, n.x, n.y, r.x, r.y, e[4], e[5]]
					},
					u = [].concat(l(s.topLeft), l(s.topRight), l(s.bottomRight), l(s.bottomLeft));
				return zt(i, a, u, e, t)
			},
			generateBarrelBezierPts: function (e, t, n, r) {
				var i = t / 2,
					a = e / 2,
					o = n - a,
					s = n + a,
					l = r - i,
					u = r + i,
					c = Ft(e, t),
					h = c.heightOffset,
					d = c.widthOffset,
					p = c.ctrlPtOffsetPct * e,
					f = {
						topLeft: [o, l + h, o + p, l, o + d, l],
						topRight: [s - d, l, s - p, l, s, l + h],
						bottomRight: [s, u - h, s - p, u, s - d, u],
						bottomLeft: [o + d, u, o + p, u, o, u - h]
					};
				return f.topLeft.isTop = !0, f.topRight.isTop = !0, f.bottomLeft.isBottom = !0, f.bottomRight.isBottom = !0, f
			},
			checkPoint: function (e, t, n, r, i, a, o) {
				var s = Ft(r, i),
					l = s.heightOffset,
					u = s.widthOffset;
				if (Pt(e, t, this.points, a, o, r, i - 2 * l, [0, -1], n)) return !0;
				if (Pt(e, t, this.points, a, o, r - 2 * u, i, [0, -1], n)) return !0;
				for (var c = this.generateBarrelBezierPts(r, i, a, o), h = function (e, t, n) {
					var r, i, a = n[4],
						o = n[2],
						s = n[0],
						l = n[5],
						u = n[1],
						c = Math.min(a, s),
						h = Math.max(a, s),
						d = Math.min(l, u),
						p = Math.max(l, u);
					if (c <= e && e <= h && d <= t && t <= p) {
						var f = [(r = a) - 2 * (i = o) + s, 2 * (i - r), r],
							g = function (e, t, n, r) {
								var i = t * t - 4 * e * (n -= r);
								if (i < 0) return [];
								var a = Math.sqrt(i),
									o = 2 * e;
								return [(-t + a) / o, (-t - a) / o]
							}(f[0], f[1], f[2], e).filter(function (e) {
								return 0 <= e && e <= 1
							});
						if (g.length > 0) return g[0]
					}
					return null
				}, d = Object.keys(c), p = 0; p < d.length; p++) {
					var f = c[d[p]],
						g = h(e, t, f);
					if (null != g) {
						var v = f[5],
							y = f[3],
							m = f[1],
							b = ct(v, y, m, g);
						if (f.isTop && b <= t) return !0;
						if (f.isBottom && t <= b) return !0
					}
				}
				return !1
			}
		}
	}, ts.generateBottomRoundrectangle = function () {
		return this.nodeShapes["bottom-round-rectangle"] = this.nodeShapes.bottomroundrectangle = {
			renderer: this,
			name: "bottom-round-rectangle",
			points: At(4, 0),
			draw: function (e, t, n, r, i) {
				this.renderer.nodeShapeImpl(this.name, e, t, n, r, i)
			},
			intersectLine: function (e, t, n, r, i, a, o) {
				var s = t - (r / 2 + o),
					l = It(i, a, e, t, e - (n / 2 + o), s, e + (n / 2 + o), s, !1);
				return l.length > 0 ? l : wt(i, a, e, t, n, r, o)
			},
			checkPoint: function (e, t, n, r, i, a, o) {
				var s = Vt(r, i),
					l = 2 * s;
				if (Pt(e, t, this.points, a, o, r, i - l, [0, -1], n)) return !0;
				if (Pt(e, t, this.points, a, o, r - l, i, [0, -1], n)) return !0;
				var u = r / 2 + 2 * n,
					c = i / 2 + 2 * n;
				return !!Dt(e, t, [a - u, o - c, a - u, o, a + u, o, a + u, o - c]) || (!!Bt(e, t, l, l, a + r / 2 - s, o + i / 2 - s, n) || !!Bt(e, t, l, l, a - r / 2 + s, o + i / 2 - s, n))
			}
		}
	}, ts.registerNodeShapes = function () {
		var e = this.nodeShapes = {},
			t = this;
		this.generateEllipse(), this.generatePolygon("triangle", At(3, 0)), this.generatePolygon("rectangle", At(4, 0)), e.square = e.rectangle, this.generateRoundRectangle(), this.generateCutRectangle(), this.generateBarrel(), this.generateBottomRoundrectangle(), this.generatePolygon("diamond", [0, 1, 1, 0, 0, -1, -1, 0]), this.generatePolygon("pentagon", At(5, 0)), this.generatePolygon("hexagon", At(6, 0)), this.generatePolygon("heptagon", At(7, 0)), this.generatePolygon("octagon", At(8, 0));
		var n = new Array(20),
			r = Rt(5, 0),
			i = Rt(5, Math.PI / 5),
			a = .5 * (3 - Math.sqrt(5));
		a *= 1.57;
		for (var o = 0; o < i.length / 2; o++) i[2 * o] *= a, i[2 * o + 1] *= a;
		for (o = 0; o < 5; o++) n[4 * o] = r[2 * o], n[4 * o + 1] = r[2 * o + 1], n[4 * o + 2] = i[2 * o], n[4 * o + 3] = i[2 * o + 1];
		n = Ot(n), this.generatePolygon("star", n), this.generatePolygon("vee", [-1, -1, 0, -.333, 1, -1, 0, 1]), this.generatePolygon("rhomboid", [-1, -1, .333, -1, 1, 1, -.333, 1]), this.nodeShapes.concavehexagon = this.generatePolygon("concave-hexagon", [-1, -.95, -.75, 0, -1, .95, 1, .95, .75, 0, 1, -.95]), this.generatePolygon("tag", [-1, -1, .25, -1, 1, 0, .25, 1, -1, 1]), e.makePolygon = function (e) {
			var n, r = "polygon-" + e.join("$");
			return (n = this[r]) ? n : t.generatePolygon(r, e)
		}
	};
	var ns = {
		timeToRender: function () {
			return this.redrawTotalTime / this.redrawCount
		},
		redraw: function (e) {
			e = e || Te();
			var t = this;
			void 0 === t.averageRedrawTime && (t.averageRedrawTime = 0), void 0 === t.lastRedrawTime && (t.lastRedrawTime = 0), void 0 === t.lastDrawTime && (t.lastDrawTime = 0), t.requestedFrame = !0, t.renderOptions = e
		},
		beforeRender: function (e, t) {
			if (!this.destroyed) {
				null == t && Ee("Priority is not optional for beforeRender");
				var n = this.beforeRenderCallbacks;
				n.push({
					fn: e,
					priority: t
				}), n.sort(function (e, t) {
					return t.priority - e.priority
				})
			}
		}
	},
		rs = function (e, t, n) {
			for (var r = e.beforeRenderCallbacks, i = 0; i < r.length; i++) r[i].fn(t, n)
		};
	ns.startRenderLoop = function () {
		var e = this,
			t = e.cy;
		if (!e.renderLoopStarted) {
			e.renderLoopStarted = !0;
			oe(function n(r) {
				if (!e.destroyed) {
					if (t.batching());
					else if (e.requestedFrame && !e.skipFrame) {
						rs(e, !0, r);
						var i = se();
						e.render(e.renderOptions);
						var a = e.lastDrawTime = se();
						void 0 === e.averageRedrawTime && (e.averageRedrawTime = a - i), void 0 === e.redrawCount && (e.redrawCount = 0), e.redrawCount++ , void 0 === e.redrawTotalTime && (e.redrawTotalTime = 0);
						var o = a - i;
						e.redrawTotalTime += o, e.lastRedrawTime = o, e.averageRedrawTime = e.averageRedrawTime / 2 + o / 2, e.requestedFrame = !1
					} else rs(e, !1, r);
					e.skipFrame = !1, oe(n)
				}
			})
		}
	};
	var is = function (e) {
		this.init(e)
	},
		as = is.prototype;
	as.clientFunctions = ["redrawHint", "render", "renderTo", "matchCanvasSize", "nodeShapeImpl", "arrowShapeImpl"], as.init = function (e) {
		var t = this;
		t.options = e, t.cy = e.cy;
		var n = t.container = e.cy.container();
		if (a) {
			var r = a.document,
				i = r.head,
				o = "__________cytoscape_container",
				s = null != r.getElementById("__________cytoscape_stylesheet");
			if (n.className.indexOf(o) < 0 && (n.className = (n.className || "") + " " + o), !s) {
				var l = r.createElement("style");
				l.id = "__________cytoscape_stylesheet", l.innerHTML = "." + o + " { position: relative; }", i.insertBefore(l, i.children[0])
			}
			"static" === a.getComputedStyle(n).getPropertyValue("position") && Ce("A Cytoscape container has style position:static and so can not use UI extensions properly")
		}
		t.selection = [void 0, void 0, void 0, void 0, 0], t.bezierProjPcts = [.05, .225, .4, .5, .6, .775, .95], t.hoverData = {
			down: null,
			last: null,
			downTime: null,
			triggerMode: null,
			dragging: !1,
			initialPan: [null, null],
			capture: !1
		}, t.dragData = {
			possibleDragElements: []
		}, t.touchData = {
			start: null,
			capture: !1,
			startPosition: [null, null, null, null, null, null],
			singleTouchStartTime: null,
			singleTouchMoved: !0,
			now: [null, null, null, null, null, null],
			earlier: [null, null, null, null, null, null]
		}, t.redraws = 0, t.showFps = e.showFps, t.debug = e.debug, t.hideEdgesOnViewport = e.hideEdgesOnViewport, t.hideLabelsOnViewport = e.hideLabelsOnViewport, t.textureOnViewport = e.textureOnViewport, t.wheelSensitivity = e.wheelSensitivity, t.motionBlurEnabled = e.motionBlur, t.forcedPixelRatio = v(e.pixelRatio) ? e.pixelRatio : null, t.motionBlur = e.motionBlur, t.motionBlurOpacity = e.motionBlurOpacity, t.motionBlurTransparency = 1 - t.motionBlurOpacity, t.motionBlurPxRatio = 1, t.mbPxRBlurry = 1, t.minMbLowQualFrames = 4, t.fullQualityMb = !1, t.clearedForMotionBlur = [], t.desktopTapThreshold = e.desktopTapThreshold, t.desktopTapThreshold2 = e.desktopTapThreshold * e.desktopTapThreshold, t.touchTapThreshold = e.touchTapThreshold, t.touchTapThreshold2 = e.touchTapThreshold * e.touchTapThreshold, t.tapholdDuration = 500, t.bindings = [], t.beforeRenderCallbacks = [], t.beforeRenderPriorities = {
			animations: 400,
			eleCalcs: 300,
			eleTxrDeq: 200,
			lyrTxrDeq: 150,
			lyrTxrSkip: 100
		}, t.registerNodeShapes(), t.registerArrowShapes(), t.registerCalculationListeners()
	}, as.notify = function (e, t) {
		var n = this,
			r = n.cy;
		this.destroyed || ("init" !== e ? "destroy" !== e ? (("add" === e || "remove" === e || "move" === e && r.hasCompoundNodes() || "load" === e || "zorder" === e || "mount" === e) && n.invalidateCachedZSortedEles(), "viewport" === e && n.redrawHint("select", !0), "load" !== e && "resize" !== e && "mount" !== e || (n.invalidateContainerClientCoordsCache(), n.matchCanvasSize(n.container)), n.redrawHint("eles", !0), n.redrawHint("drag", !0), this.startRenderLoop(), this.redraw()) : n.destroy() : n.load())
	}, as.destroy = function () {
		var e = this;
		e.destroyed = !0, e.cy.stopAnimationLoop();
		for (var t = 0; t < e.bindings.length; t++) {
			var n = e.bindings[t],
				r = n.target;
			(r.off || r.removeEventListener).apply(r, n.args)
		}
		if (e.bindings = [], e.beforeRenderCallbacks = [], e.onUpdateEleCalcsFns = [], e.removeObserver && e.removeObserver.disconnect(), e.styleObserver && e.styleObserver.disconnect(), e.labelCalcDiv) try {
			document.body.removeChild(e.labelCalcDiv)
		} catch (e) { }
	}, as.isHeadless = function () {
		return !1
	}, [Oo, Qo, Jo, es, ts, ns].forEach(function (e) {
		I(as, e)
	});
	var os = function (e) {
		return function () {
			var t = this,
				n = this.renderer;
			if (!t.dequeueingSetup) {
				t.dequeueingSetup = !0;
				var r = ne(function () {
					n.redrawHint("eles", !0), n.redrawHint("drag", !0), n.redraw()
				}, e.deqRedrawThreshold),
					i = e.priority || we;
				n.beforeRender(function (i, a) {
					var o = se(),
						s = n.averageRedrawTime,
						l = n.lastRedrawTime,
						u = [],
						c = n.cy.extent(),
						h = n.getPixelRatio();
					for (i || n.flushRenderedStyleQueue(); ;) {
						var d = se(),
							p = d - o,
							f = d - a;
						if (l < 1e3 / 60) {
							var g = 1e3 / 60 - (i ? s : 0);
							if (f >= e.deqFastCost * g) break
						} else if (i) {
							if (p >= e.deqCost * l || p >= e.deqAvgCost * s) break
						} else if (f >= e.deqNoDrawCost * (1e3 / 60)) break;
						var v = e.deq(t, h, c);
						if (!(v.length > 0)) break;
						for (var y = 0; y < v.length; y++) u.push(v[y])
					}
					u.length > 0 && (e.onDeqd(t, u), !i && e.shouldRedraw(t, u, h, c) && r())
				}, i(t))
			}
		}
	},
		ss = function () {
			function e(n) {
				var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : be;
				t(this, e), this.idsByKey = new ze, this.keyForId = new ze, this.cachesByLvl = new ze, this.lvls = [], this.getKey = n, this.doesEleInvalidateKey = r
			}
			return r(e, [{
				key: "getIdsFor",
				value: function (e) {
					null == e && Ee("Can not get id list for null key");
					var t = this.idsByKey,
						n = this.idsByKey.get(e);
					return n || (n = new Ae, t.set(e, n)), n
				}
			}, {
				key: "addIdForKey",
				value: function (e, t) {
					null != e && this.getIdsFor(e).add(t)
				}
			}, {
				key: "deleteIdForKey",
				value: function (e, t) {
					null != e && this.getIdsFor(e).delete(t)
				}
			}, {
				key: "getNumberOfIdsForKey",
				value: function (e) {
					return null == e ? 0 : this.getIdsFor(e).size
				}
			}, {
				key: "updateKeyMappingFor",
				value: function (e) {
					var t = e.id(),
						n = this.keyForId.get(t),
						r = this.getKey(e);
					this.deleteIdForKey(n, t), this.addIdForKey(r, t), this.keyForId.set(t, r)
				}
			}, {
				key: "deleteKeyMappingFor",
				value: function (e) {
					var t = e.id(),
						n = this.keyForId.get(t);
					this.deleteIdForKey(n, t), this.keyForId.delete(t)
				}
			}, {
				key: "keyHasChangedFor",
				value: function (e) {
					var t = e.id();
					return this.keyForId.get(t) !== this.getKey(e)
				}
			}, {
				key: "isInvalid",
				value: function (e) {
					return this.keyHasChangedFor(e) || this.doesEleInvalidateKey(e)
				}
			}, {
				key: "getCachesAt",
				value: function (e) {
					var t = this.cachesByLvl,
						n = this.lvls,
						r = t.get(e);
					return r || (r = new ze, t.set(e, r), n.push(e)), r
				}
			}, {
				key: "getCache",
				value: function (e, t) {
					return this.getCachesAt(t).get(e)
				}
			}, {
				key: "get",
				value: function (e, t) {
					var n = this.getKey(e),
						r = this.getCache(n, t);
					return null != r && this.updateKeyMappingFor(e), r
				}
			}, {
				key: "getForCachedKey",
				value: function (e, t) {
					var n = this.keyForId.get(e.id());
					return this.getCache(n, t)
				}
			}, {
				key: "hasCache",
				value: function (e, t) {
					return this.getCachesAt(t).has(e)
				}
			}, {
				key: "has",
				value: function (e, t) {
					var n = this.getKey(e);
					return this.hasCache(n, t)
				}
			}, {
				key: "setCache",
				value: function (e, t, n) {
					n.key = e, this.getCachesAt(t).set(e, n)
				}
			}, {
				key: "set",
				value: function (e, t, n) {
					var r = this.getKey(e);
					this.setCache(r, t, n), this.updateKeyMappingFor(e)
				}
			}, {
				key: "deleteCache",
				value: function (e, t) {
					this.getCachesAt(t).delete(e)
				}
			}, {
				key: "delete",
				value: function (e, t) {
					var n = this.getKey(e);
					this.deleteCache(n, t)
				}
			}, {
				key: "invalidateKey",
				value: function (e) {
					var t = this;
					this.lvls.forEach(function (n) {
						return t.deleteCache(e, n)
					})
				}
			}, {
				key: "invalidate",
				value: function (e) {
					var t = e.id(),
						n = this.keyForId.get(t);
					this.deleteKeyMappingFor(e);
					var r = this.doesEleInvalidateKey(e);
					return r && this.invalidateKey(n), r || 0 === this.getNumberOfIdsForKey(n)
				}
			}]), e
		}(),
		ls = {
			dequeue: "dequeue",
			downscale: "downscale",
			highQuality: "highQuality"
		},
		us = Me({
			getKey: null,
			doesEleInvalidateKey: be,
			drawElement: null,
			getBoundingBox: null,
			getRotationPoint: null,
			getRotationOffset: null,
			isVisible: me,
			allowEdgeTxrCaching: !0,
			allowParentTxrCaching: !0
		}),
		cs = function (e, t) {
			this.renderer = e, this.onDequeues = [];
			var n = us(t);
			I(this, n), this.lookup = new ss(n.getKey, n.doesEleInvalidateKey), this.setupDequeueing()
		},
		hs = cs.prototype;
	hs.reasons = ls, hs.getTextureQueue = function (e) {
		return this.eleImgCaches = this.eleImgCaches || {}, this.eleImgCaches[e] = this.eleImgCaches[e] || []
	}, hs.getRetiredTextureQueue = function (e) {
		var t = this.eleImgCaches.retired = this.eleImgCaches.retired || {};
		return t[e] = t[e] || []
	}, hs.getElementQueue = function () {
		return this.eleCacheQueue = this.eleCacheQueue || new Fe(function (e, t) {
			return t.reqs - e.reqs
		})
	}, hs.getElementKeyToQueue = function () {
		return this.eleKeyToCacheQueue = this.eleKeyToCacheQueue || {}
	}, hs.getElement = function (e, t, n, r, i) {
		var a = this,
			o = this.renderer,
			s = o.cy.zoom(),
			l = this.lookup;
		if (0 === t.w || 0 === t.h || isNaN(t.w) || isNaN(t.h) || !e.visible()) return null;
		if (!a.allowEdgeTxrCaching && e.isEdge() || !a.allowParentTxrCaching && e.isParent()) return null;
		if (null == r && (r = Math.ceil(at(s * n))), r < -4) r = -4;
		else if (s >= 7.99 || r > 3) return null;
		var u = Math.pow(2, r),
			c = t.h * u,
			h = t.w * u,
			d = o.eleTextBiggerThanMin(e, u);
		if (!this.isVisible(e, d)) return null;
		var p, f = l.get(e, r);
		if (f && f.invalidated && (f.invalidated = !1, f.texture.invalidatedWidth -= f.width), f) return f;
		if (p = c <= 25 ? 25 : c <= 50 ? 50 : 50 * Math.ceil(c / 50), c > 1024 || h > 1024) return null;
		var g = a.getTextureQueue(p),
			v = g[g.length - 2],
			y = function () {
				return a.recycleTexture(p, h) || a.addTexture(p, h)
			};
		v || (v = g[g.length - 1]), v || (v = y()), v.width - v.usedWidth < h && (v = y());
		for (var m, b = function (e) {
			return e && e.scaledLabelShown === d
		}, x = i && i === ls.dequeue, w = i && i === ls.highQuality, E = i && i === ls.downscale, k = r + 1; k <= 3; k++) {
			var C = l.get(e, k);
			if (C) {
				m = C;
				break
			}
		}
		var S = m && m.level === r + 1 ? m : null,
			D = function () {
				v.context.drawImage(S.texture.canvas, S.x, 0, S.width, S.height, v.usedWidth, 0, h, c)
			};
		if (v.context.setTransform(1, 0, 0, 1, 0, 0), v.context.clearRect(v.usedWidth, 0, h, p), b(S)) D();
		else if (b(m)) {
			if (!w) return a.queueElement(e, m.level - 1), m;
			for (var P = m.level; P > r; P--) S = a.getElement(e, t, n, P, ls.downscale);
			D()
		} else {
			var T;
			if (!x && !w && !E)
				for (var M = r - 1; M >= -4; M--) {
					var B = l.get(e, M);
					if (B) {
						T = B;
						break
					}
				}
			if (b(T)) return a.queueElement(e, r), T;
			v.context.translate(v.usedWidth, 0), v.context.scale(u, u), this.drawElement(v.context, e, t, d, !1), v.context.scale(1 / u, 1 / u), v.context.translate(-v.usedWidth, 0)
		}
		return f = {
			x: v.usedWidth,
			texture: v,
			level: r,
			scale: u,
			width: h,
			height: c,
			scaledLabelShown: d
		}, v.usedWidth += Math.ceil(h + 8), v.eleCaches.push(f), l.set(e, r, f), a.checkTextureFullness(v), f
	}, hs.invalidateElements = function (e) {
		for (var t = 0; t < e.length; t++) this.invalidateElement(e[t])
	}, hs.invalidateElement = function (e) {
		var t = this.lookup,
			n = [];
		if (t.isInvalid(e)) {
			for (var r = -4; r <= 3; r++) {
				var i = t.getForCachedKey(e, r);
				i && n.push(i)
			}
			if (t.invalidate(e))
				for (var a = 0; a < n.length; a++) {
					var o = n[a],
						s = o.texture;
					s.invalidatedWidth += o.width, o.invalidated = !0, this.checkTextureUtility(s)
				}
			this.removeFromQueue(e)
		}
	}, hs.checkTextureUtility = function (e) {
		e.invalidatedWidth >= .2 * e.width && this.retireTexture(e)
	}, hs.checkTextureFullness = function (e) {
		var t = this.getTextureQueue(e.height);
		e.usedWidth / e.width > .8 && e.fullnessChecks >= 10 ? Be(t, e) : e.fullnessChecks++
	}, hs.retireTexture = function (e) {
		var t = e.height,
			n = this.getTextureQueue(t),
			r = this.lookup;
		Be(n, e), e.retired = !0;
		for (var i = e.eleCaches, a = 0; a < i.length; a++) {
			var o = i[a];
			r.deleteCache(o.key, o.level)
		}
		_e(i), this.getRetiredTextureQueue(t).push(e)
	}, hs.addTexture = function (e, t) {
		var n = {};
		return this.getTextureQueue(e).push(n), n.eleCaches = [], n.height = e, n.width = Math.max(1024, t), n.usedWidth = 0, n.invalidatedWidth = 0, n.fullnessChecks = 0, n.canvas = this.renderer.makeOffscreenCanvas(n.width, n.height), n.context = n.canvas.getContext("2d"), n
	}, hs.recycleTexture = function (e, t) {
		for (var n = this.getTextureQueue(e), r = this.getRetiredTextureQueue(e), i = 0; i < r.length; i++) {
			var a = r[i];
			if (a.width >= t) return a.retired = !1, a.usedWidth = 0, a.invalidatedWidth = 0, a.fullnessChecks = 0, _e(a.eleCaches), a.context.setTransform(1, 0, 0, 1, 0, 0), a.context.clearRect(0, 0, a.width, a.height), Be(r, a), n.push(a), a
		}
	}, hs.queueElement = function (e, t) {
		var n = this.getElementQueue(),
			r = this.getElementKeyToQueue(),
			i = this.getKey(e),
			a = r[i];
		if (a) a.level = Math.max(a.level, t), a.eles.merge(e), a.reqs++ , n.updateItem(a);
		else {
			var o = {
				eles: e.spawn().merge(e),
				level: t,
				reqs: 1,
				key: i
			};
			n.push(o), r[i] = o
		}
	}, hs.dequeue = function (e) {
		for (var t = this.getElementQueue(), n = this.getElementKeyToQueue(), r = [], i = this.lookup, a = 0; a < 1 && t.size() > 0; a++) {
			var o = t.pop(),
				s = o.key,
				l = o.eles[0],
				u = i.hasCache(l, o.level);
			if (n[s] = null, !u) {
				r.push(o);
				var c = this.getBoundingBox(l);
				this.getElement(l, c, e, o.level, ls.dequeue)
			}
		}
		return r
	}, hs.removeFromQueue = function (e) {
		var t = this.getElementQueue(),
			n = this.getElementKeyToQueue(),
			r = this.getKey(e),
			i = n[r];
		null != i && (1 === i.eles.length ? (i.reqs = ye, t.updateItem(i), t.pop(), n[r] = null) : i.eles.unmerge(e))
	}, hs.onDequeue = function (e) {
		this.onDequeues.push(e)
	}, hs.offDequeue = function (e) {
		Be(this.onDequeues, e)
	}, hs.setupDequeueing = os({
		deqRedrawThreshold: 100,
		deqCost: .15,
		deqAvgCost: .1,
		deqNoDrawCost: .9,
		deqFastCost: .9,
		deq: function (e, t, n) {
			return e.dequeue(t, n)
		},
		onDeqd: function (e, t) {
			for (var n = 0; n < e.onDequeues.length; n++) {
				(0, e.onDequeues[n])(t)
			}
		},
		shouldRedraw: function (e, t, n, r) {
			for (var i = 0; i < t.length; i++)
				for (var a = t[i].eles, o = 0; o < a.length; o++) {
					var s = a[o].boundingBox();
					if (mt(s, r)) return !0
				}
			return !1
		},
		priority: function (e) {
			return e.renderer.beforeRenderPriorities.eleTxrDeq
		}
	});
	var ds = function (e) {
		var t = this,
			n = t.renderer = e,
			r = n.cy;
		t.layersByLevel = {}, t.firstGet = !0, t.lastInvalidationTime = se() - 500, t.skipping = !1, t.eleTxrDeqs = r.collection(), t.scheduleElementRefinement = ne(function () {
			t.refineElementTextures(t.eleTxrDeqs), t.eleTxrDeqs.unmerge(t.eleTxrDeqs)
		}, 50), n.beforeRender(function (e, n) {
			n - t.lastInvalidationTime <= 250 ? t.skipping = !0 : t.skipping = !1
		}, n.beforeRenderPriorities.lyrTxrSkip);
		t.layersQueue = new Fe(function (e, t) {
			return t.reqs - e.reqs
		}), t.setupDequeueing()
	},
		ps = ds.prototype,
		fs = 0,
		gs = Math.pow(2, 53) - 1;
	ps.makeLayer = function (e, t) {
		var n = Math.pow(2, t),
			r = Math.ceil(e.w * n),
			i = Math.ceil(e.h * n),
			a = this.renderer.makeOffscreenCanvas(r, i),
			o = {
				id: fs = ++fs % gs,
				bb: e,
				level: t,
				width: r,
				height: i,
				canvas: a,
				context: a.getContext("2d"),
				eles: [],
				elesQueue: [],
				reqs: 0
			},
			s = o.context,
			l = -o.bb.x1,
			u = -o.bb.y1;
		return s.scale(n, n), s.translate(l, u), o
	}, ps.getLayers = function (e, t, n) {
		var r = this,
			i = r.renderer.cy.zoom(),
			a = r.firstGet;
		if (r.firstGet = !1, null == n)
			if ((n = Math.ceil(at(i * t))) < -4) n = -4;
			else if (i >= 3.99 || n > 2) return null;
		r.validateLayersElesOrdering(n, e);
		var o, s, l = r.layersByLevel,
			u = Math.pow(2, n),
			c = l[n] = l[n] || [];
		if (r.levelIsComplete(n, e)) return c;
		! function () {
			var t = function (t) {
				if (r.validateLayersElesOrdering(t, e), r.levelIsComplete(t, e)) return s = l[t], !0
			},
				i = function (e) {
					if (!s)
						for (var r = n + e; - 4 <= r && r <= 2 && !t(r); r += e);
				};
			i(1), i(-1);
			for (var a = c.length - 1; a >= 0; a--) {
				var o = c[a];
				o.invalid && Be(c, o)
			}
		}();
		var h = function (t) {
			var i = (t = t || {}).after;
			if (function () {
				if (!o) {
					o = pt();
					for (var t = 0; t < e.length; t++) n = o, r = e[t].boundingBox(), n.x1 = Math.min(n.x1, r.x1), n.x2 = Math.max(n.x2, r.x2), n.w = n.x2 - n.x1, n.y1 = Math.min(n.y1, r.y1), n.y2 = Math.max(n.y2, r.y2), n.h = n.y2 - n.y1
				}
				var n, r
			}(), o.w * u * (o.h * u) > 16e6) return null;
			var a = r.makeLayer(o, n);
			if (null != i) {
				var s = c.indexOf(i) + 1;
				c.splice(s, 0, a)
			} else (void 0 === t.insert || t.insert) && c.unshift(a);
			return a
		};
		if (r.skipping && !a) return null;
		for (var d = null, p = e.length / 1, f = !a, g = 0; g < e.length; g++) {
			var v = e[g],
				y = v._private.rscratch,
				m = y.imgLayerCaches = y.imgLayerCaches || {},
				b = m[n];
			if (b) d = b;
			else {
				if ((!d || d.eles.length >= p || !xt(d.bb, v.boundingBox())) && !(d = h({
					insert: !0,
					after: d
				}))) return null;
				s || f ? r.queueLayer(d, v) : r.drawEleInLayer(d, v, n, t), d.eles.push(v), m[n] = d
			}
		}
		return s || (f ? null : c)
	}, ps.getEleLevelForLayerLevel = function (e, t) {
		return e
	}, ps.drawEleInLayer = function (e, t, n, r) {
		var i = this.renderer,
			a = e.context,
			o = t.boundingBox();
		0 !== o.w && 0 !== o.h && t.visible() && (n = this.getEleLevelForLayerLevel(n, r), i.setImgSmoothing(a, !1), i.drawCachedElement(a, t, null, null, n, !0), i.setImgSmoothing(a, !0))
	}, ps.levelIsComplete = function (e, t) {
		var n = this.layersByLevel[e];
		if (!n || 0 === n.length) return !1;
		for (var r = 0, i = 0; i < n.length; i++) {
			var a = n[i];
			if (a.reqs > 0) return !1;
			if (a.invalid) return !1;
			r += a.eles.length
		}
		return r === t.length
	}, ps.validateLayersElesOrdering = function (e, t) {
		var n = this.layersByLevel[e];
		if (n)
			for (var r = 0; r < n.length; r++) {
				for (var i = n[r], a = -1, o = 0; o < t.length; o++)
					if (i.eles[0] === t[o]) {
						a = o;
						break
					}
				if (a < 0) this.invalidateLayer(i);
				else {
					var s = a;
					for (o = 0; o < i.eles.length; o++)
						if (i.eles[o] !== t[s + o]) {
							this.invalidateLayer(i);
							break
						}
				}
			}
	}, ps.updateElementsInLayers = function (e, t) {
		for (var n = b(e[0]), r = 0; r < e.length; r++)
			for (var i = n ? null : e[r], a = n ? e[r] : e[r].ele, o = a._private.rscratch, s = o.imgLayerCaches = o.imgLayerCaches || {}, l = -4; l <= 2; l++) {
				var u = s[l];
				u && (i && this.getEleLevelForLayerLevel(u.level) !== i.level || t(u, a, i))
			}
	}, ps.haveLayers = function () {
		for (var e = !1, t = -4; t <= 2; t++) {
			var n = this.layersByLevel[t];
			if (n && n.length > 0) {
				e = !0;
				break
			}
		}
		return e
	}, ps.invalidateElements = function (e) {
		var t = this;
		0 !== e.length && (t.lastInvalidationTime = se(), 0 !== e.length && t.haveLayers() && t.updateElementsInLayers(e, function (e, n, r) {
			t.invalidateLayer(e)
		}))
	}, ps.invalidateLayer = function (e) {
		if (this.lastInvalidationTime = se(), !e.invalid) {
			var t = e.level,
				n = e.eles,
				r = this.layersByLevel[t];
			Be(r, e), e.elesQueue = [], e.invalid = !0, e.replacement && (e.replacement.invalid = !0);
			for (var i = 0; i < n.length; i++) {
				var a = n[i]._private.rscratch.imgLayerCaches;
				a && (a[t] = null)
			}
		}
	}, ps.refineElementTextures = function (e) {
		var t = this;
		t.updateElementsInLayers(e, function (e, n, r) {
			var i = e.replacement;
			if (i || ((i = e.replacement = t.makeLayer(e.bb, e.level)).replaces = e, i.eles = e.eles), !i.reqs)
				for (var a = 0; a < i.eles.length; a++) t.queueLayer(i, i.eles[a])
		})
	}, ps.enqueueElementRefinement = function (e) {
		this.eleTxrDeqs.merge(e), this.scheduleElementRefinement()
	}, ps.queueLayer = function (e, t) {
		var n = this.layersQueue,
			r = e.elesQueue,
			i = r.hasId = r.hasId || {};
		if (!e.replacement) {
			if (t) {
				if (i[t.id()]) return;
				r.push(t), i[t.id()] = !0
			}
			e.reqs ? (e.reqs++ , n.updateItem(e)) : (e.reqs = 1, n.push(e))
		}
	}, ps.dequeue = function (e) {
		for (var t = this.layersQueue, n = [], r = 0; r < 1 && 0 !== t.size();) {
			var i = t.peek();
			if (i.replacement) t.pop();
			else if (i.replaces && i !== i.replaces.replacement) t.pop();
			else if (i.invalid) t.pop();
			else {
				var a = i.elesQueue.shift();
				a && (this.drawEleInLayer(i, a, i.level, e), r++), 0 === n.length && n.push(!0), 0 === i.elesQueue.length && (t.pop(), i.reqs = 0, i.replaces && this.applyLayerReplacement(i), this.requestRedraw())
			}
		}
		return n
	}, ps.applyLayerReplacement = function (e) {
		var t = this.layersByLevel[e.level],
			n = e.replaces,
			r = t.indexOf(n);
		if (!(r < 0 || n.invalid)) {
			t[r] = e;
			for (var i = 0; i < e.eles.length; i++) {
				var a = e.eles[i]._private,
					o = a.imgLayerCaches = a.imgLayerCaches || {};
				o && (o[e.level] = e)
			}
			this.requestRedraw()
		}
	}, ps.requestRedraw = ne(function () {
		var e = this.renderer;
		e.redrawHint("eles", !0), e.redrawHint("drag", !0), e.redraw()
	}, 100), ps.setupDequeueing = os({
		deqRedrawThreshold: 50,
		deqCost: .15,
		deqAvgCost: .1,
		deqNoDrawCost: .9,
		deqFastCost: .9,
		deq: function (e, t) {
			return e.dequeue(t)
		},
		onDeqd: we,
		shouldRedraw: me,
		priority: function (e) {
			return e.renderer.beforeRenderPriorities.lyrTxrDeq
		}
	});
	var vs, ys = {};

	function ms(e, t) {
		for (var n = 0; n < t.length; n++) {
			var r = t[n];
			e.lineTo(r.x, r.y)
		}
	}

	function bs(e, t, n) {
		for (var r, i = 0; i < t.length; i++) {
			var a = t[i];
			0 === i && (r = a), e.lineTo(a.x, a.y)
		}
		e.quadraticCurveTo(n.x, n.y, r.x, r.y)
	}

	function xs(e, t, n) {
		e.beginPath && e.beginPath();
		for (var r = t, i = 0; i < r.length; i++) {
			var a = r[i];
			e.lineTo(a.x, a.y)
		}
		var o = n,
			s = n[0];
		e.moveTo(s.x, s.y);
		for (i = 1; i < o.length; i++) {
			a = o[i];
			e.lineTo(a.x, a.y)
		}
		e.closePath && e.closePath()
	}

	function ws(e, t, n, r) {
		e.arc(t, n, r, 0, 2 * Math.PI, !1)
	}
	ys.arrowShapeImpl = function (e) {
		return (vs || (vs = {
			polygon: ms,
			"triangle-backcurve": bs,
			"triangle-tee": xs,
			"triangle-cross": xs,
			circle: ws
		}))[e]
	};
	var Es = {
		drawElement: function (e, t, n, r, i, a) {
			t.isNode() ? this.drawNode(e, t, n, r, i, a) : this.drawEdge(e, t, n, r, i, a)
		},
		drawElementOverlay: function (e, t) {
			t.isNode() ? this.drawNodeOverlay(e, t) : this.drawEdgeOverlay(e, t)
		},
		drawCachedElementPortion: function (e, t, n, r, i, a, o, s) {
			var l = this,
				u = n.getBoundingBox(t);
			if (0 !== u.w && 0 !== u.h) {
				var c = n.getElement(t, u, r, i, a);
				if (null != c) {
					var h = s(l, t);
					if (0 === h) return;
					var d, p, f, g, v, y, m = o(l, t),
						b = u.x1,
						x = u.y1,
						w = u.w,
						E = u.h;
					if (0 !== m) {
						var k = n.getRotationPoint(t);
						f = k.x, g = k.y, e.translate(f, g), e.rotate(m), (v = l.getImgSmoothing(e)) || l.setImgSmoothing(e, !0);
						var C = n.getRotationOffset(t);
						d = C.x, p = C.y
					} else d = b, p = x;
					1 !== h && (y = e.globalAlpha, e.globalAlpha = y * h), e.drawImage(c.texture.canvas, c.x, 0, c.width, c.height, d, p, w, E), 1 !== h && (e.globalAlpha = y), 0 !== m && (e.rotate(-m), e.translate(-f, -g), v || l.setImgSmoothing(e, !1))
				} else n.drawElement(e, t)
			}
		}
	},
		ks = function () {
			return 0
		},
		Cs = function (e, t) {
			return e.getTextAngle(t, null)
		},
		Ss = function (e, t) {
			return e.getTextAngle(t, "source")
		},
		Ds = function (e, t) {
			return e.getTextAngle(t, "target")
		},
		Ps = function (e, t) {
			return t.effectiveOpacity()
		},
		Ts = function (e, t) {
			return t.pstyle("text-opacity").pfValue * t.effectiveOpacity()
		};
	Es.drawCachedElement = function (e, t, n, r, i, a) {
		var o = this,
			s = o.data,
			l = s.eleTxrCache,
			u = s.lblTxrCache,
			c = s.slbTxrCache,
			h = s.tlbTxrCache,
			d = t.boundingBox(),
			p = !0 === a ? l.reasons.highQuality : null;
		0 !== d.w && 0 !== d.h && t.visible() && (r && !mt(d, r) || (o.drawCachedElementPortion(e, t, l, n, i, p, ks, Ps), o.drawCachedElementPortion(e, t, u, n, i, p, Cs, Ts), t.isEdge() && (o.drawCachedElementPortion(e, t, c, n, i, p, Ss, Ts), o.drawCachedElementPortion(e, t, h, n, i, p, Ds, Ts)), o.drawElementOverlay(e, t)))
	}, Es.drawElements = function (e, t) {
		for (var n = 0; n < t.length; n++) {
			var r = t[n];
			this.drawElement(e, r)
		}
	}, Es.drawCachedElements = function (e, t, n, r) {
		for (var i = 0; i < t.length; i++) {
			var a = t[i];
			this.drawCachedElement(e, a, n, r)
		}
	}, Es.drawCachedNodes = function (e, t, n, r) {
		for (var i = 0; i < t.length; i++) {
			var a = t[i];
			a.isNode() && this.drawCachedElement(e, a, n, r)
		}
	}, Es.drawLayeredElements = function (e, t, n, r) {
		var i = this.data.lyrTxrCache.getLayers(t, n);
		if (i)
			for (var a = 0; a < i.length; a++) {
				var o = i[a],
					s = o.bb;
				0 !== s.w && 0 !== s.h && e.drawImage(o.canvas, s.x1, s.y1, s.w, s.h)
			} else this.drawCachedElements(e, t, n, r)
	};
	var Ms = {
		drawEdge: function (e, t, n) {
			var r = !(arguments.length > 3 && void 0 !== arguments[3]) || arguments[3],
				i = !(arguments.length > 4 && void 0 !== arguments[4]) || arguments[4],
				a = !(arguments.length > 5 && void 0 !== arguments[5]) || arguments[5],
				o = this,
				s = t._private.rscratch;
			if ((!a || t.visible()) && !s.badLine && null != s.allpts && !isNaN(s.allpts[0])) {
				var l;
				n && (l = n, e.translate(-l.x1, -l.y1));
				var u = a ? t.pstyle("opacity").value : 1,
					c = t.pstyle("line-style").value,
					h = t.pstyle("width").pfValue,
					d = t.pstyle("line-cap").value,
					p = function () {
						var n = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : u;
						e.lineWidth = h, e.lineCap = d, o.eleStrokeStyle(e, t, n), o.drawEdgePath(t, e, s.allpts, c), e.lineCap = "butt"
					},
					f = function () {
						var n = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : u;
						o.drawArrowheads(e, t, n)
					};
				if (e.lineJoin = "round", "yes" === t.pstyle("ghost").value) {
					var g = t.pstyle("ghost-offset-x").pfValue,
						v = t.pstyle("ghost-offset-y").pfValue,
						y = t.pstyle("ghost-opacity").value,
						m = u * y;
					e.translate(g, v), p(m), f(m), e.translate(-g, -v)
				}
				p(), f(), i && o.drawEdgeOverlay(e, t), o.drawElementText(e, t, null, r), n && e.translate(l.x1, l.y1)
			}
		},
		drawEdgeOverlay: function (e, t) {
			if (t.visible()) {
				var n = t.pstyle("overlay-opacity").value;
				if (0 !== n) {
					var r = this,
						i = r.usePaths(),
						a = t._private.rscratch,
						o = 2 * t.pstyle("overlay-padding").pfValue,
						s = t.pstyle("overlay-color").value;
					e.lineWidth = o, "self" !== a.edgeType || i ? e.lineCap = "round" : e.lineCap = "butt", r.colorStrokeStyle(e, s[0], s[1], s[2], n), r.drawEdgePath(t, e, a.allpts, "solid")
				}
			}
		},
		drawEdgePath: function (e, t, n, r) {
			var i, a = e._private.rscratch,
				o = t,
				s = !1,
				l = this.usePaths(),
				u = e.pstyle("line-dash-pattern").pfValue,
				c = e.pstyle("line-dash-offset").pfValue;
			if (l) {
				var h = n.join("$");
				a.pathCacheKey && a.pathCacheKey === h ? (i = t = a.pathCache, s = !0) : (i = t = new Path2D, a.pathCacheKey = h, a.pathCache = i)
			}
			if (o.setLineDash) switch (r) {
				case "dotted":
					o.setLineDash([1, 1]);
					break;
				case "dashed":
					o.setLineDash(u), o.lineDashOffset = c;
					break;
				case "solid":
					o.setLineDash([])
			}
			if (!s && !a.badLine) switch (t.beginPath && t.beginPath(), t.moveTo(n[0], n[1]), a.edgeType) {
				case "bezier":
				case "self":
				case "compound":
				case "multibezier":
					for (var d = 2; d + 3 < n.length; d += 4) t.quadraticCurveTo(n[d], n[d + 1], n[d + 2], n[d + 3]);
					break;
				case "straight":
				case "segments":
				case "haystack":
					for (var p = 2; p + 1 < n.length; p += 2) t.lineTo(n[p], n[p + 1])
			}
			t = o, l ? t.stroke(i) : t.stroke(), t.setLineDash && t.setLineDash([])
		},
		drawArrowheads: function (e, t, n) {
			var r = t._private.rscratch,
				i = "haystack" === r.edgeType;
			i || this.drawArrowhead(e, t, "source", r.arrowStartX, r.arrowStartY, r.srcArrowAngle, n), this.drawArrowhead(e, t, "mid-target", r.midX, r.midY, r.midtgtArrowAngle, n), this.drawArrowhead(e, t, "mid-source", r.midX, r.midY, r.midsrcArrowAngle, n), i || this.drawArrowhead(e, t, "target", r.arrowEndX, r.arrowEndY, r.tgtArrowAngle, n)
		},
		drawArrowhead: function (e, t, n, r, i, a, o) {
			if (!(isNaN(r) || null == r || isNaN(i) || null == i || isNaN(a) || null == a)) {
				var s = t.pstyle(n + "-arrow-shape").value;
				if ("none" !== s) {
					var l = "hollow" === t.pstyle(n + "-arrow-fill").value ? "both" : "filled",
						u = t.pstyle(n + "-arrow-fill").value,
						c = t.pstyle("width").pfValue,
						h = t.pstyle("opacity").value;
					void 0 === o && (o = h);
					var d = e.globalCompositeOperation;
					1 === o && "hollow" !== u || (e.globalCompositeOperation = "destination-out", this.colorFillStyle(e, 255, 255, 255, 1), this.colorStrokeStyle(e, 255, 255, 255, 1), this.drawArrowShape(t, e, l, c, s, r, i, a), e.globalCompositeOperation = d);
					var p = t.pstyle(n + "-arrow-color").value;
					this.colorFillStyle(e, p[0], p[1], p[2], o), this.colorStrokeStyle(e, p[0], p[1], p[2], o), this.drawArrowShape(t, e, u, c, s, r, i, a)
				}
			}
		},
		drawArrowShape: function (e, t, n, r, i, a, o, s) {
			var l, u = this,
				c = this.usePaths() && "triangle-cross" !== i,
				h = !1,
				d = t,
				p = {
					x: a,
					y: o
				},
				f = e.pstyle("arrow-scale").value,
				g = this.getArrowWidth(r, f),
				v = u.arrowShapes[i];
			if (c) {
				var y = u.arrowPathCache = u.arrowPathCache || [],
					m = he(i),
					b = y[m];
				null != b ? (l = t = b, h = !0) : (l = t = new Path2D, y[m] = l)
			}
			t.beginPath && t.beginPath(), h || (c ? v.draw(t, 1, 0, {
				x: 0,
				y: 0
			}, 1) : v.draw(t, g, s, p, r)), t.closePath && t.closePath(), t = d, c && (t.translate(a, o), t.rotate(s), t.scale(g, g)), "filled" !== n && "both" !== n || (c ? t.fill(l) : t.fill()), "hollow" !== n && "both" !== n || (t.lineWidth = (v.matchEdgeWidth ? r : 1) / (c ? g : 1), t.lineJoin = "miter", c ? t.stroke(l) : t.stroke()), c && (t.scale(1 / g, 1 / g), t.rotate(-s), t.translate(-a, -o))
		}
	},
		Bs = {
			safeDrawImage: function (e, t, n, r, i, a, o, s, l, u) {
				i <= 0 || a <= 0 || l <= 0 || u <= 0 || e.drawImage(t, n, r, i, a, o, s, l, u)
			},
			drawInscribedImage: function (e, t, n, r, i) {
				var a = this,
					o = n.position(),
					s = o.x,
					l = o.y,
					u = n.cy().style(),
					c = u.getIndexedStyle.bind(u),
					h = c(n, "background-fit", "value", r),
					d = c(n, "background-repeat", "value", r),
					p = n.width(),
					f = n.height(),
					g = 2 * n.padding(),
					v = p + ("inner" === c(n, "background-width-relative-to", "value", r) ? 0 : g),
					y = f + ("inner" === c(n, "background-height-relative-to", "value", r) ? 0 : g),
					m = n._private.rscratch,
					b = "node" === c(n, "background-clip", "value", r),
					x = c(n, "background-image-opacity", "value", r) * i,
					w = t.width || t.cachedW,
					E = t.height || t.cachedH;
				null != w && null != E || (document.body.appendChild(t), w = t.cachedW = t.width || t.offsetWidth, E = t.cachedH = t.height || t.offsetHeight, document.body.removeChild(t));
				var k = w,
					C = E;
				if ("auto" !== c(n, "background-width", "value", r) && (k = "%" === c(n, "background-width", "units", r) ? c(n, "background-width", "pfValue", r) * v : c(n, "background-width", "pfValue", r)), "auto" !== c(n, "background-height", "value", r) && (C = "%" === c(n, "background-height", "units", r) ? c(n, "background-height", "pfValue", r) * y : c(n, "background-height", "pfValue", r)), 0 !== k && 0 !== C) {
					if ("contain" === h) k *= S = Math.min(v / k, y / C), C *= S;
					else if ("cover" === h) {
						var S;
						k *= S = Math.max(v / k, y / C), C *= S
					}
					var D = s - v / 2,
						P = c(n, "background-position-x", "units", r),
						T = c(n, "background-position-x", "pfValue", r);
					D += "%" === P ? (v - k) * T : T;
					var M = c(n, "background-offset-x", "units", r),
						B = c(n, "background-offset-x", "pfValue", r);
					D += "%" === M ? (v - k) * B : B;
					var _ = l - y / 2,
						N = c(n, "background-position-y", "units", r),
						I = c(n, "background-position-y", "pfValue", r);
					_ += "%" === N ? (y - C) * I : I;
					var z = c(n, "background-offset-y", "units", r),
						L = c(n, "background-offset-y", "pfValue", r);
					_ += "%" === z ? (y - C) * L : L, m.pathCache && (D -= s, _ -= l, s = 0, l = 0);
					var A = e.globalAlpha;
					if (e.globalAlpha = x, "no-repeat" === d) b && (e.save(), m.pathCache ? e.clip(m.pathCache) : (a.nodeShapes[a.getNodeShape(n)].draw(e, s, l, v, y), e.clip())), a.safeDrawImage(e, t, 0, 0, w, E, D, _, k, C), b && e.restore();
					else {
						var O = e.createPattern(t, d);
						e.fillStyle = O, a.nodeShapes[a.getNodeShape(n)].draw(e, s, l, v, y), e.translate(D, _), e.fill(), e.translate(-D, -_)
					}
					e.globalAlpha = A
				}
			}
		},
		_s = {};
	_s.eleTextBiggerThanMin = function (e, t) {
		if (!t) {
			var n = e.cy().zoom(),
				r = this.getPixelRatio(),
				i = Math.ceil(at(n * r));
			t = Math.pow(2, i)
		}
		return !(e.pstyle("font-size").pfValue * t < e.pstyle("min-zoomed-font-size").pfValue)
	}, _s.drawElementText = function (e, t, n, r, i) {
		var a = !(arguments.length > 5 && void 0 !== arguments[5]) || arguments[5],
			o = this;
		if (null == r) {
			if (a && !o.eleTextBiggerThanMin(t)) return
		} else if (!1 === r) return;
		if (t.isNode()) {
			var s = t.pstyle("label");
			if (!s || !s.value) return;
			var l = o.getLabelJustification(t);
			e.textAlign = l, e.textBaseline = "bottom"
		} else {
			var u = t.pstyle("label"),
				c = t.pstyle("source-label"),
				h = t.pstyle("target-label");
			if (!(u && u.value || c && c.value || h && h.value)) return;
			e.textAlign = "center", e.textBaseline = "bottom"
		}
		var d, p = !n;
		n && (d = n, e.translate(-d.x1, -d.y1)), null == i ? (o.drawText(e, t, null, p, a), t.isEdge() && (o.drawText(e, t, "source", p, a), o.drawText(e, t, "target", p, a))) : o.drawText(e, t, i, p, a), n && e.translate(d.x1, d.y1)
	}, _s.getFontCache = function (e) {
		var t;
		this.fontCaches = this.fontCaches || [];
		for (var n = 0; n < this.fontCaches.length; n++)
			if ((t = this.fontCaches[n]).context === e) return t;
		return t = {
			context: e
		}, this.fontCaches.push(t), t
	}, _s.setupTextStyle = function (e, t) {
		var n = !(arguments.length > 2 && void 0 !== arguments[2]) || arguments[2],
			r = t.pstyle("font-style").strValue,
			i = t.pstyle("font-size").pfValue + "px",
			a = t.pstyle("font-family").strValue,
			o = t.pstyle("font-weight").strValue,
			s = n ? t.effectiveOpacity() * t.pstyle("text-opacity").value : 1,
			l = t.pstyle("text-outline-opacity").value * s,
			u = t.pstyle("color").value,
			c = t.pstyle("text-outline-color").value;
		e.font = r + " " + o + " " + i + " " + a, e.lineJoin = "round", this.colorFillStyle(e, u[0], u[1], u[2], s), this.colorStrokeStyle(e, c[0], c[1], c[2], l)
	}, _s.getTextAngle = function (e, t) {
		var n = e._private.rscratch,
			r = t ? t + "-" : "",
			i = e.pstyle(r + "text-rotation"),
			a = Ne(n, "labelAngle", t);
		return "autorotate" === i.strValue ? e.isEdge() ? a : 0 : "none" === i.strValue ? 0 : i.pfValue
	}, _s.drawText = function (e, t, n) {
		var r = !(arguments.length > 3 && void 0 !== arguments[3]) || arguments[3],
			i = !(arguments.length > 4 && void 0 !== arguments[4]) || arguments[4],
			a = t._private.rscratch,
			o = i ? t.effectiveOpacity() : 1;
		if (!i || 0 !== o && 0 !== t.pstyle("text-opacity").value) {
			"main" === n && (n = null);
			var s, l, u = Ne(a, "labelX", n),
				c = Ne(a, "labelY", n),
				h = this.getLabelText(t, n);
			if (null != h && "" !== h && !isNaN(u) && !isNaN(c)) {
				this.setupTextStyle(e, t, i);
				var d, p = n ? n + "-" : "",
					f = Ne(a, "labelWidth", n),
					g = Ne(a, "labelHeight", n),
					v = t.pstyle(p + "text-margin-x").pfValue,
					y = t.pstyle(p + "text-margin-y").pfValue,
					m = t.isEdge(),
					b = t.pstyle("text-halign").value,
					x = t.pstyle("text-valign").value;
				switch (m && (b = "center", x = "center"), u += v, c += y, 0 !== (d = r ? this.getTextAngle(t, n) : 0) && (s = u, l = c, e.translate(s, l), e.rotate(d), u = 0, c = 0), x) {
					case "top":
						break;
					case "center":
						c += g / 2;
						break;
					case "bottom":
						c += g
				}
				var w = t.pstyle("text-background-opacity").value,
					E = t.pstyle("text-border-opacity").value,
					k = t.pstyle("text-border-width").pfValue,
					C = t.pstyle("text-background-padding").pfValue;
				if (w > 0 || k > 0 && E > 0) {
					var S = u - C;
					switch (b) {
						case "left":
							S -= f;
							break;
						case "center":
							S -= f / 2
					}
					var D = c - g - C,
						P = f + 2 * C,
						T = g + 2 * C;
					if (w > 0) {
						var M = e.fillStyle,
							B = t.pstyle("text-background-color").value;
						e.fillStyle = "rgba(" + B[0] + "," + B[1] + "," + B[2] + "," + w * o + ")", "roundrectangle" == t.pstyle("text-background-shape").strValue ? function (e, t, n, r, i) {
							var a = arguments.length > 5 && void 0 !== arguments[5] ? arguments[5] : 5;
							e.beginPath(), e.moveTo(t + a, n), e.lineTo(t + r - a, n), e.quadraticCurveTo(t + r, n, t + r, n + a), e.lineTo(t + r, n + i - a), e.quadraticCurveTo(t + r, n + i, t + r - a, n + i), e.lineTo(t + a, n + i), e.quadraticCurveTo(t, n + i, t, n + i - a), e.lineTo(t, n + a), e.quadraticCurveTo(t, n, t + a, n), e.closePath(), e.fill()
						}(e, S, D, P, T, 2) : e.fillRect(S, D, P, T), e.fillStyle = M
					}
					if (k > 0 && E > 0) {
						var _ = e.strokeStyle,
							N = e.lineWidth,
							I = t.pstyle("text-border-color").value,
							z = t.pstyle("text-border-style").value;
						if (e.strokeStyle = "rgba(" + I[0] + "," + I[1] + "," + I[2] + "," + E * o + ")", e.lineWidth = k, e.setLineDash) switch (z) {
							case "dotted":
								e.setLineDash([1, 1]);
								break;
							case "dashed":
								e.setLineDash([4, 2]);
								break;
							case "double":
								e.lineWidth = k / 4, e.setLineDash([]);
								break;
							case "solid":
								e.setLineDash([])
						}
						if (e.strokeRect(S, D, P, T), "double" === z) {
							var L = k / 2;
							e.strokeRect(S + L, D + L, P - 2 * L, T - 2 * L)
						}
						e.setLineDash && e.setLineDash([]), e.lineWidth = N, e.strokeStyle = _
					}
				}
				var A = 2 * t.pstyle("text-outline-width").pfValue;
				if (A > 0 && (e.lineWidth = A), "wrap" === t.pstyle("text-wrap").value) {
					var O = Ne(a, "labelWrapCachedLines", n),
						R = Ne(a, "labelLineHeight", n),
						V = f / 2,
						F = this.getLabelJustification(t);
					switch ("auto" === F || ("left" === b ? "left" === F ? u += -f : "center" === F && (u += -V) : "center" === b ? "left" === F ? u += -V : "right" === F && (u += V) : "right" === b && ("center" === F ? u += V : "right" === F && (u += f))), x) {
						case "top":
							c -= (O.length - 1) * R;
							break;
						case "center":
						case "bottom":
							c -= (O.length - 1) * R
					}
					for (var q = 0; q < O.length; q++) A > 0 && e.strokeText(O[q], u, c), e.fillText(O[q], u, c), c += R
				} else A > 0 && e.strokeText(h, u, c), e.fillText(h, u, c);
				0 !== d && (e.rotate(-d), e.translate(-s, -l))
			}
		}
	};
	var Ns = {
		drawNode: function (e, t, n) {
			var r, i, a = !(arguments.length > 3 && void 0 !== arguments[3]) || arguments[3],
				o = !(arguments.length > 4 && void 0 !== arguments[4]) || arguments[4],
				s = !(arguments.length > 5 && void 0 !== arguments[5]) || arguments[5],
				l = this,
				u = t._private,
				c = u.rscratch,
				h = t.position();
			if (v(h.x) && v(h.y) && (!s || t.visible())) {
				var d, p, f = s ? t.effectiveOpacity() : 1,
					g = l.usePaths(),
					y = !1,
					m = t.padding();
				r = t.width() + 2 * m, i = t.height() + 2 * m, n && (p = n, e.translate(-p.x1, -p.y1));
				for (var b = t.pstyle("background-image").value, x = new Array(b.length), w = new Array(b.length), E = 0, k = 0; k < b.length; k++) {
					var C = b[k];
					if (x[k] = null != C && "none" !== C) {
						var S = t.cy().style().getIndexedStyle(t, "background-image-crossorigin", "value", k);
						E++ , w[k] = l.getCachedImage(C, S, function () {
							u.backgroundTimestamp = Date.now(), t.emitAndNotify("background")
						})
					}
				}
				var D = t.pstyle("background-blacken").value,
					P = t.pstyle("border-width").pfValue,
					T = t.pstyle("background-opacity").value * f,
					M = t.pstyle("border-color").value,
					B = t.pstyle("border-style").value,
					_ = t.pstyle("border-opacity").value * f;
				e.lineJoin = "miter";
				var N = function () {
					var n = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : T;
					l.eleFillStyle(e, t, n)
				},
					I = function () {
						var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : _;
						l.colorStrokeStyle(e, M[0], M[1], M[2], t)
					},
					z = t.pstyle("shape").strValue,
					L = t.pstyle("shape-polygon-points").pfValue;
				if (g) {
					e.translate(h.x, h.y);
					var A = l.nodePathCache = l.nodePathCache || [],
						O = de("polygon" === z ? z + "," + L.join(",") : z, "" + i, "" + r),
						R = A[O];
					null != R ? (d = R, y = !0, c.pathCache = d) : (d = new Path2D, A[O] = c.pathCache = d)
				}
				var V = function () {
					if (!y) {
						var n = h;
						g && (n = {
							x: 0,
							y: 0
						}), l.nodeShapes[l.getNodeShape(t)].draw(d || e, n.x, n.y, r, i)
					}
					g ? e.fill(d) : e.fill()
				},
					F = function () {
						for (var n = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : f, r = u.backgrounding, i = 0, a = 0; a < w.length; a++) x[a] && w[a].complete && !w[a].error && (i++ , l.drawInscribedImage(e, w[a], t, a, n));
						u.backgrounding = !(i === E), r !== u.backgrounding && t.updateStyle(!1)
					},
					q = function () {
						var n = arguments.length > 0 && void 0 !== arguments[0] && arguments[0],
							a = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : f;
						l.hasPie(t) && (l.drawPie(e, t, a), n && (g || l.nodeShapes[l.getNodeShape(t)].draw(e, h.x, h.y, r, i)))
					},
					Y = function () {
						var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : f,
							n = (D > 0 ? D : -D) * t,
							r = D > 0 ? 0 : 255;
						0 !== D && (l.colorFillStyle(e, r, r, r, n), g ? e.fill(d) : e.fill())
					},
					j = function () {
						if (P > 0) {
							if (e.lineWidth = P, e.lineCap = "butt", e.setLineDash) switch (B) {
								case "dotted":
									e.setLineDash([1, 1]);
									break;
								case "dashed":
									e.setLineDash([4, 2]);
									break;
								case "solid":
								case "double":
									e.setLineDash([])
							}
							if (g ? e.stroke(d) : e.stroke(), "double" === B) {
								e.lineWidth = P / 3;
								var t = e.globalCompositeOperation;
								e.globalCompositeOperation = "destination-out", g ? e.stroke(d) : e.stroke(), e.globalCompositeOperation = t
							}
							e.setLineDash && e.setLineDash([])
						}
					};
				if ("yes" === t.pstyle("ghost").value) {
					var X = t.pstyle("ghost-offset-x").pfValue,
						W = t.pstyle("ghost-offset-y").pfValue,
						H = t.pstyle("ghost-opacity").value,
						K = H * f;
					e.translate(X, W), N(H * T), V(), F(K), q(0 !== D || 0 !== P), Y(K), I(H * _), j(), e.translate(-X, -W)
				}
				N(), V(), F(), q(0 !== D || 0 !== P), Y(), I(), j(), g && e.translate(-h.x, -h.y), l.drawElementText(e, t, null, a), o && l.drawNodeOverlay(e, t, h, r, i), n && e.translate(p.x1, p.y1)
			}
		},
		drawNodeOverlay: function (e, t, n, r, i) {
			if (t.visible()) {
				var a = t.pstyle("overlay-padding").pfValue,
					o = t.pstyle("overlay-opacity").value,
					s = t.pstyle("overlay-color").value;
				if (o > 0) {
					if (n = n || t.position(), null == r || null == i) {
						var l = t.padding();
						r = t.width() + 2 * l, i = t.height() + 2 * l
					}
					this.colorFillStyle(e, s[0], s[1], s[2], o), this.nodeShapes.roundrectangle.draw(e, n.x, n.y, r + 2 * a, i + 2 * a), e.fill()
				}
			}
		},
		hasPie: function (e) {
			return (e = e[0])._private.hasPie
		},
		drawPie: function (e, t, n, r) {
			t = t[0], r = r || t.position();
			var i = t.cy().style(),
				a = t.pstyle("pie-size"),
				o = r.x,
				s = r.y,
				l = t.width(),
				u = t.height(),
				c = Math.min(l, u) / 2,
				h = 0;
			this.usePaths() && (o = 0, s = 0), "%" === a.units ? c *= a.pfValue : void 0 !== a.pfValue && (c = a.pfValue / 2);
			for (var d = 1; d <= i.pieBackgroundN; d++) {
				var p = t.pstyle("pie-" + d + "-background-size").value,
					f = t.pstyle("pie-" + d + "-background-color").value,
					g = t.pstyle("pie-" + d + "-background-opacity").value * n,
					v = p / 100;
				v + h > 1 && (v = 1 - h);
				var y = 1.5 * Math.PI + 2 * Math.PI * h,
					m = y + 2 * Math.PI * v;
				0 === p || h >= 1 || h + v > 1 || (e.beginPath(), e.moveTo(o, s), e.arc(o, s, c, y, m), e.closePath(), this.colorFillStyle(e, f[0], f[1], f[2], g), e.fill(), h += v)
			}
		}
	},
		Is = {};
	Is.getPixelRatio = function () {
		var e = this.data.contexts[0];
		if (null != this.forcedPixelRatio) return this.forcedPixelRatio;
		var t = e.backingStorePixelRatio || e.webkitBackingStorePixelRatio || e.mozBackingStorePixelRatio || e.msBackingStorePixelRatio || e.oBackingStorePixelRatio || e.backingStorePixelRatio || 1;
		return (window.devicePixelRatio || 1) / t
	}, Is.paintCache = function (e) {
		for (var t, n = this.paintCaches = this.paintCaches || [], r = !0, i = 0; i < n.length; i++)
			if ((t = n[i]).context === e) {
				r = !1;
				break
			}
		return r && (t = {
			context: e
		}, n.push(t)), t
	}, Is.createGradientStyleFor = function (e, t, n, r, i) {
		var a, o = this.usePaths(),
			s = n.pstyle(t + "-gradient-stop-colors").value,
			l = n.pstyle(t + "-gradient-stop-positions").pfValue;
		if ("radial-gradient" === r)
			if (n.isEdge()) {
				var u = n.sourceEndpoint(),
					c = n.targetEndpoint(),
					h = n.midpoint(),
					d = st(u, h),
					p = st(c, h);
				a = e.createRadialGradient(h.x, h.y, 0, h.x, h.y, Math.max(d, p))
			} else {
				var f = o ? {
					x: 0,
					y: 0
				} : n.position(),
					g = n.width(),
					v = n.height();
				a = e.createRadialGradient(f.x, f.y, 0, f.x, f.y, Math.max(g, v))
			} else if (n.isEdge()) {
				var y = n.sourceEndpoint(),
					m = n.targetEndpoint();
				a = e.createLinearGradient(y.x, y.y, m.x, m.y)
			} else {
			var b = o ? {
				x: 0,
				y: 0
			} : n.position(),
				x = n.width() / 2,
				w = n.height() / 2;
			switch (n.pstyle("background-gradient-direction").value) {
				case "to-bottom":
					a = e.createLinearGradient(b.x, b.y - w, b.x, b.y + w);
					break;
				case "to-top":
					a = e.createLinearGradient(b.x, b.y + w, b.x, b.y - w);
					break;
				case "to-left":
					a = e.createLinearGradient(b.x - x, b.y, b.x + x, b.y);
					break;
				case "to-right":
					a = e.createLinearGradient(b.x + x, b.y, b.x - x, b.y);
					break;
				case "to-bottom-right":
				case "to-right-bottom":
					a = e.createLinearGradient(b.x - x, b.y - w, b.x + x, b.y + w);
					break;
				case "to-top-right":
				case "to-right-top":
					a = e.createLinearGradient(b.x - x, b.y + w, b.x + x, b.y - w);
					break;
				case "to-bottom-left":
				case "to-left-bottom":
					a = e.createLinearGradient(b.x + x, b.y - w, b.x - x, b.y + w);
					break;
				case "to-top-left":
				case "to-left-top":
					a = e.createLinearGradient(b.x + x, b.y + w, b.x - x, b.y - w)
			}
		} if (!a) return null;
		for (var E = l.length === s.length, k = s.length, C = 0; C < k; C++) a.addColorStop(E ? l[C] : C / (k - 1), "rgba(" + s[C][0] + "," + s[C][1] + "," + s[C][2] + "," + i + ")");
		return a
	}, Is.gradientFillStyle = function (e, t, n, r) {
		var i = this.createGradientStyleFor(e, "background", t, n, r);
		if (!i) return null;
		e.fillStyle = i
	}, Is.colorFillStyle = function (e, t, n, r, i) {
		e.fillStyle = "rgba(" + t + "," + n + "," + r + "," + i + ")"
	}, Is.eleFillStyle = function (e, t, n) {
		var r = t.pstyle("background-fill").value;
		if ("linear-gradient" === r || "radial-gradient" === r) this.gradientFillStyle(e, t, r, n);
		else {
			var i = t.pstyle("background-color").value;
			this.colorFillStyle(e, i[0], i[1], i[2], n)
		}
	}, Is.gradientStrokeStyle = function (e, t, n, r) {
		var i = this.createGradientStyleFor(e, "line", t, n, r);
		if (!i) return null;
		e.strokeStyle = i
	}, Is.colorStrokeStyle = function (e, t, n, r, i) {
		e.strokeStyle = "rgba(" + t + "," + n + "," + r + "," + i + ")"
	}, Is.eleStrokeStyle = function (e, t, n) {
		var r = t.pstyle("line-fill").value;
		if ("linear-gradient" === r || "radial-gradient" === r) this.gradientStrokeStyle(e, t, r, n);
		else {
			var i = t.pstyle("line-color").value;
			this.colorStrokeStyle(e, i[0], i[1], i[2], n)
		}
	}, Is.matchCanvasSize = function (e) {
		var t = this,
			n = t.data,
			r = t.findContainerClientCoords(),
			i = r[2],
			a = r[3],
			o = t.getPixelRatio(),
			s = t.motionBlurPxRatio;
		e !== t.data.bufferCanvases[t.MOTIONBLUR_BUFFER_NODE] && e !== t.data.bufferCanvases[t.MOTIONBLUR_BUFFER_DRAG] || (o = s);
		var l, u = i * o,
			c = a * o;
		if (u !== t.canvasWidth || c !== t.canvasHeight) {
			t.fontCaches = null;
			var h = n.canvasContainer;
			h.style.width = i + "px", h.style.height = a + "px";
			for (var d = 0; d < t.CANVAS_LAYERS; d++)(l = n.canvases[d]).width = u, l.height = c, l.style.width = i + "px", l.style.height = a + "px";
			for (d = 0; d < t.BUFFER_COUNT; d++)(l = n.bufferCanvases[d]).width = u, l.height = c, l.style.width = i + "px", l.style.height = a + "px";
			t.textureMult = 1, o <= 1 && (l = n.bufferCanvases[t.TEXTURE_BUFFER], t.textureMult = 2, l.width = u * t.textureMult, l.height = c * t.textureMult), t.canvasWidth = u, t.canvasHeight = c
		}
	}, Is.renderTo = function (e, t, n, r) {
		this.render({
			forcedContext: e,
			forcedZoom: t,
			forcedPan: n,
			drawAllLayers: !0,
			forcedPxRatio: r
		})
	}, Is.render = function (e) {
		var t = (e = e || Te()).forcedContext,
			n = e.drawAllLayers,
			r = e.drawOnlyNodeLayer,
			i = e.forcedZoom,
			a = e.forcedPan,
			o = this,
			s = void 0 === e.forcedPxRatio ? this.getPixelRatio() : e.forcedPxRatio,
			l = o.cy,
			u = o.data,
			c = u.canvasNeedsRedraw,
			h = o.textureOnViewport && !t && (o.pinching || o.hoverData.dragging || o.swipePanning || o.data.wheelZooming),
			d = void 0 !== e.motionBlur ? e.motionBlur : o.motionBlur,
			p = o.motionBlurPxRatio,
			f = l.hasCompoundNodes(),
			g = o.hoverData.draggingEles,
			v = !(!o.hoverData.selecting && !o.touchData.selecting),
			y = d = d && !t && o.motionBlurEnabled && !v;
		t || (o.prevPxRatio !== s && (o.invalidateContainerClientCoordsCache(), o.matchCanvasSize(o.container), o.redrawHint("eles", !0), o.redrawHint("drag", !0)), o.prevPxRatio = s), !t && o.motionBlurTimeout && clearTimeout(o.motionBlurTimeout), d && (null == o.mbFrames && (o.mbFrames = 0), o.mbFrames++ , o.mbFrames < 3 && (y = !1), o.mbFrames > o.minMbLowQualFrames && (o.motionBlurPxRatio = o.mbPxRBlurry)), o.clearingMotionBlur && (o.motionBlurPxRatio = 1), o.textureDrawLastFrame && !h && (c[o.NODE] = !0, c[o.SELECT_BOX] = !0);
		var m = l.style(),
			b = l.zoom(),
			x = void 0 !== i ? i : b,
			w = l.pan(),
			E = {
				x: w.x,
				y: w.y
			},
			k = {
				zoom: b,
				pan: {
					x: w.x,
					y: w.y
				}
			},
			C = o.prevViewport;
		void 0 === C || k.zoom !== C.zoom || k.pan.x !== C.pan.x || k.pan.y !== C.pan.y || g && !f || (o.motionBlurPxRatio = 1), a && (E = a), x *= s, E.x *= s, E.y *= s;
		var S = o.getCachedZSortedEles();

		function D(e, t, n, r, i) {
			var a = e.globalCompositeOperation;
			e.globalCompositeOperation = "destination-out", o.colorFillStyle(e, 255, 255, 255, o.motionBlurTransparency), e.fillRect(t, n, r, i), e.globalCompositeOperation = a
		}

		function P(e, r) {
			var s, l, c, h;
			o.clearingMotionBlur || e !== u.bufferContexts[o.MOTIONBLUR_BUFFER_NODE] && e !== u.bufferContexts[o.MOTIONBLUR_BUFFER_DRAG] ? (s = E, l = x, c = o.canvasWidth, h = o.canvasHeight) : (s = {
				x: w.x * p,
				y: w.y * p
			}, l = b * p, c = o.canvasWidth * p, h = o.canvasHeight * p), e.setTransform(1, 0, 0, 1, 0, 0), "motionBlur" === r ? D(e, 0, 0, c, h) : t || void 0 !== r && !r || e.clearRect(0, 0, c, h), n || (e.translate(s.x, s.y), e.scale(l, l)), a && e.translate(a.x, a.y), i && e.scale(i, i)
		}
		if (h || (o.textureDrawLastFrame = !1), h) {
			if (o.textureDrawLastFrame = !0, !o.textureCache) {
				o.textureCache = {}, o.textureCache.bb = l.mutableElements().boundingBox(), o.textureCache.texture = o.data.bufferCanvases[o.TEXTURE_BUFFER];
				var T = o.data.bufferContexts[o.TEXTURE_BUFFER];
				T.setTransform(1, 0, 0, 1, 0, 0), T.clearRect(0, 0, o.canvasWidth * o.textureMult, o.canvasHeight * o.textureMult), o.render({
					forcedContext: T,
					drawOnlyNodeLayer: !0,
					forcedPxRatio: s * o.textureMult
				}), (k = o.textureCache.viewport = {
					zoom: l.zoom(),
					pan: l.pan(),
					width: o.canvasWidth,
					height: o.canvasHeight
				}).mpan = {
						x: (0 - k.pan.x) / k.zoom,
						y: (0 - k.pan.y) / k.zoom
					}
			}
			c[o.DRAG] = !1, c[o.NODE] = !1;
			var M = u.contexts[o.NODE],
				B = o.textureCache.texture;
			k = o.textureCache.viewport;
			M.setTransform(1, 0, 0, 1, 0, 0), d ? D(M, 0, 0, k.width, k.height) : M.clearRect(0, 0, k.width, k.height);
			var _ = m.core("outside-texture-bg-color").value,
				N = m.core("outside-texture-bg-opacity").value;
			o.colorFillStyle(M, _[0], _[1], _[2], N), M.fillRect(0, 0, k.width, k.height);
			b = l.zoom();
			P(M, !1), M.clearRect(k.mpan.x, k.mpan.y, k.width / k.zoom / s, k.height / k.zoom / s), M.drawImage(B, k.mpan.x, k.mpan.y, k.width / k.zoom / s, k.height / k.zoom / s)
		} else o.textureOnViewport && !t && (o.textureCache = null);
		var I = l.extent(),
			z = o.pinching || o.hoverData.dragging || o.swipePanning || o.data.wheelZooming || o.hoverData.draggingEles,
			L = o.hideEdgesOnViewport && z,
			A = [];
		if (A[o.NODE] = !c[o.NODE] && d && !o.clearedForMotionBlur[o.NODE] || o.clearingMotionBlur, A[o.NODE] && (o.clearedForMotionBlur[o.NODE] = !0), A[o.DRAG] = !c[o.DRAG] && d && !o.clearedForMotionBlur[o.DRAG] || o.clearingMotionBlur, A[o.DRAG] && (o.clearedForMotionBlur[o.DRAG] = !0), c[o.NODE] || n || r || A[o.NODE]) {
			var O = d && !A[o.NODE] && 1 !== p;
			P(M = t || (O ? o.data.bufferContexts[o.MOTIONBLUR_BUFFER_NODE] : u.contexts[o.NODE]), d && !O ? "motionBlur" : void 0), L ? o.drawCachedNodes(M, S.nondrag, s, I) : o.drawLayeredElements(M, S.nondrag, s, I), o.debug && o.drawDebugPoints(M, S.nondrag), n || d || (c[o.NODE] = !1)
		}
		if (!r && (c[o.DRAG] || n || A[o.DRAG])) {
			O = d && !A[o.DRAG] && 1 !== p;
			P(M = t || (O ? o.data.bufferContexts[o.MOTIONBLUR_BUFFER_DRAG] : u.contexts[o.DRAG]), d && !O ? "motionBlur" : void 0), L ? o.drawCachedNodes(M, S.drag, s, I) : o.drawCachedElements(M, S.drag, s, I), o.debug && o.drawDebugPoints(M, S.drag), n || d || (c[o.DRAG] = !1)
		}
		if (o.showFps || !r && c[o.SELECT_BOX] && !n) {
			if (P(M = t || u.contexts[o.SELECT_BOX]), 1 == o.selection[4] && (o.hoverData.selecting || o.touchData.selecting)) {
				b = o.cy.zoom();
				var R = m.core("selection-box-border-width").value / b;
				M.lineWidth = R, M.fillStyle = "rgba(" + m.core("selection-box-color").value[0] + "," + m.core("selection-box-color").value[1] + "," + m.core("selection-box-color").value[2] + "," + m.core("selection-box-opacity").value + ")", M.fillRect(o.selection[0], o.selection[1], o.selection[2] - o.selection[0], o.selection[3] - o.selection[1]), R > 0 && (M.strokeStyle = "rgba(" + m.core("selection-box-border-color").value[0] + "," + m.core("selection-box-border-color").value[1] + "," + m.core("selection-box-border-color").value[2] + "," + m.core("selection-box-opacity").value + ")", M.strokeRect(o.selection[0], o.selection[1], o.selection[2] - o.selection[0], o.selection[3] - o.selection[1]))
			}
			if (u.bgActivePosistion && !o.hoverData.selecting) {
				b = o.cy.zoom();
				var V = u.bgActivePosistion;
				M.fillStyle = "rgba(" + m.core("active-bg-color").value[0] + "," + m.core("active-bg-color").value[1] + "," + m.core("active-bg-color").value[2] + "," + m.core("active-bg-opacity").value + ")", M.beginPath(), M.arc(V.x, V.y, m.core("active-bg-size").pfValue / b, 0, 2 * Math.PI), M.fill()
			}
			var F = o.lastRedrawTime;
			if (o.showFps && F) {
				F = Math.round(F);
				var q = Math.round(1e3 / F);
				M.setTransform(1, 0, 0, 1, 0, 0), M.fillStyle = "rgba(255, 0, 0, 0.75)", M.strokeStyle = "rgba(255, 0, 0, 0.75)", M.lineWidth = 1, M.fillText("1 frame = " + F + " ms = " + q + " fps", 0, 20);
				M.strokeRect(0, 30, 250, 20), M.fillRect(0, 30, 250 * Math.min(q / 60, 1), 20)
			}
			n || (c[o.SELECT_BOX] = !1)
		}
		if (d && 1 !== p) {
			var Y = u.contexts[o.NODE],
				j = o.data.bufferCanvases[o.MOTIONBLUR_BUFFER_NODE],
				X = u.contexts[o.DRAG],
				W = o.data.bufferCanvases[o.MOTIONBLUR_BUFFER_DRAG],
				H = function (e, t, n) {
					e.setTransform(1, 0, 0, 1, 0, 0), n || !y ? e.clearRect(0, 0, o.canvasWidth, o.canvasHeight) : D(e, 0, 0, o.canvasWidth, o.canvasHeight);
					var r = p;
					e.drawImage(t, 0, 0, o.canvasWidth * r, o.canvasHeight * r, 0, 0, o.canvasWidth, o.canvasHeight)
				};
			(c[o.NODE] || A[o.NODE]) && (H(Y, j, A[o.NODE]), c[o.NODE] = !1), (c[o.DRAG] || A[o.DRAG]) && (H(X, W, A[o.DRAG]), c[o.DRAG] = !1)
		}
		o.prevViewport = k, o.clearingMotionBlur && (o.clearingMotionBlur = !1, o.motionBlurCleared = !0, o.motionBlur = !0), d && (o.motionBlurTimeout = setTimeout(function () {
			o.motionBlurTimeout = null, o.clearedForMotionBlur[o.NODE] = !1, o.clearedForMotionBlur[o.DRAG] = !1, o.motionBlur = !1, o.clearingMotionBlur = !h, o.mbFrames = 0, c[o.NODE] = !0, c[o.DRAG] = !0, o.redraw()
		}, 100)), t || l.emit("render")
	};
	for (var zs = {
		drawPolygonPath: function (e, t, n, r, i, a) {
			var o = r / 2,
				s = i / 2;
			e.beginPath && e.beginPath(), e.moveTo(t + o * a[0], n + s * a[1]);
			for (var l = 1; l < a.length / 2; l++) e.lineTo(t + o * a[2 * l], n + s * a[2 * l + 1]);
			e.closePath()
		},
		drawRoundRectanglePath: function (e, t, n, r, i) {
			var a = r / 2,
				o = i / 2,
				s = Vt(r, i);
			e.beginPath && e.beginPath(), e.moveTo(t, n - o), e.arcTo(t + a, n - o, t + a, n, s), e.arcTo(t + a, n + o, t, n + o, s), e.arcTo(t - a, n + o, t - a, n, s), e.arcTo(t - a, n - o, t, n - o, s), e.lineTo(t, n - o), e.closePath()
		},
		drawBottomRoundRectanglePath: function (e, t, n, r, i) {
			var a = r / 2,
				o = i / 2,
				s = Vt(r, i);
			e.beginPath && e.beginPath(), e.moveTo(t, n - o), e.lineTo(t + a, n - o), e.lineTo(t + a, n), e.arcTo(t + a, n + o, t, n + o, s), e.arcTo(t - a, n + o, t - a, n, s), e.lineTo(t - a, n - o), e.lineTo(t, n - o), e.closePath()
		},
		drawCutRectanglePath: function (e, t, n, r, i) {
			var a = r / 2,
				o = i / 2;
			e.beginPath && e.beginPath(), e.moveTo(t - a + 8, n - o), e.lineTo(t + a - 8, n - o), e.lineTo(t + a, n - o + 8), e.lineTo(t + a, n + o - 8), e.lineTo(t + a - 8, n + o), e.lineTo(t - a + 8, n + o), e.lineTo(t - a, n + o - 8), e.lineTo(t - a, n - o + 8), e.closePath()
		},
		drawBarrelPath: function (e, t, n, r, i) {
			var a = r / 2,
				o = i / 2,
				s = t - a,
				l = t + a,
				u = n - o,
				c = n + o,
				h = Ft(r, i),
				d = h.widthOffset,
				p = h.heightOffset,
				f = h.ctrlPtOffsetPct * d;
			e.beginPath && e.beginPath(), e.moveTo(s, u + p), e.lineTo(s, c - p), e.quadraticCurveTo(s + f, c, s + d, c), e.lineTo(l - d, c), e.quadraticCurveTo(l - f, c, l, c - p), e.lineTo(l, u + p), e.quadraticCurveTo(l - f, u, l - d, u), e.lineTo(s + d, u), e.quadraticCurveTo(s + f, u, s, u + p), e.closePath()
		}
	}, Ls = Math.sin(0), As = Math.cos(0), Os = {}, Rs = {}, Vs = Math.PI / 40, Fs = 0 * Math.PI; Fs < 2 * Math.PI; Fs += Vs) Os[Fs] = Math.sin(Fs), Rs[Fs] = Math.cos(Fs);
	zs.drawEllipsePath = function (e, t, n, r, i) {
		if (e.beginPath && e.beginPath(), e.ellipse) e.ellipse(t, n, r / 2, i / 2, 0, 0, 2 * Math.PI);
		else
			for (var a, o, s = r / 2, l = i / 2, u = 0 * Math.PI; u < 2 * Math.PI; u += Vs) a = t - s * Os[u] * Ls + s * Rs[u] * As, o = n + l * Rs[u] * Ls + l * Os[u] * As, 0 === u ? e.moveTo(a, o) : e.lineTo(a, o);
		e.closePath()
	};
	var qs = {};

	function Ys(e) {
		var t = e.indexOf(",");
		return e.substr(t + 1)
	}

	function js(e, t, n) {
		var r = function () {
			return t.toDataURL(n, e.quality)
		};
		switch (e.output) {
			case "blob-promise":
				return new Kn(function (r, i) {
					try {
						t.toBlob(function (e) {
							null != e ? r(e) : i(new Error("`canvas.toBlob()` sent a null value in its callback"))
						}, n, e.quality)
					} catch (e) {
						i(e)
					}
				});
			case "blob":
				return function (e, t) {
					for (var n = atob(e), r = new ArrayBuffer(n.length), i = new Uint8Array(r), a = 0; a < n.length; a++) i[a] = n.charCodeAt(a);
					return new Blob([r], {
						type: t
					})
				}(Ys(r()), n);
			case "base64":
				return Ys(r());
			case "base64uri":
			default:
				return r()
		}
	}
	qs.createBuffer = function (e, t) {
		var n = document.createElement("canvas");
		return n.width = e, n.height = t, [n, n.getContext("2d")]
	}, qs.bufferCanvasImage = function (e) {
		var t = this.cy,
			n = t.mutableElements().boundingBox(),
			r = this.findContainerClientCoords(),
			i = e.full ? Math.ceil(n.w) : r[2],
			a = e.full ? Math.ceil(n.h) : r[3],
			o = v(e.maxWidth) || v(e.maxHeight),
			s = this.getPixelRatio(),
			l = 1;
		if (void 0 !== e.scale) i *= e.scale, a *= e.scale, l = e.scale;
		else if (o) {
			var u = 1 / 0,
				c = 1 / 0;
			v(e.maxWidth) && (u = l * e.maxWidth / i), v(e.maxHeight) && (c = l * e.maxHeight / a), i *= l = Math.min(u, c), a *= l
		}
		o || (i *= s, a *= s, l *= s);
		var h = document.createElement("canvas");
		h.width = i, h.height = a, h.style.width = i + "px", h.style.height = a + "px";
		var d = h.getContext("2d");
		if (i > 0 && a > 0) {
			d.clearRect(0, 0, i, a), d.globalCompositeOperation = "source-over";
			var p = this.getCachedZSortedEles();
			if (e.full) d.translate(-n.x1 * l, -n.y1 * l), d.scale(l, l), this.drawElements(d, p), d.scale(1 / l, 1 / l), d.translate(n.x1 * l, n.y1 * l);
			else {
				var f = t.pan(),
					g = {
						x: f.x * l,
						y: f.y * l
					};
				l *= t.zoom(), d.translate(g.x, g.y), d.scale(l, l), this.drawElements(d, p), d.scale(1 / l, 1 / l), d.translate(-g.x, -g.y)
			}
			e.bg && (d.globalCompositeOperation = "destination-over", d.fillStyle = e.bg, d.rect(0, 0, i, a), d.fill())
		}
		return h
	}, qs.png = function (e) {
		return js(e, this.bufferCanvasImage(e), "image/png")
	}, qs.jpg = function (e) {
		return js(e, this.bufferCanvasImage(e), "image/jpeg")
	};
	var Xs = {
		nodeShapeImpl: function (e, t, n, r, i, a, o) {
			switch (e) {
				case "ellipse":
					return this.drawEllipsePath(t, n, r, i, a);
				case "polygon":
					return this.drawPolygonPath(t, n, r, i, a, o);
				case "roundrectangle":
				case "round-rectangle":
					return this.drawRoundRectanglePath(t, n, r, i, a);
				case "cutrectangle":
				case "cut-rectangle":
					return this.drawCutRectanglePath(t, n, r, i, a);
				case "bottomroundrectangle":
				case "bottom-round-rectangle":
					return this.drawBottomRoundRectanglePath(t, n, r, i, a);
				case "barrel":
					return this.drawBarrelPath(t, n, r, i, a)
			}
		}
	},
		Ws = Ks,
		Hs = Ks.prototype;

	function Ks(e) {
		var t = this;
		t.data = {
			canvases: new Array(Hs.CANVAS_LAYERS),
			contexts: new Array(Hs.CANVAS_LAYERS),
			canvasNeedsRedraw: new Array(Hs.CANVAS_LAYERS),
			bufferCanvases: new Array(Hs.BUFFER_COUNT),
			bufferContexts: new Array(Hs.CANVAS_LAYERS)
		};
		t.data.canvasContainer = document.createElement("div");
		var n = t.data.canvasContainer.style;
		t.data.canvasContainer.style["-webkit-tap-highlight-color"] = "rgba(0,0,0,0)", n.position = "relative", n.zIndex = "0", n.overflow = "hidden";
		var r = e.cy.container();
		r.appendChild(t.data.canvasContainer), r.style["-webkit-tap-highlight-color"] = "rgba(0,0,0,0)";
		var i = {
			"-webkit-user-select": "none",
			"-moz-user-select": "-moz-none",
			"user-select": "none",
			"-webkit-tap-highlight-color": "rgba(0,0,0,0)",
			"outline-style": "none"
		};
		S() && (i["-ms-touch-action"] = "none", i["touch-action"] = "none");
		for (var a = 0; a < Hs.CANVAS_LAYERS; a++) {
			var o = t.data.canvases[a] = document.createElement("canvas");
			t.data.contexts[a] = o.getContext("2d"), Object.keys(i).forEach(function (e) {
				o.style[e] = i[e]
			}), o.style.position = "absolute", o.setAttribute("data-id", "layer" + a), o.style.zIndex = String(Hs.CANVAS_LAYERS - a), t.data.canvasContainer.appendChild(o), t.data.canvasNeedsRedraw[a] = !1
		}
		t.data.topCanvas = t.data.canvases[0], t.data.canvases[Hs.NODE].setAttribute("data-id", "layer" + Hs.NODE + "-node"), t.data.canvases[Hs.SELECT_BOX].setAttribute("data-id", "layer" + Hs.SELECT_BOX + "-selectbox"), t.data.canvases[Hs.DRAG].setAttribute("data-id", "layer" + Hs.DRAG + "-drag");
		for (a = 0; a < Hs.BUFFER_COUNT; a++) t.data.bufferCanvases[a] = document.createElement("canvas"), t.data.bufferContexts[a] = t.data.bufferCanvases[a].getContext("2d"), t.data.bufferCanvases[a].style.position = "absolute", t.data.bufferCanvases[a].setAttribute("data-id", "buffer" + a), t.data.bufferCanvases[a].style.zIndex = String(-a - 1), t.data.bufferCanvases[a].style.visibility = "hidden";
		t.pathsEnabled = !0;
		var s = pt(),
			l = function (e) {
				return {
					x: -e.w / 2,
					y: -e.h / 2
				}
			},
			u = function (e) {
				return e.boundingBox(), e[0]._private.bodyBounds
			},
			c = function (e) {
				return e.boundingBox(), e[0]._private.labelBounds.main || s
			},
			h = function (e) {
				return e.boundingBox(), e[0]._private.labelBounds.source || s
			},
			d = function (e) {
				return e.boundingBox(), e[0]._private.labelBounds.target || s
			},
			p = function (e, t) {
				return t
			},
			f = function (e, t) {
				return {
					x: e.x + t.pstyle("text-margin-x").pfValue,
					y: e.y + t.pstyle("text-margin-y").pfValue
				}
			},
			g = function (e, t, n) {
				var r = e[0]._private.rscratch;
				return {
					x: r[t],
					y: r[n]
				}
			},
			v = t.data.eleTxrCache = new cs(t, {
				getKey: function (e) {
					return e[0]._private.nodeKey
				},
				doesEleInvalidateKey: function (e) {
					var t = e[0]._private;
					return !(t.oldBackgroundTimestamp === t.backgroundTimestamp)
				},
				drawElement: function (e, n, r, i, a) {
					return t.drawElement(e, n, r, !1, !1, a)
				},
				getBoundingBox: u,
				getRotationPoint: function (e) {
					return {
						x: ((t = u(e)).x1 + t.x2) / 2,
						y: (t.y1 + t.y2) / 2
					};
					var t
				},
				getRotationOffset: function (e) {
					return l(u(e))
				},
				allowEdgeTxrCaching: !1,
				allowParentTxrCaching: !1
			}),
			y = t.data.lblTxrCache = new cs(t, {
				getKey: function (e) {
					return e[0]._private.labelStyleKey
				},
				drawElement: function (e, n, r, i, a) {
					return t.drawElementText(e, n, r, i, "main", a)
				},
				getBoundingBox: c,
				getRotationPoint: function (e) {
					return f(g(e, "labelX", "labelY"), e)
				},
				getRotationOffset: function (e) {
					var t = c(e),
						n = l(c(e));
					if (e.isNode()) {
						switch (e.pstyle("text-halign").value) {
							case "left":
								n.x = -t.w;
								break;
							case "right":
								n.x = 0
						}
						switch (e.pstyle("text-valign").value) {
							case "top":
								n.y = -t.h;
								break;
							case "bottom":
								n.y = 0
						}
					}
					return n
				},
				isVisible: p
			}),
			m = t.data.slbTxrCache = new cs(t, {
				getKey: function (e) {
					return e[0]._private.sourceLabelStyleKey
				},
				drawElement: function (e, n, r, i, a) {
					return t.drawElementText(e, n, r, i, "source", a)
				},
				getBoundingBox: h,
				getRotationPoint: function (e) {
					return f(g(e, "sourceLabelX", "sourceLabelY"), e)
				},
				getRotationOffset: function (e) {
					return l(h(e))
				},
				isVisible: p
			}),
			b = t.data.tlbTxrCache = new cs(t, {
				getKey: function (e) {
					return e[0]._private.targetLabelStyleKey
				},
				drawElement: function (e, n, r, i, a) {
					return t.drawElementText(e, n, r, i, "target", a)
				},
				getBoundingBox: d,
				getRotationPoint: function (e) {
					return f(g(e, "targetLabelX", "targetLabelY"), e)
				},
				getRotationOffset: function (e) {
					return l(d(e))
				},
				isVisible: p
			}),
			x = t.data.lyrTxrCache = new ds(t);
		t.onUpdateEleCalcs(function (e, t) {
			v.invalidateElements(t), y.invalidateElements(t), m.invalidateElements(t), b.invalidateElements(t), x.invalidateElements(t);
			for (var n = 0; n < t.length; n++) {
				var r = t[n]._private;
				r.oldBackgroundTimestamp = r.backgroundTimestamp
			}
		});
		var w = function (e) {
			for (var t = 0; t < e.length; t++) x.enqueueElementRefinement(e[t].ele)
		};
		v.onDequeue(w), y.onDequeue(w), m.onDequeue(w), b.onDequeue(w)
	}
	Hs.CANVAS_LAYERS = 3, Hs.SELECT_BOX = 0, Hs.DRAG = 1, Hs.NODE = 2, Hs.BUFFER_COUNT = 3, Hs.TEXTURE_BUFFER = 0, Hs.MOTIONBLUR_BUFFER_NODE = 1, Hs.MOTIONBLUR_BUFFER_DRAG = 2, Hs.redrawHint = function (e, t) {
		var n = this;
		switch (e) {
			case "eles":
				n.data.canvasNeedsRedraw[Hs.NODE] = t;
				break;
			case "drag":
				n.data.canvasNeedsRedraw[Hs.DRAG] = t;
				break;
			case "select":
				n.data.canvasNeedsRedraw[Hs.SELECT_BOX] = t
		}
	};
	var Gs = "undefined" != typeof Path2D;
	Hs.path2dEnabled = function (e) {
		if (void 0 === e) return this.pathsEnabled;
		this.pathsEnabled = !!e
	}, Hs.usePaths = function () {
		return Gs && this.pathsEnabled
	}, Hs.setImgSmoothing = function (e, t) {
		null != e.imageSmoothingEnabled ? e.imageSmoothingEnabled = t : (e.webkitImageSmoothingEnabled = t, e.mozImageSmoothingEnabled = t, e.msImageSmoothingEnabled = t)
	}, Hs.getImgSmoothing = function (e) {
		return null != e.imageSmoothingEnabled ? e.imageSmoothingEnabled : e.webkitImageSmoothingEnabled || e.mozImageSmoothingEnabled || e.msImageSmoothingEnabled
	}, Hs.makeOffscreenCanvas = function (t, n) {
		var r;
		return "undefined" !== ("undefined" == typeof OffscreenCanvas ? "undefined" : e(OffscreenCanvas)) ? r = new OffscreenCanvas(t, n) : ((r = document.createElement("canvas")).width = t, r.height = n), r
	}, [ys, Es, Ms, Bs, _s, Ns, Is, zs, qs, Xs].forEach(function (e) {
		I(Hs, e)
	});
	var Zs = [{
		type: "layout",
		extensions: zo
	}, {
		type: "renderer",
		extensions: [{
			name: "null",
			impl: Lo
		}, {
			name: "base",
			impl: is
		}, {
			name: "canvas",
			impl: Ws
		}]
	}],
		Us = {},
		$s = {};

	function Qs(e, t, n) {
		var r = n,
			i = function (n) {
				Ee("Can not register `" + t + "` for `" + e + "` since `" + n + "` already exists in the prototype and can not be overridden")
			};
		if ("core" === e) {
			if (Ka.prototype[t]) return i(t);
			Ka.prototype[t] = n
		} else if ("collection" === e) {
			if (ca.prototype[t]) return i(t);
			ca.prototype[t] = n
		} else if ("layout" === e) {
			for (var a = function (e) {
				this.options = e, n.call(this, e), g(this._private) || (this._private = {}), this._private.cy = e.cy, this._private.listeners = [], this.createEmitter()
			}, o = a.prototype = Object.create(n.prototype), s = [], l = 0; l < s.length; l++) {
				var u = s[l];
				o[u] = o[u] || function () {
					return this
				}
			}
			o.start && !o.run ? o.run = function () {
				return this.start(), this
			} : !o.start && o.run && (o.start = function () {
				return this.run(), this
			});
			var c = n.prototype.stop;
			o.stop = function () {
				var e = this.options;
				if (e && e.animate) {
					var t = this.animations;
					if (t)
						for (var n = 0; n < t.length; n++) t[n].stop()
				}
				return c ? c.call(this) : this.emit("layoutstop"), this
			}, o.destroy || (o.destroy = function () {
				return this
			}), o.cy = function () {
				return this._private.cy
			};
			var h = function (e) {
				return e._private.cy
			},
				d = {
					addEventFields: function (e, t) {
						t.layout = e, t.cy = h(e), t.target = e
					},
					bubble: function () {
						return !0
					},
					parent: function (e) {
						return h(e)
					}
				};
			I(o, {
				createEmitter: function () {
					return this._private.emitter = new Ti(d, this), this
				},
				emitter: function () {
					return this._private.emitter
				},
				on: function (e, t) {
					return this.emitter().on(e, t), this
				},
				one: function (e, t) {
					return this.emitter().one(e, t), this
				},
				once: function (e, t) {
					return this.emitter().one(e, t), this
				},
				removeListener: function (e, t) {
					return this.emitter().removeListener(e, t), this
				},
				removeAllListeners: function () {
					return this.emitter().removeAllListeners(), this
				},
				emit: function (e, t) {
					return this.emitter().emit(e, t), this
				}
			}), Un.eventAliasesOn(o), r = a
		} else if ("renderer" === e && "null" !== t && "base" !== t) {
			var p = Js("renderer", "base"),
				f = p.prototype,
				v = n,
				y = n.prototype,
				m = function () {
					p.apply(this, arguments), v.apply(this, arguments)
				},
				b = m.prototype;
			for (var x in f) {
				var w = f[x];
				if (null != y[x]) return i(x);
				b[x] = w
			}
			for (var E in y) b[E] = y[E];
			f.clientFunctions.forEach(function (e) {
				b[e] = b[e] || function () {
					Ee("Renderer does not implement `renderer." + e + "()` on its prototype")
				}
			}), r = m
		}
		return A({
			map: Us,
			keys: [e, t],
			value: r
		})
	}

	function Js(e, t) {
		return O({
			map: Us,
			keys: [e, t]
		})
	}
	var el = function () {
		return 2 === arguments.length ? Js.apply(null, arguments) : 3 === arguments.length ? Qs.apply(null, arguments) : 4 === arguments.length ? function (e, t, n, r) {
			return O({
				map: $s,
				keys: [e, t, n, r]
			})
		}.apply(null, arguments) : 5 === arguments.length ? function (e, t, n, r, i) {
			return A({
				map: $s,
				keys: [e, t, n, r],
				value: i
			})
		}.apply(null, arguments) : void Ee("Invalid extension access syntax")
	};
	Ka.prototype.extension = el, Zs.forEach(function (e) {
		e.extensions.forEach(function (t) {
			Qs(e.type, t.name, t.impl)
		})
	});
	var tl = function e() {
		if (!(this instanceof e)) return new e;
		this.length = 0
	},
		nl = tl.prototype;
	nl.instanceString = function () {
		return "stylesheet"
	}, nl.selector = function (e) {
		return this[this.length++] = {
			selector: e,
			properties: []
		}, this
	}, nl.css = function (e, t) {
		var n = this.length - 1;
		if (d(e)) this[n].properties.push({
			name: e,
			value: t
		});
		else if (g(e))
			for (var r = e, i = Object.keys(r), a = 0; a < i.length; a++) {
				var o = i[a],
					s = r[o];
				if (null != s) {
					var l = Ya.properties[o] || Ya.properties[T(o)];
					if (null != l) {
						var u = l.name,
							c = s;
						this[n].properties.push({
							name: u,
							value: c
						})
					}
				}
			}
		return this
	}, nl.style = nl.css, nl.generateStyle = function (e) {
		var t = new Ya(e);
		return this.appendToStyle(t)
	}, nl.appendToStyle = function (e) {
		for (var t = 0; t < this.length; t++) {
			var n = this[t],
				r = n.selector,
				i = n.properties;
			e.selector(r);
			for (var a = 0; a < i.length; a++) {
				var o = i[a];
				e.css(o.name, o.value)
			}
		}
		return e
	};
	var rl = function (e) {
		return void 0 === e && (e = {}), g(e) ? new Ka(e) : d(e) ? el.apply(el, arguments) : void 0
	};
	return rl.use = function (e) {
		var t = Array.prototype.slice.call(arguments, 1);
		return t.unshift(rl), e.apply(null, t), this
	}, rl.warnings = function (e) {
		return ke(e)
	}, rl.version = "3.8.1", rl.stylesheet = rl.Stylesheet = tl, rl
});