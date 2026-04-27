let modal;
const API = AppConfig.apiBaseUrl + "/api/StockOut";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('stockOutModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Stock Out");
        modal.show();
    });

    $("#btnSave").click(saveData);

    // Auto Balance Calculation
    $("#qty").on("input", calculateBalance);
});


// ================= VALIDATION =================
function validateForm() {

    if (!$("#officeName").val().trim())
        return showError("Office Name required");

    if (!$("#outDate").val())
        return showError("Out Date required");

    if (!$("#itemName").val().trim())
        return showError("Item Name required");

    if (!$("#qty").val())
        return showError("Quantity required");

    return true;
}

function showError(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
} function formatDateWithMonthName(dateStr) {
    if (!dateStr) return '';

    let d = new Date(dateStr);

    let day = String(d.getDate()).padStart(2, '0');
    let month = d.toLocaleString('en-GB', { month: 'short' }); // Jan, Feb
    let year = d.getFullYear();

    return `${day}-${month}-${year}`;
}
function loadData() {

    showLoader();

    $.get(API, function (res) {

        let data = res?.data || res;


        let html = "";

        if (!data || data.length === 0) {
            html = "<tr><td colspan='11'>No Data Found</td></tr>";
        } else {

            data.forEach(x => {

                html += `<tr>
                    <td>${x.soid}</td>
                    <td>${x.sOfficeName}</td>
                    <td>${formatDateWithMonthName(x.sOutDate)}</td>
                    <td>${x.sItemName}</td>
                    <td>${x.sQuantity}</td>
                    <td>${x.serialized ? 'Yes' : 'No'}</td>
                    <td>${x.awb ? 'Yes' : 'No'}</td>
                    <td>${x.startNo || '-'}</td>
                    <td>${x.endNo || '-'}</td>
                    <td>${x.balQuantity || 0}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="edit(${x.soid})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.soid})">Delete</button>
                    </td>
                </tr>`;
            });
        }

        $("#tblData").html(html);

    }).fail(err => {
        Swal.fire("Error", err.responseText, "error");
    }).always(hideLoader);
}


// ================= SAVE =================
function saveData() {

    if (!validateForm()) return;

    let id = parseInt($("#soid").val()) || 0;

    let obj = {
        soid: id,
        sOfficeName: $("#officeName").val(),
        sOutDate: $("#outDate").val(),
        sItemName: $("#itemName").val(),
        sQuantity: $("#qty").val(),
        serialized: $("#serialized").is(":checked") ? "true" : "false",
        awb: $("#awb").is(":checked") ? "true" : "false",
        startNo: $("#startNo").val() || null,
        endNo: $("#endNo").val() || null,
        balQuantity: $("#balQty").val() || null
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

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

        error: err => Swal.fire("Error", err.responseText, "error"),

        complete: hideLoader
    });
}


// ================= EDIT =================
function edit(id) {

    showLoader();

    $.get(API + "/" + id, function (x) {

        x = x?.data || x;

        $("#soid").val(x.soid);
        $("#officeName").val(x.sOfficeName);
        $("#outDate").val(x.sOutDate);
        $("#itemName").val(x.sItemName);
        $("#qty").val(x.sQuantity);
        $("#serialized").prop("checked", x.serialized);
        $("#awb").prop("checked", x.awb);
        $("#startNo").val(x.startNo);
        $("#endNo").val(x.endNo);
        $("#balQty").val(x.balQuantity);

        $("#modalTitle").text("Edit Stock Out");
        modal.show();

    }).fail(err => {
        Swal.fire("Error", err.responseText, "error");
    }).always(hideLoader);
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "This will be deleted permanently!",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (res.isConfirmed) {

            showLoader();

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",

                success: function () {
                    Swal.fire("Deleted!", "", "success");
                    loadData();
                },

                error: err => Swal.fire("Error", err.responseText, "error"),

                complete: hideLoader
            });
        }
    });
}


// ================= EXTRA =================
function calculateBalance() {
    let qty = parseInt($("#qty").val()) || 0;
    $("#balQty").val(qty); // simple logic (you can adjust API-based)
}

function clearForm() {
    $("input").val('');
    $("#serialized, #awb").prop("checked", false);
}

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}