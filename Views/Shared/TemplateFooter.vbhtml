
                            </div>
                    </div>
                </div>
            </article>

@Html.Partial("Footer")

        </div>
    </div>
</div>
<div id="div-loading"></div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
<script>
    if (typeof jQuery === 'undefined') {
        document.write(unescape('%3Cscript%20src%3D%22/js/jquery-2.1.4.min.js%22%3E%3C/script%3E'))
    };
</script>
<script src="/js/whitworth.min.js"></script>

<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/themes/smoothness/jquery-ui.css">
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js"></script>
<!-- div-loading functions -->

<script language="javascript" type="text/javascript">
    function showLoadingDiv() {
        document.getElementById('div-loading').style.display = 'block';
    }
    function showLoadingDivSetTime(seconds) {
        document.getElementById('div-loading').style.display = 'block';
        window.setInterval(hideLoadingDiv, seconds * 1000);
    }
    function hideLoadingDiv() {
        document.getElementById('div-loading').style.display = 'none';
    }
</script>
