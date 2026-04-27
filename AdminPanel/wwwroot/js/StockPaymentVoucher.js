let modal;
const API = AppConfig.apiBaseUrl + "/api/StockPaymentVoucher";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('voucherModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Payment Voucher");
        modal.show();
    });

    $("#btnSave").click(saveData);

    // Auto calculation
    $("#qty, #rate").on("input", calculateTotal);
});


// ================= VALIDATION =================
function validateForm() {

    if (!$("#voucherNo").val().trim())
        return showError("Voucher Number required");

    if (!$("#franchise").val().trim())
        return showError("Franchise Name required");

    if (!$("#qty").val())
        return showError("Quantity required");

    if (!$("#paymentDate").val())
        return showError("Payment Date required");

    if (!$("#rate").val())
        return showError("Amount per AWB required");

    if (!$("#paymentMode").val())
        return showError("Payment Mode required");

    return true;
}

function showError(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
}


// ================= DATE FORMAT =================
function formatDate(dateStr) {
    if (!dateStr) return '';

    let d = new Date(dateStr);

    let day = String(d.getDate()).padStart(2, '0');
    let month = d.toLocaleString('en-GB', { month: 'short' });
    let year = d.getFullYear();

    return `${day}-${month}-${year}`;
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
                    <td>${x.spvId}</td>
                    <td>${x.voucherNumber}</td>
                    <td>${x.franchiseName}</td>
                     <td>${formatDate(x.paymentDate)}</td>
                    <td>${x.quantity}</td>
                    <td>${x.amountPerAWB}</td>
                    <td>${x.totalAmount}</td>
                     <td>${x.paymentMode}</td>
                      <td>${x.bankName || '-'}</td>
                      <td>${x.chequeDDNo}</td>
                       <td>${x.remarks}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="edit(${x.spvId})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.spvId})">Delete</button>
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

    let id = parseInt($("#spvId").val()) || 0;

    let obj = {
        spvId: id,
        voucherNumber: $("#voucherNo").val(),
        franchiseName: $("#franchise").val(),
        paymentDate: $("#paymentDate").val(),
        quantity: $("#qty").val(),
        amountPerAwb: $("#rate").val(),
        totalAmount: $("#total").val(),
        paymentMode: $("#paymentMode").val(),
        bankName: $("#bankname").val(),
        chequeDDNo: $("#cheque").val(),
        remarks: $("#remarks").val()
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

        $("#spvId").val(x.spvId);
        $("#voucherNo").val(x.voucherNumber);
        $("#franchise").val(x.franchiseName);
        $("#paymentDate").val(x.paymentDate);
        $("#qty").val(x.quantity);
        $("#rate").val(x.amountPerAWB);
        $("#total").val(x.totalAmount);
        $("#paymentMode").val(x.paymentMode);
        $("#bankname").val(x.bankName);
        $("#cheque").val(x.chequeDDNo);
        $("#remarks").val(x.remarks);

        $("#modalTitle").text("Edit Payment Voucher");
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


// ================= AUTO CALC =================
function calculateTotal() {

    let qty = parseFloat($("#qty").val()) || 0;
    let rate = parseFloat($("#rate").val()) || 0;

    $("#total").val(qty * rate);
}


// ================= HELPERS =================
function clearForm() {
    $("input, textarea").val('');
}

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}