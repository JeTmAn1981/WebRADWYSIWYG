<div class="alert" align="center" style="background-color: rgb(244, 244, 244);display:none;" id="AlertMessageSection">
    <div style="max-width:80em; padding: 2em 1em; text-align:left;">
        <img src="/images/alert.png" style="float:left; margin: .5em;">
        <p id="AlertMessage" style="margin-left: 4em;"></p>
    </div>
</div>
<!-- End Alert Message Display -->
<!-- Reusable code : Google Tag Manager -->
<noscript>
    <iframe src="//www.googletagmanager.com/ns.html?id=GTM-T5B6BN" height="0" width="0" style="display:none;visibility:hidden"></iframe>
</noscript>
<script>
    (function (w, d, s, l, i) {
        w[l] = w[l] || []; w[l].push({
            'gtm.start': new Date().getTime(), event: 'gtm.js'
        });
        var f = d.getElementsByTagName(s)[0],
            j = d.createElement(s),
            dl = l != 'dataLayer' ? '&l=' + l : '';
        j.async = true;
        j.src = '//www.googletagmanager.com/gtm.js?id=' + i + dl;
        f.parentNode.insertBefore(j, f);
    })(window, document, 'script', 'dataLayer', 'GTM-T5B6BN');
</script>
<!-- End Google Tag Manager -->
<div class="overflow-hidden-wrapper" id="open-menu">
    <div class="pushed-by-menu">
        <a href="#" class="pushed-by-menu--overlay">
            <h3 class="visually-hidden">Close Menu</h3>
        </a>
@Html.Partial("Header")  
         <div class="hide-primary-nav">
            <article>
                <div id="GeneralSubpageHeader">
                    <header class="page-header page_title-border_top page_title-bg">
                        <div class="pl-page-wrapper">
                            <h1 class="page_title-text page_title-text_size">
                                @ViewBag.Title
                            </h1>
                            <select class="header-links icon-arrow-down"></select>
                            <div class="header_links-fallback icon-arrow-down"></div>
                        </div>
                    </header>
                </div>
                <div class="pl-page-wrapper pad_vertical-x2-at_lg">
                    <div class="pl-wide">
                        <!-- form controls -->
                            <!-- breadcrumbs -->
                            <ol class="breadcrumbs hide_at_sm"><li><a href="http://www.whitworth.edu/cms/">Whitworth University</a></li></ol>
                            <!-- heading -->
                            @ViewBag.Heading
                            <!-- content -->
                            <div class="v1">
                       