const API = AppConfig.apiBaseUrl + "/api/NonDelivery";

let modal = new bootstrap.Modal(document.getElementById('ndrModal'));

$(document).ready(function () {

    loadData();

    $("#btnAdd").click(function () {
        clearForm();
        modal.show();
    });

    $("#btnSave").click(function () {
        saveData();
    });

});

function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
}

function formatDate(date) {

    if (!date) return "";

    return new Date(date).toISOString().split('T')[0];
}

function clearForm() {

    $("#ndId").val("");
    $("#DestinationOffice").val("");
    $("#AwbNo").val("");
    $("#NDRDate").val("");
    $("#NDRReason").val("");
    $("#Remarks").val("");
    $("#ShipperName").val("");
    $("#TotalNDR").val("");
    $("#PhysicalAttempt").val("");
    $("#MaxAttempt").val("");
}

function validateForm() {

    if ($("#DestinationOffice").val().trim() == "") {

        Swal.fire({
            icon: 'warning',
            title: 'Validation',
            text: 'Destination Office is required'
        });

        return false;
    }

    if ($("#AwbNo").val().trim() == "") {

        Swal.fire({
            icon: 'warning',
            title: 'Validation',
            text: 'AWB No is required'
        });

        return false;
    }

    if ($("#NDRDate").val().trim() == "") {

        Swal.fire({
            icon: 'warning',
            title: 'Validation',
            text: 'NDR Date is required'
        });

        return false;
    }

    if ($("#NDRReason").val().trim() == "") {

        Swal.fire({
            icon: 'warning',
            title: 'Validation',
            text: 'NDR Reason is required'
        });

        return false;
    }

    return true;
}

function getFormData() {

    return {

        ndId: parseInt($("#ndId").val()) || 0,

        DestinationOffice: $("#DestinationOffice").val(),
        AwbNo: parseFloat($("#AwbNo").val()) || 0,
        NDRDate: $("#NDRDate").val() || null,
        NDRReason: $("#NDRReason").val(),
        Remarks: $("#Remarks").val(),
        ShipperName: $("#ShipperName").val(),
        TotalNDR: parseFloat($("#TotalNDR").val()) || 0,
        PhysicalAttempt: parseFloat($("#PhysicalAttempt").val()) || 0,
        MaxAttempt: parseFloat($("#MaxAttempt").val()) || 0,

        createdby: "ADMIN",
        createdon: new Date().toISOString(),

        mfdby: "ADMIN",
        mfdon: new Date().toISOString(),

        IsActive: "Y",
        EndDate: null
    };
}

function saveData() {

    if (!validateForm())
        return;

    let model = getFormData();

    let id = $("#ndId").val();

    let method = id ? "PUT" : "POST";
    let url = id ? `${API}/${id}` : API;

    showLoader();

    $.ajax({

        url: url,
        type: method,
        contentType: "application/json",
        data: JSON.stringify(model),

        success: function (res) {

            hideLoader();

            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: id
                    ? 'Record updated successfully'
                    : 'Record saved successfully'
            });

            modal.hide();

            clearForm();

            loadData();
        },

        error: function (xhr) {

            hideLoader();

            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: xhr.responseText || 'Something went wrong'
            });
        }

    });
}

function loadData() {

    showLoader();

    $.ajax({

        url: API,
        type: "GET",

        success: function (res) {

            hideLoader();

            let data = res?.data || res;

            let html = "";

            if (data.length === 0) {

                html += `
                    <tr>
                        <td colspan="11" class="text-center text-danger fw-bold">
                            No Record Found
                        </td>
                    </tr>
                `;
            }

            $.each(data, function (i, item) {

                html += `
                    <tr>

                        <td>${i + 1}</td>

                        <td>${item.destinationOffice ?? ''}</td>

                        <td>${item.awbNo ?? ''}</td>

                        <td>${formatDate(item.ndrDate)}</td>

                        <td>${item.ndrReason ?? ''}</td>

                        <td>${item.remarks ?? ''}</td>

                        <td>${item.shipperName ?? ''}</td>

                        <td>${item.totalNDR ?? 0}</td>

                        <td>${item.physicalAttempt ?? 0}</td>

                        <td>${item.maxAttempt ?? 0}</td>

                        <td>

                            <button class="action-btn text-primary"
                                    onclick="edit(${item.ndId})">

                                <i class="bi bi-pencil-square"></i>

                            </button>

                            <button class="action-btn text-danger"
                                    onclick="deleteRec(${item.ndId})">

                                <i class="bi bi-trash"></i>

                            </button>

                        </td>

                    </tr>
                `;
            });

            $("#tblData").html(html);
        },

        error: function () {

            hideLoader();

            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to load data'
            });
        }

    });
}

function edit(id) {

    showLoader();

    $.ajax({

        url: `${API}/${id}`,
        type: "GET",

        success: function (res) {

            hideLoader();

            let data = res?.data || res;

            $("#ndId").val(data.ndId);

            $("#DestinationOffice").val(data.destinationOffice);
            $("#AwbNo").val(data.awbNo);
            $("#NDRDate").val(formatDate(data.ndrDate));
            $("#NDRReason").val(data.ndrReason);
            $("#Remarks").val(data.remarks);
            $("#ShipperName").val(data.shipperName);
            $("#TotalNDR").val(data.totalNDR);
            $("#PhysicalAttempt").val(data.physicalAttempt);
            $("#MaxAttempt").val(data.maxAttempt);

            modal.show();
        },

        error: function () {

            hideLoader();

            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to fetch record'
            });
        }

    });
}

function deleteRec(id) {

    Swal.fire({

        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',

        showCancelButton: true,

        confirmButtonColor: '#d33',
        cancelButtonColor: '#6c757d',

        confirmButtonText: 'Yes, Delete'

    }).then((result) => {

        if (result.isConfirmed) {

            showLoader();

            $.ajax({

                url: `${API}/${id}`,
                type: "DELETE",

                success: function () {

                    hideLoader();

                    Swal.fire({
                        icon: 'success',
                        title: 'Deleted',
                        text: 'Record deleted successfully'
                    });

                    loadData();
                },

                error: function () {

                    hideLoader();

                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Delete failed'
                    });
                }

            });
        }
    });
}