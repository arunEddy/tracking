let modal;
const API = AppConfig.apiBaseUrl + "/api/ReturnBooking";

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('bookingModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        modal.show();
    });

    $("#btnSave").click(saveData);

});


// LOAD
function loadData() {

    $.get(API, function (res) {

        let data = res?.data || res;
        let html = "";

        data.forEach((x, i) => {

            html += `<tr>
                <td>${i + 1}</td>
                <td>${x.awbReferenceNo || ''}</td>
                <td>${x.consignorName || ''}</td>
                <td>${x.pickupCity || ''}</td>
                <td class="text-end">
                    <button class="btn btn-warning btn-sm" onclick="edit(${x.rbid})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.rbid})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);
    });
}


// SAVE
function saveData() {

    if (!$("#AWBReferenceNo").val()) return Swal.fire("AWB Ref required");

    let id = $("#rbid").val() || 0;

    let obj = {

        rbid: id,

        AWBReference: $("#AWBReference").val(),
        AWBReferenceNo: $("#AWBReferenceNo").val(),
        BookingOffice: $("#BookingOffice").val(),
        PickupCity: $("#PickupCity").val(),
        PickupPincode: $("#PickupPincode").val(),

        ConsignorName: $("#ConsignorName").val(),
        DepttPersonName: $("#DepttPersonName").val(),
        BookingDate: formatDate($("#BookingDate").val()),
        ExpDeliveryDate: formatDate($("#ExpDeliveryDate").val()),
        ExpDeliveryTime: $("#ExpDeliveryTime").val(),

        Pcs: $("#Pcs").val(),
        ChargeWeight: $("#ChargeWeight").val(),
        MachineWeight: $("#MachineWeight").val(),
        ProductName: $("#ProductName").val(),
        Mode: $("#Mode").val(),
        CODAmount: $("#CODAmount").val(),

        bsName: $("#bsName").val(),
        Address1: $("#Address1").val(),
        Address2: $("#Address2").val(),
        City: $("#City").val(),
        Pincode: $("#Pincode").val(),
        bsState: $("#bsState").val(),
        Phone: $("#Phone").val(),
        Mobile: $("#Mobile").val(),
        ODAChargeApplicable: $("#ODAChargeApplicable").is(":checked") ? "Y" : "N",

        ConsigneeName: $("#ConsigneeName").val(),
        ConsigneeAddress1: $("#ConsigneeAddress1").val(),
        ConsigneeAddress2: $("#ConsigneeAddress2").val(),
        ReturnPhone: $("#ReturnPhone").val(),
        ReturnMobile: $("#ReturnMobile").val(),
        ReturnPincode: $("#ReturnPincode").val(),
        ReturnCity: $("#ReturnCity").val(),

        ReturnAWBNo: $("#ReturnAWBNo").val(),
        ReturnPickupCity: $("#ReturnPickupCity").val(),
        ReturnDestinationCity: $("#ReturnDestinationCity").val(),
        ReturnRemarks: $("#ReturnRemarks").val(),
        ReturnPieces: $("#ReturnPieces").val(),
        ReturnWeight: parseFloat($("#ReturnWeight").val()) || 0,
        ReturnDate: formatDate($("#ReturnDate").val()),
        ReturnTime: $("#ReturnTime").val(),
        ReturnPickupPincode: $("#ReturnPickupPincode").val(),
        ReturnDestinationPicode: $("#ReturnDestinationPicode").val(),
        ReturnProductName: $("#ReturnProductName").val(),
        ReturnMode: $("#ReturnMode").val(),
        ReturnProductType: $("#ReturnProductType").val(),

        IsActive: "Y"
    };

    let type = id == 0 ? "POST" : "PUT";
    let url = id == 0 ? API : API + "/" + id;

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: () => {
            $.when.apply($, requests).then(() => {

                Swal.fire("Success", isUpdate ? "Updated" : "Saved", "success");

                modal.hide();
                loadData();

            }).fail(err => {
                console.log(err.responseText);
                Swal.fire("Error", "Item save failed", "error");
            });
            modal.hide();
            loadData();
        },
        error: () => Swal.fire("Error", "Failed", "error")
    });
}


// EDIT
function edit(id) {

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;

        $("#rbid").val(x.rbid);

        $("#AWBReference").val(x.awbReference);
        $("#AWBReferenceNo").val(x.awbReferenceNo);
        $("#BookingOffice").val(x.bookingOffice);
        $("#PickupCity").val(x.pickupCity);
        $("#PickupPincode").val(x.pickupPincode);

        $("#ConsignorName").val(x.consignorName);
        $("#DepttPersonName").val(x.depttPersonName);

        $("#BookingDate").val(formatInputDate(x.bookingDate));
        $("#ExpDeliveryDate").val(formatInputDate(x.expDeliveryDate));
        $("#ExpDeliveryTime").val(x.expDeliveryTime);

        $("#Pcs").val(x.pcs);
        $("#ChargeWeight").val(x.chargeWeight);
        $("#MachineWeight").val(x.machineWeight);
        $("#ProductName").val(x.productName);
        $("#Mode").val(x.mode);
        $("#CODAmount").val(x.codAmount);

        $("#bsName").val(x.bsName);
        $("#Address1").val(x.address1);
        $("#Address2").val(x.address2);
        $("#City").val(x.city);
        $("#Pincode").val(x.pincode);
        $("#bsState").val(x.bsState);
        $("#Phone").val(x.phone);
        $("#Mobile").val(x.mobile);

        $("#ODAChargeApplicable").prop("checked", x.odaChargeApplicable === "Y");

        $("#ConsigneeName").val(x.consigneeName);
        $("#ConsigneeAddress1").val(x.consigneeAddress1);
        $("#ConsigneeAddress2").val(x.consigneeAddress2);
        $("#ReturnPhone").val(x.returnPhone);
        $("#ReturnMobile").val(x.returnMobile);
        $("#ReturnPincode").val(x.returnPincode);
        $("#ReturnCity").val(x.returnCity);

        $("#ReturnAWBNo").val(x.returnAWBNo);
        $("#ReturnPickupCity").val(x.returnPickupCity);
        $("#ReturnDestinationCity").val(x.returnDestinationCity);
        $("#ReturnRemarks").val(x.returnRemarks);
        $("#ReturnPieces").val(x.returnPieces);
        $("#ReturnWeight").val(x.returnWeight);
        $("#ReturnDate").val(formatInputDate(x.returnDate));
        $("#ReturnTime").val(x.returnTime);
        $("#ReturnPickupPincode").val(x.returnPickupPincode);
        $("#ReturnDestinationPicode").val(x.returnDestinationPicode);
        $("#ReturnProductName").val(x.returnProductName);
        $("#ReturnMode").val(x.returnMode);
        $("#ReturnProductType").val(x.returnProductType);

        modal.show();
    });
}


// DELETE
function deleteRec(id) {

    Swal.fire({
        title: "Delete?",
        showCancelButton: true
    }).then(res => {

        if (res.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: () => {
                    Swal.fire("Deleted");
                    loadData();
                }
            });
        }
    });
}


// HELPERS
function formatDate(date) {
    return date ? new Date(date).toISOString() : null;
}

function formatInputDate(date) {
    return date ? new Date(date).toISOString().split('T')[0] : '';
}

function clearForm() {
    $("input").val('');
    $("select").val('');
}