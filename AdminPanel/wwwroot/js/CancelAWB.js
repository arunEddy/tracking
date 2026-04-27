let modal;
const API = AppConfig.apiBaseUrl + "/api/CancelAWB";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('awbCancelModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Cancel AWB");
        modal.show();
    });

    $("#btnSave").click(saveData);
});


// LOAD DATA
function loadData() {

    showLoader();

    $.get(API, function (res) {

        let data = res?.data || res;
        let html = "";

        data.forEach((x, i) => {

            html += `<tr>
                <td>${i + 1}</td>
                <td>${x.awbNumber}</td>
                <td>${x.reason}</td>

                <td class="text-end">
                    <button class="btn btn-sm btn-outline-warning me-1" onclick="edit(${x.caid})">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-sm btn-outline-danger" onclick="deleteRec(${x.caid})">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);

    }).always(hideLoader);
}


// SAVE
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#id").val()) || 0;
    let user = sessionStorage.getItem("username") || "Admin";
    let obj = {
        caid: id,
        awbNumber: $("#awbNumbers").val(),
        reason: $("#reason").val(),
        isActive: $("#isActive").val(),
        createdBy: id === 0 ? user : null,
        modifiedBy: id > 0 ? user : null
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    Swal.fire({
        title: id ? "Update cancel request?" : "Save Cancel AWB?",
        icon: "question",
        showCancelButton: true
    }).then(r => {

        if (!r.isConfirmed) return;

        showLoader();

        $.ajax({
            url: url,
            type: type,
            contentType: "application/json",
            data: JSON.stringify(obj),

            success: () => {
                Swal.fire({
                    icon: "success",
                    title: id ? "Updated Successfully" : "Created Successfully",
                    timer: 1500,
                    showConfirmButton: false
                });
                modal.hide();
                loadData();
            },
            error: function (err) {
                console.error(err);
                Swal.fire("Error", "Something went wrong", "error");
            },

            complete: hideLoader
        });
    });
}


// EDIT
function edit(id) {

    $.get(API + "/" + id, function (x) {

        x = x?.data || x;

        $("#id").val(x.caid);
        $("#awbNumbers").val(x.awbNumber);
        $("#reason").val(x.reason);
        $("#isActive").val(mapIsActive(x.isActive));

        $("#modalTitle").text("Edit Cancel AWB");
        modal.show();
    });
}
function mapIsActive(val) {
    val = (val || "").toString().trim().toLowerCase();
    return (val === "yes" || val === "true" || val === "1" || val === "y") ? "Yes" : "No";
}

// DELETE
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        icon: "warning",
        showCancelButton: true
    }).then(r => {

        if (!r.isConfirmed) return;

        $.ajax({
            url: API + "/" + id,
            type: "DELETE",
            success: () => {
                Swal.fire("Deleted!", "", "success");
                loadData();
            }
        });
    });
}


// VALIDATION
function validate() {

    let awb = $("#awbNumbers").val().trim();
    let reason = $("#reason").val().trim();

    if (!awb)
        return Swal.fire("Validation", "AWB required", "warning"), false;

    if (!reason)
        return Swal.fire("Validation", "Reason required", "warning"), false;

    return true;
}


// HELPERS
function clearForm() {
    $("#awbCancelModal textarea").val('');
    $("#id").val('');
}

function formatDate(d) {
    return d ? new Date(d).toLocaleDateString("en-GB") : '';
}

function showLoader() {
    $("body").append('<div id="loader" class="spinner-border text-danger" style="position:fixed;top:50%;left:50%;z-index:9999;"></div>');
}

function hideLoader() {
    $("#loader").remove();
}