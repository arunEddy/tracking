let modal;
const API = AppConfig.apiBaseUrl + "/api/TransportMaster";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('transportModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Transport");
        modal.show();
    });

    $("#btnSave").click(saveData);
    $("#btnSearch").click(() => loadData($("#search").val()));
});

function validateForm() {

    if (!$("#originOffice").val().trim())
        return showError("Origin Office required");

    if (!$("#destinationOffice").val().trim())
        return showError("Destination Office required");

    if (!$("#modeType").val())
        return showError("Mode Type required");

    if (!$("#modeName").val())
        return showError("Mode Name required");

    if (!$("#departureTime").val())
        return showError("Departure Time required");

    if (!$("#arrivalTime").val())
        return showError("Arrival Time required");

    return true;
}

function showError(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
}
function loadData(search = "") {

    showLoader();

    $.ajax({
        url: API + "?search=" + search,
        type: "GET",

        success: function (res) {

            let data = res?.data || res;

            let html = "";

            data.forEach(x => {

                html += `<tr>
                    <td>${x.tmid}</td>
                    <td>-</td>
<td>${x.originOffice || '-'}</td>
<td>${x.destinationOffice || '-'}</td>
<td>${x.modeType || '-'}</td>
<td>${x.airlineName || '-'}</td>
<td>${x.modeName || '-'}</td>
<td>${x.departureTime || '-'}</td>
<td>${x.arrivalTime || '-'}</td>
<td>${x.expectedTransitDay || '-'}</td>
<td>${x.expReachingTimeatHub || '-'}</td>
<td>${x.serviceType || '-'}</td>
<td>${x.isActive === "true" ? 'Yes' : 'No'}</td>

                    <td class="text-end">
                        <button class="btn btn-sm btn-outline-warning me-1" onclick="edit(${x.tmid})">
                            <i class="bi bi-pencil"></i>
                        </button>
                        <button class="btn btn-sm btn-outline-danger" onclick="deleteRec(${x.tmid})">
                            <i class="bi bi-trash"></i>
                        </button>
                    </td>
                </tr>`;
            });

            $("#tblData").html(html);
        },

        error: function (err) {
            Swal.fire("Error", err.responseText, "error");
        },

        complete: hideLoader
    });
}
function saveData() {

    if (!validateForm()) return;

    let id = parseInt($("#tmid").val()) || 0;

    let obj = {
        originOffice: $("#originOffice").val(),
        destinationOffice: $("#destinationOffice").val(),
        modeType: $("#modeType").val(),
        modeName: $("#modeName").val(),
        airlineName: $("#airlineName").val(),
        departureTime: $("#departureTime").val(),
        arrivalTime: $("#arrivalTime").val(),
        ExpectedTransitDay: $("#transitDay").val(),  
        serviceType: $("#serviceType").val(),
        ExpReachingTimeatHub: $("#reachingTime").val(),
        IsActive: $("#active").is(":checked") ? "true" : "false",
        CreatedBy: "Admin",
        CreatedDate: new Date().toISOString(),
    };

    if (id > 0) obj.tmid = id;

    let url = id > 0 ? API + "/" + id : API;
    let type = id > 0 ? "PUT" : "POST";

    showLoader();

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function () {

            Swal.fire({
                icon: "success",
                title: id > 0 ? "Updated Successfully" : "Created Successfully",
                timer: 1500,
                showConfirmButton: false
            });

            modal.hide();
            loadData();
        },

        error: function (err) {
            Swal.fire("Error", err.responseText, "error");
        },

        complete: hideLoader
    });
}
function edit(id) {

    showLoader();

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;

        $("#tmid").val(x.tmid);
        $("#originOffice").val(x.originOffice);
        $("#destinationOffice").val(x.destinationOffice);
        $("#modeType").val(x.modeType);
        $("#modeName").val(x.modeName);
        $("#airlineName").val(x.airlineName);
        $("#departureTime").val(x.departureTime);
        $("#arrivalTime").val(x.arrivalTime);
        $("#expectedtransitday").val(x.expectedTransitDay);
        $("#serviceType").val(x.serviceType);
        $("#reachingTime").val(x.expReachingTimeatHub);
        $("#active").prop("checked", x.isActive);
        $("#modifiedBy").val("Admin");
        $("#modifieddate").val(new Date().toISOString());

        $("#modalTitle").text("Edit Transport");
        modal.show();

    }).fail(err => {
        Swal.fire("Error", err.responseText, "error");
    }).always(hideLoader);
}
function deleteRec(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "This record will be deleted permanently",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            showLoader();

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",

                success: function () {
                    Swal.fire("Deleted!", "", "success");
                    loadData();
                },

                error: function (err) {
                    Swal.fire("Error", err.responseText, "error");
                },

                complete: hideLoader
            });
        }
    });
}

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
} function clearForm() {

    $("#tmid").val('');
    $("#originOffice").val('');
    $("#destinationOffice").val('');
    $("#modeType").val('');
    $("#modeName").val('');
    $("#airlineName").val('');
    $("#departureTime").val('');
    $("#arrivalTime").val('');
    $("#expectedTransitDay").val('');
    $("#serviceType").val('');
    $("#reachingTime").val('');
    $("#active").prop("checked", true);
}