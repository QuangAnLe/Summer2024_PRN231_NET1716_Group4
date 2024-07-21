@page
@model ClientMilkTeamPage.Pages.Shipper.ShipperPageModel
@{
    ViewData["Title"] = "Task List";
}

<h1>Task List</h1>

<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.TaskUserVM[0].WorkName)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.TaskUserVM[0].WorkDescription)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.TaskUserVM[0].UserID)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.TaskUserVM[0].OrderID)
            </th>
            <th>
                Status
            </th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.TaskUserVM)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.WorkName)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.WorkDescription)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.UserID)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.OrderID)
                </td>
                <td>
                    <form method="post" asp-page-handler="UpdateStatus">
                        <input type="hidden" name="TaskId" value="@item.TaskId" />
                        <input type="hidden" name="TeaID" value="@item.OrderID" /> <!-- Assuming OrderID is TeaID -->
                        <input type="hidden" name="UserID" value="@item.UserID" />
                        <input type="radio" name="Status" value="Success" /> Success
                        <input type="radio" name="Status" value="Failed" /> Failed
                        <button type="submit">Update</button>
                    </form>
                </td>
            </tr>
        }
    </tbody>
</table>

<!-- Modal for inputting reason (rendered conditionally) -->
@if (Model.ShowModal)
{
    <div id="failureReasonModal" class="modal" style="display: block;">
        <div class="modal-content">
            <h2>Enter Reason for Failure</h2>
            <form method="post" asp-page-handler="SubmitFailureReason">
                <textarea name="FailureReason" required></textarea>
                <button type="submit">Submit</button>
            </form>
        </div>
    </div>
}

<style>
    .modal {
        display: none;
        position: fixed;
        z-index: 1;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgb(0,0,0);
        background-color: rgba(0,0,0,0.4);
    }

    .modal-content {
        background-color: #fefefe;
        margin: 15% auto;
        padding: 20px;
        border: 1px solid #888;
        width: 80%;
    }
</style>
<a asp-page="https://localhost:7097/Shipper/TaskHistory">Go to Task History</a>