function FillState(serviceUrl) {

    var countryId = $('#CountryID').val();

    $.ajax({
        url: serviceUrl,
        type: "GET",
        dataType: "JSON",
        data: { countryId: countryId },
        success: function (states) {
            $("#StateID").html("");
            $("#StateID").prop('disabled', false);
            $.each(states, function (i, state) {
                $("#StateID").append(
                    $('<option></option>').val(state.ID).html(state.Name));
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.html + ' ' + textStatus + ' ' + errorThrown);
        }
    });

}