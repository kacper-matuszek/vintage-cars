import { useRouter } from "next/router"

export const redirectTo = (route: string) => {
    const router = useRouter();
    router.push(route);
}