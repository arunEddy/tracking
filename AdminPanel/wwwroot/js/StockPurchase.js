let modal;
const API = AppConfig.apiBaseUrl + "/api/StockPurchase";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('stockModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Stock");
        modal.show();
    });

    $("#btnSave").click(saveData);
});


// ================= VALIDATION =================
function validateForm() {

    if (!$("#officeName").val().trim())
        return showError("Office Name required");

    if (!$("#vendorName").val().trim())
        return showError("Vendor Name required");

    if (!$("#purchaseDate").val().trim())
        return showError("Purchase Date required");

    if (!$("#itemName").val().trim())
        return showError("Item Name required");

    if (!$("#qty").val())
        return showError("Quantity required");

    //if (!$("#purchaseRate").val())
    //    return showError("Purchase Rate required");

    return true;
}

function showError(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
}


// ================= LOAD =================
function loadData() {

    showLoader();

    $.get(API, function (res) {

        let data = res?.data || res;

        let html = "";

        if (!data || data.length === 0) {
            html = "<tr><td colspan='10'>No Data Found</td></tr>";
        } else {

            data.forEach(x => {

                html += `<tr>
                    <td>${x.stpId}</td>
                    <td>${x.officeName}</td>
                    <td>${x.vendorName}</td>
                    <td>${x.itemName}</td>
                    <td>${x.quantity}</td>
                    <td>${x.purchaseRate}</td>
                    <td>${x.vendorRate || '-'}</td>
                    <td>${x.startNo || '-'}</td>
                    <td>${x.endNo || '-'}</td>
                    <td>${x.bookRequired ? 'Yes' : 'No'}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="edit(${x.stpId})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.stpId})">Delete</button>
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
    clearForm();
    let id = parseInt($("#stpId").val()) || 0;

    let obj = {
        stpId: id,
        officeName: $("#officeName").val(),
        vendorName: $("#vendorName").val(),
        purchaseDate: $("#purchaseDate").val() || null,
        itemName: $("#itemName").val(),
        quantity: parseInt($("#qty").val()),
        purchaseRate: parseFloat($("#purchaseRate").val()),
        vendorRate: $("#vendorRate").val() || null,
        startNo: $("#startNo").val(),
        endNo: $("#endNo").val(),
        bookRequired: $("#bookRequired").is(":checked") ? "true" : "false"
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
    clearForm(true); 
    $.get(API + "/" + id, function (x) {

        x = x?.data || x;
       
        $("#stpId").val(x.stpId);
        $("#officeName").val(x.officeName);
        $("#vendorName").val(x.vendorName);
        $("#purchaseDate").val(x.purchaseDate);
        $("#itemName").val(x.itemName);
        $("#qty").val(x.quantity);
        $("#purchaseRate").val(x.purchaseRate);
        $("#vendorRate").val(x.vendorRate);
        $("#startNo").val(x.startNo);
        $("#endNo").val(x.endNo);
        $("#bookRequired").prop("checked", x.bookRequired);

        $("#modalTitle").text("Edit Stock");
        console.log("stpId after set:", $("#stpId").val());
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


// ================= HELPERS =================
function clearForm(isEdit = false) {
    if (!isEdit) {
        $("input").val('');

        $("#serialized, #awb").prop("checked", false);
    }
}
function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}