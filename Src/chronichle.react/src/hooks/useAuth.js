import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { useRootContext } from "./useRootContext";
import { useAlertBarContext } from "./useAlertBarContext";
import { fetchData } from "../functions/fetchData";

export const useAuth = (mutationKey, urlParam, authType = "login") => {
    const { baseUrl } = useRootContext();
    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const navigate = useNavigate();

    switch (authType) {
        case "login":
        case "register":
            const mutation = useMutation({
                mutationKey: mutationKey,
                mutationFn: (authData) =>
                    fetchData({
                        url: baseUrl + urlParam,
                        method: "post",
                        data: JSON.stringify(authData),
                    }),
                onSuccess: (result) => {
                    if (authType === "login") {
                        sessionStorage.setItem("userData", JSON.stringify(result.data));
                        sessionStorage.setItem("token", JSON.stringify(result.data.token));
                        sessionStorage.setItem("login", "true");
                    }
                    handleSnackbarOpen();
                    setSnackbarStat(snackbarStatus.success);
                    setSnackbarMessage(
                        `${authType === "login" ? "Logged in" : "Registered"} Successfully!`
                    );
                    setTimeout(
                        () => navigate(`${authType === "login" ? "/" : "/login"}`),
                        1000
                    );
                },
                onError: (error) => {
                    console.log(error);
                    if (authType === "login") {
                        sessionStorage.removeItem("userData");
                        sessionStorage.removeItem("token");
                        sessionStorage.removeItem("login");
                    }
                    handleSnackbarOpen();
                    setSnackbarStat(snackbarStatus.error);
                    setSnackbarMessage(error.data?.Result?.Message || "An Error Occured");
                },
            });
            return mutation;

        default:
            return useMutation();
    }
};