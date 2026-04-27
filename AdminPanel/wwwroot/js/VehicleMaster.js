const API = AppConfig.apiBaseUrl + "/api/VehicleMasters";
let modal;

$(document).ready(function () {

    modal = new bootstrap.Modal("#vehicleModal");

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Vehicle");
        modal.show();
    });

    $("#btnSave").click(saveData);
    $("#btnSearch").click(() => loadData($("#search").val()));

});


// ================== LOAD DATA ==================
function loadData(search = "") {

    $("#tblData").html("<tr><td colspan='9'>Loading...</td></tr>");

    $.get(API + "?search=" + search)
        .done(function (res) {

            let data = res.data || res; // 🔥 handle wrapper

            let html = "";

            if (!data || data.length === 0) {
                html = "<tr><td colspan='9'>No Data Found</td></tr>";
            } else {

                data.forEach(x => {

                    html += `<tr>
                        <td>${x.vemid}</td>
                        <td>${x.vehicleNo}</td>
                        <td>${x.vehicleType}</td>
                        <td>${x.runningType || '-'}</td>
                        <td>${x.makeModel || '-'}</td>
                        <td>${x.capacity}</td>
                        <td>${x.vendorName || '-'}</td>
                        <td>${(x.isActive === "Y" || x.isActive === "true") ? 'Active' : 'Inactive'}</td>
                        <td>
                            <button class="btn btn-warning btn-sm" onclick="edit(${x.vemid})">Edit</button>
                            <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.vemid})">Delete</button>
                        </td>
                    </tr>`;
                });
            }

            $("#tblData").html(html);
        })
        .fail(err => handleError(err));
}


// ================== VALIDATION ==================
function validate() {

    $(".is-invalid").removeClass("is-invalid");

    function setError(id, msg) {
        $("#" + id).addClass("is-invalid");
        Swal.fire("Validation", msg, "warning");
        return false;
    }

    if (!$("#vehicleNo").val().trim())
        return setError("vehicleNo", "Vehicle No is required");

    if (!$("#vehicleType").val().trim())
        return setError("vehicleType", "Vehicle Type is required");

    if (!$("#capacity").val() || parseInt($("#capacity").val()) <= 0)
        return setError("capacity", "Capacity must be greater than 0");

    if (!$("#originOffice").val().trim())
        return setError("originOffice", "Origin Office required");

    if (!$("#destinationOffice").val().trim())
        return setError("destinationOffice", "Destination required");

    if (!$("#reportingTime").val())
        return setError("reportingTime", "Reporting Time required");

    return true;
}


// ================== SAVE ==================
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#vehicleId").val()) || 0;

    let obj = {
        vemid: id, // 🔥 IMPORTANT FIX
        vehicleType: $("#vehicleType").val(),
        runningType: $("#runningType").val(),
        vehicleNo: $("#vehicleNo").val(),
        reportingTime: $("#reportingTime").val(),
        makeModel: $("#model").val(),
        capacity: $("#capacity").val().toString(), // backend string
        originOffice: $("#originOffice").val(),
        destinationOffice: $("#destinationOffice").val(),
        vendorName: $("#vendorName").val(),
        trackingDeviceID: $("#deviceId").val(),
        isActive: $("#active").is(":checked") ? "Y" : "N"
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    $("#btnSave").prop("disabled", true);

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function () {
            Swal.fire("Success", "Saved Successfully", "success");
            modal.hide();
            loadData();
        },

        error: function (err) {
            handleError(err);
        },

        complete: function () {
            $("#btnSave").prop("disabled", false);
        }
    });
}


// ================== EDIT ==================
function edit(id) {

    $.get(API + "/" + id)
        .done(function (res) {

            console.log("FULL RESPONSE:", res);

            let x = res.data || res; // 🔥 FIX

            console.log("DATA:", x);

            $("#vehicleId").val(x.vemid);

            $("#vehicleNo").val(x.vehicleNo);
            $("#vehicleType").val(x.vehicleType);
            $("#runningType").val(x.runningType);

            $("#model").val(x.makeModel);

            $("#capacity").val(x.capacity);

            $("#reportingTime").val(x.reportingTime);

            $("#originOffice").val(x.originOffice);
            $("#destinationOffice").val(x.destinationOffice);

            $("#vendorName").val(x.vendorName);

            $("#deviceId").val(x.trackingDeviceID);

            $("#active").prop("checked",
                x.isActive === "Y" || x.isActive === "true"
            );

            $("#modalTitle").text("Edit Vehicle");
            modal.show();
        })
        .fail(err => handleError(err));
}


// ================== DELETE ==================
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (res.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: function () {
                    Swal.fire("Deleted", "Record removed", "success");
                    loadData();
                },
                error: function (err) {
                    handleError(err);
                }
            });
        }
    });
}


// ================== CLEAR ==================
function clearForm() {
    $("#vehicleId").val(0);
    $("input").val('');
    $("select").val('');
    $("#active").prop("checked", true);
    $(".is-invalid").removeClass("is-invalid");
}


// ================== ERROR HANDLER ==================
function handleError(err) {

    console.log("ERROR:", err);

    let msg = "Something went wrong";

    if (err.responseJSON && err.responseJSON.errors) {
        msg = Object.values(err.responseJSON.errors)
            .flat()
            .join("\n");
    } else if (err.responseText) {
        msg = err.responseText;
    }

    Swal.fire("Error", msg, "error");
}