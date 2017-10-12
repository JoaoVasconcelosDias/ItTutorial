<script type="text/javascript">
    $(function () {
        $('#btnRadio').click(function () {
            var checkedradio = $('[name="gr"]:radio:checked').val();
            $("#sel").html("Selected Value: " + checkedradio);
        });
    });
</script>