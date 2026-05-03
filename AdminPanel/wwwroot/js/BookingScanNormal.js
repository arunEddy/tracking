const API = AppConfig.apiBaseUrl + "/api/BookingScanNormal";

let modal;
let editId = 0;

$(document).ready(function () {
    modal = new bootstrap.Modal(document.getElementById("mainModal"));
    loadData();
});

// ================= LOAD =================
function loadData() {
    $.get(API, function (res) {
        let data = res?.data || res;

        let html = "";
        data.forEach((x, i) => {
            html += `<tr>
                <td>${i + 1}</td>
                <td>${x.awb || ""}</td>
                <td>${x.customerName || ""}</td>
                <td>${x.city || ""}</td>
                <td>${x.pincode || ""}</td>
                <td class="text-end">
                    <button class="btn btn-sm btn-warning" onclick="edit(${x.bsnid})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRec(${x.bsnid})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);
    });
}

// ================= OPEN =================
function openModal() {
    resetForm();
    modal.show();
}

// ================= VALIDATION =================
function validate() {

    if (!$("#BookingOffice").val()) return Swal.fire("Booking Office required"), false;
    if (!$("#BookingDate").val()) return Swal.fire("Booking Date required"), false;
    if (!$("#PickupCity").val()) return Swal.fire("Pickup City required"), false;
    if (!$("#ManifestNo").val()) return Swal.fire("Manifest No required"), false;
    if (!$("#AWB").val()) return Swal.fire("AWB required"), false;
    if (!$("#Mode").val()) return Swal.fire("Mode required"), false;
    if (!$("#ChargeWt").val()) return Swal.fire("ChargeWt required"), false;
    if (!$("#ProductName").val()) return Swal.fire("Product required"), false;
    if (!$("#bsnName").val()) return Swal.fire("Name required"), false;
    if (!$("#City").val()) return Swal.fire("City required"), false;
    if (!$("#Pincode").val()) return Swal.fire("Pincode required"), false;

    return true;
}

// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let obj = {
        bsnid: editId,
        BookingOffice: $("#BookingOffice").val(),
        PickupCity: $("#PickupCity").val(),
        PickupPincode: $("#PickupPincode").val(),
        BookingDate: fixDate($("#BookingDate").val()),
        ManifestNo: $("#ManifestNo").val(),
        ManifestDate: fixDate($("#ManifestDate").val()),
        CreditLimit: $("#CreditLimit").val(),
        AWB: $("#AWB").val(),
        CustomerName: $("#CustomerName").val(),
        Mode: $("#Mode").val(),
        Pcs: $("#Pcs").val(),
        ActualWeight: $("#ActualWeight").val(),
        Volumetric: $("#Volumetric").val(),
        VolWt: $("#VolWt").val(),
        ChargeWt: $("#ChargeWt").val(),
        ProductName: $("#ProductName").val(),
        CODAmount: $("#CODAmount").val(),
        BookType: $("#BookType").val(),
        Content: $("#Content").val(),
        RefNo: $("#RefNo").val(),
        ProductService: $("#ProductService").val(),
        bsnName: $("#bsnName").val(),
        bsnAddress: $("#bsnAddress").val(),
        City: $("#City").val(),
        Pincode: $("#Pincode").val(),
        ODAChargeApplicable: $("#ODAChargeApplicable").is(":checked") ? "Y" : "N",
    };

    let type = editId ? "PUT" : "POST";
    let url = editId ? API + "/" + editId : API;

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function () {
            Swal.fire("Saved Successfully");
            modal.hide();
            loadData();
        },
        error: function () {
            Swal.fire("Error while saving");
        }
    });
}

// ================= EDIT =================
function edit(id) {

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;
        editId = x.bsnid;

        $("#BookingOffice").val(x.bookingOffice);
        $("#BookingDate").val(fixDate(x.bookingDate));
        $("#PickupCity").val(x.pickupCity);
        $("#PickupPincode").val(x.pickupPincode);
        $("#ManifestNo").val(x.manifestNo);
        $("#ManifestDate").val(fixDate(x.manifestDate));
        $("#CreditLimit").val(x.creditLimit);

        $("#AWB").val(x.awb);
        $("#CustomerName").val(x.customerName);
        $("#Mode").val(x.mode);
        $("#Pcs").val(x.pcs);
        $("#ActualWeight").val(x.actualWeight);
        $("#Volumetric").val(x.volumetric);
        $("#VolWt").val(x.volWt);
        $("#ChargeWt").val(x.chargeWt);
        $("#ProductName").val(x.productName);
        $("#CODAmount").val(x.codAmount);
        $("#BookType").val(x.bookType);
        $("#Content").val(x.content);
        $("#RefNo").val(x.refNo);
        $("#ProductService").val(x.productService);

        $("#bsnName").val(x.bsnName);
        $("#bsnAddress").val(x.bsnAddress);
        $("#City").val(x.city);
        $("#Pincode").val(x.pincode);
        $("#ODAChargeApplicable").prop("checked", x.odaChargeApplicable === "Y");


        modal.show();
    });
}

// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete record?",
        icon: "warning",
        showCancelButton: true
    }).then((r) => {

        if (r.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: function () {
                    Swal.fire("Deleted");
                    loadData();
                }
            });
        }
    });
}

// ================= PRINT =================
function printManifest() {
    let no = $("#ManifestNoPrint").val();
    if (!no) return Swal.fire("Enter Manifest No");
    window.open(API + "/PrintManifest/" + no);
}

// ================= HELPERS =================
function resetForm() {
    $("input").val("");
    editId = 0;
}

function fixDate(d) {
    return d ? new Date(d).toISOString().split('T')[0] : null;
}

$(document).on("input", ".only-number", function () {
    this.value = this.value.replace(/\D/g, "");
});