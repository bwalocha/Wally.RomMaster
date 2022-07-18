import { defineNuxtConfig } from 'nuxt'

// https://v3.nuxtjs.org/api/configuration/nuxt.config
export default defineNuxtConfig({
    /*components: {
        global: true,
        dirs: ['~/components']
    },*/
    plugins: [
        '@/plugins/fontawesome'
    ],
    css: [
        '@fortawesome/fontawesome-svg-core/styles.css'
    ]
})
