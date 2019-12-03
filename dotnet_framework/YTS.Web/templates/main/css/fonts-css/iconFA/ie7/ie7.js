/* To avoid CSS expressions while still supporting IE 7 and IE 6, use this script */
/* The script tag referencing this file must be placed before the ending body tag. */

/* Use conditional comments in order to target IE 7 and older:
	<!--[if lt IE 8]><!-->
	<script src="ie7/ie7.js"></script>
	<!--<![endif]-->
*/

(function() {
	function addIcon(el, entity) {
		var html = el.innerHTML;
		el.innerHTML = '<span style="font-family: \'iconFA\'">' + entity + '</span>' + html;
	}
	var icons = {
		'iconFA-search': '&#xf002;',
		'iconFA-heart': '&#xf004;',
		'iconFA-power-off': '&#xf011;',
		'iconFA-clock-o': '&#xf017;',
		'iconFA-camera': '&#xf030;',
		'iconFA-video-camera': '&#xf03d;',
		'iconFA-map-marker': '&#xf041;',
		'iconFA-chevron-left': '&#xf053;',
		'iconFA-chevron-right': '&#xf054;',
		'iconFA-arrow-left': '&#xf060;',
		'iconFA-arrow-right': '&#xf061;',
		'iconFA-eye': '&#xf06e;',
		'iconFA-plane': '&#xf072;',
		'iconFA-calendar': '&#xf073;',
		'iconFA-heart-o': '&#xf08a;',
		'iconFA-phone': '&#xf095;',
		'iconFA-envelope': '&#xf0e0;',
		'iconFA-location-arrow': '&#xf124;',
		'iconFA-cny': '&#xf157;',
		'iconFA-jpy': '&#xf157;',
		'iconFA-rmb': '&#xf157;',
		'iconFA-yen': '&#xf157;',
		'iconFA-weibo': '&#xf18a;',
		'iconFA-qq': '&#xf1d6;',
		'iconFA-wechat': '&#xf1d7;',
		'iconFA-weixin': '&#xf1d7;',
		'iconFA-map-pin': '&#xf276;',
		'iconFA-user-circle-o': '&#xf2be;',
		'0': 0
		},
		els = document.getElementsByTagName('*'),
		i, c, el;
	for (i = 0; ; i += 1) {
		el = els[i];
		if(!el) {
			break;
		}
		c = el.className;
		c = c.match(/iconFA-[^\s'"]+/);
		if (c && icons[c[0]]) {
			addIcon(el, icons[c[0]]);
		}
	}
}());
