using Datos; 
using Practica.Model;
using System;
using System.Linq;
using System.Web.UI;

namespace WWW {
    public partial class EditActivity : System.Web.UI.Page {
                private CapaDatos Data {
            get {
                CapaDatos data = (CapaDatos)Application["datos"];
                if (data == null) {
                    data = new CapaDatos();
                    Application["datos"] = data;
                }
                return data;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadActivityData();
            }
        }

        private void LoadActivityData() {
            string activityIdString = Request.QueryString["id"];
            int activityId;

            if (!string.IsNullOrEmpty(activityIdString) && int.TryParse(activityIdString, out activityId)) {
                Activity activityToEdit = Data.GetActivityById(activityId);

                if (activityToEdit != null) {
                    PopulateForm(activityToEdit);
                } else {
                    Response.Redirect("MainView.aspx");
                }
            } else {
                Response.Redirect("MainView.aspx");
            }
        }

        private void PopulateForm(Activity activity) {
            hdnActivityId.Value = activity.Id.ToString();

            txtName.Text = activity.Name;
            txtStartTime.Text = activity.StartTime.ToString("yyyy-MM-ddTHH:mm");
            txtDuration.Text = activity.Duration.ToString();
            txtNotes.Text = activity.Notes;
            ddlActivityType.SelectedValue = activity.TypeActivity;

            pnlRunningCycling.Visible = false;
            pnlSwimming.Visible = false;
            pnlGym.Visible = false;
            pnlOther.Visible = false;

            if (activity is ActivityRunning runningActivity) {
                pnlRunningCycling.Visible = true;
                txtDistance.Text = runningActivity.Distance.ToString();
                txtSlope.Text = runningActivity.Slope.ToString();
                txtPlaceRunBike.Text = runningActivity.Place;
            } else if (activity is ActividadCycling cyclingActivity) {
                pnlRunningCycling.Visible = true;
                txtDistance.Text = cyclingActivity.Distance.ToString();
                txtSlope.Text = cyclingActivity.Slope.ToString();
                txtPlaceRunBike.Text = cyclingActivity.Place;
            } else if (activity is ActivitySwimming swimmingActivity) {
                pnlSwimming.Visible = true;
                txtSwimDistance.Text = swimmingActivity.Distance.ToString();
                txtSwimPlace.Text = swimmingActivity.Place;
            } else if (activity is ActivityGym gymActivity) {
                pnlGym.Visible = true;
                txtCalories.Text = gymActivity.Calories.ToString();
                txtBodyPart.Text = gymActivity.BodyPart;
            } else if (activity is ActivityOther otherActivity) {
                pnlOther.Visible = true;
                txtOtherActivity.Text = otherActivity.OtherActivity;
                txtOtherPlace.Text = otherActivity.Place;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if (Page.IsValid) {
                int activityId = int.Parse(hdnActivityId.Value);
                Activity activityToUpdate = Data.GetActivityById(activityId);

                if (activityToUpdate != null) {
                    activityToUpdate.Name = txtName.Text;
                    activityToUpdate.StartTime = DateTime.Parse(txtStartTime.Text);
                    activityToUpdate.Duration = int.Parse(txtDuration.Text);
                    activityToUpdate.Notes = txtNotes.Text;

                    if (activityToUpdate is ActivityRunning runningActivity) {
                        runningActivity.Distance = float.Parse(txtDistance.Text);
                        runningActivity.Slope = int.Parse(txtSlope.Text);
                        runningActivity.Place = txtPlaceRunBike.Text;
                    } else if (activityToUpdate is ActividadCycling cyclingActivity) {
                        cyclingActivity.Distance = float.Parse(txtDistance.Text);
                        cyclingActivity.Slope = int.Parse(txtSlope.Text);
                        cyclingActivity.Place = txtPlaceRunBike.Text;
                    } else if (activityToUpdate is ActivitySwimming swimmingActivity) {
                        swimmingActivity.Distance = int.Parse(txtSwimDistance.Text);
                        swimmingActivity.Place = txtSwimPlace.Text;
                    } else if (activityToUpdate is ActivityGym gymActivity) {
                        gymActivity.Calories = int.Parse(txtCalories.Text);
                        gymActivity.BodyPart = txtBodyPart.Text;
                    } else if (activityToUpdate is ActivityOther otherActivity) {
                        otherActivity.OtherActivity = txtOtherActivity.Text;
                        otherActivity.Place = txtOtherPlace.Text;
                    }

                    Response.Redirect("MainView.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("MainView.aspx");
        }
    }
}