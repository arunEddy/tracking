let modal;
const API = AppConfig.apiBaseUrl + "/api/AWBStockRequest";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('awbModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Request");
        modal.show();
    });

    $("#btnSave").click(saveData);
});


// ================= VALIDATION =================
function validateForm() {

    if (!$("#officeName").val().trim())
        return showError("Office Name is required");

    if (!$("#itemName").val().trim())
        return showError("Item Name is required");

    if (!$("#quantity").val())
        return showError("Quantity is required");

    if (!$("#requestDate").val())
        return showError("Request Date is required");

    if (!$("#remarks").val().trim())
        return showError("Remarks is required");

    return true;
}

function showError(msg) {
    Swal.fire({
        icon: "warning",
        title: "Validation",
        text: msg
    });
    return false;
}


// ================= LOAD =================
function loadData() {

    showLoader();

    $.get(API, function (res) {

        console.log("AWB Response:", res);

        let data = res?.data || res?.$values || res;

        if (!Array.isArray(data)) {
            Swal.fire("Error", "Invalid API response", "error");
            return;
        }

        let html = "";

        data.forEach(x => {

            html += `<tr>
                <td>${x.asrid}</td>
                <td>${x.aOfficeName}</td>
                <td>${x.aItemName}</td>
                <td>${x.quantity}</td>
                <td>${formatDate(x.aRequestDate)}</td>
                <td>${formatDate(x.expectedDeliveryDate)}</td>
                <td>${x.remarks}</td>
                <td class="text-end">
                    <button class="btn btn-sm btn-outline-warning me-1" onclick="edit(${x.asrid})">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-sm btn-outline-danger" onclick="deleteRec(${x.asrid})">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);

    }).always(hideLoader);
}


// ================= SAVE =================
function saveData() {

    if (!validateForm()) return;
    // let user = sessionStorage.getItem("username");
    let createdby = "Admin";
    let mdfby = "Admin";
    let id = parseInt($("#id").val()) || 0;

    let obj = {
        asrid: id,
        aOfficeName: $("#officeName").val(),
        aItemName: $("#itemName").val(),
        quantity: parseInt($("#quantity").val()),
        aRequestDate: $("#requestDate").val(),
        expectedDeliveryDate: $("#expectedDate").val() || null,
        remarks: $("#remarks").val(),
        isActive: $("#isActive").val(),
        createdby: id === 0 ? createdby : null,
        mdfby: id > 0 ? mdfby : null
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    // CONFIRM BEFORE SAVE
    Swal.fire({
        title: id > 0 ? "Update record?" : "Create record?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Yes"
    }).then(res => {

        if (!res.isConfirmed) return;

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
                console.log(err);
                Swal.fire("Error", "Something went wrong", "error");
            },

            complete: hideLoader
        });
    });
}


// ================= EDIT =================
function edit(id) {

    showLoader();

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;

        $("#id").val(x.asrid);
        $("#officeName").val(x.aOfficeName);
        $("#itemName").val(x.aItemName);
        $("#quantity").val(x.quantity);
        $("#requestDate").val(formatDateInput(x.aRequestDate));
        $("#expectedDate").val(formatDateInput(x.expectedDeliveryDate));
        $("#remarks").val(x.remarks);

        $("#isActive").val(mapIsActive(x.isActive));

        $("#modalTitle").text("Edit Request");

        modal.show();

    }).always(hideLoader);
}

function mapIsActive(val) {
    val = (val || "").toString().trim().toLowerCase();
    return (val === "yes" || val === "true" || val === "1" || val === "y") ? "Yes" : "No";
}
// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        text: "This action cannot be undone",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, Delete"
    }).then(res => {

        if (!res.isConfirmed) return;

        showLoader();

        $.ajax({
            url: API + "/" + id,
            type: "DELETE",

            success: function () {
                Swal.fire("Deleted!", "", "success");
                loadData();
            },

            error: function () {
                Swal.fire("Error", "Delete failed", "error");
            },

            complete: hideLoader
        });
    });
}


// ================= HELPERS =================
function clearForm() {
    $("input").val('');
}

function formatDate(date) {
    return date ? new Date(date).toLocaleDateString() : '';
}

function formatDateInput(date) {
    if (!date) return '';

    let d = new Date(date);
    let offset = d.getTimezoneOffset();

    d.setMinutes(d.getMinutes() - offset);

    return d.toISOString().split('T')[0];
}

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}