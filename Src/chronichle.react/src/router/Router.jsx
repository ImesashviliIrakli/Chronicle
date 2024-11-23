import {
    Route,
    createBrowserRouter,
    createRoutesFromElements,
    Navigate,
} from "react-router-dom";
// Layouts
import RootLayout from "../layout/root-layout/RootLayout";
// Pages
import NotFound from "../pages/not-found/NotFound";
import Login from "../pages/auth/Login";
import Registration from "../pages/auth/Registration";
import Tasks from "../pages/tasks/Tasks";


export const router = createBrowserRouter(
    createRoutesFromElements(
        <>
            <Route path="/" element={<Navigate to="/tasks" />} />
            <Route path="/" element={<RootLayout />}>
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Registration />} />
                <Route path="/tasks" element={ <Tasks/>} />
                <Route path="*" element={<NotFound />} />
            </Route>
        </>
    )
);